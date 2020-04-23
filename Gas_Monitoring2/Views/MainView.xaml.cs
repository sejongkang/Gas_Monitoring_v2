using System.Windows.Controls;
//웹서버 관련 내용
using System.Collections.Generic;
using System.Threading;
using SimpleHttpServer;
using Gas_Monitoring2.Models;
using System;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Win32;

namespace Gas_Monitoring2.Views
{
    public partial class MainView : UserControl
    {
        public Thread thread;
        public MainView()
        {
            InitializeComponent();
            SetBrowserEmulationMode();
            thread = new Thread(MapInit);
            Console.WriteLine("Behind code");

            var route_config = new List<SimpleHttpServer.Models.Route>() {
                new SimpleHttpServer.Models.Route {
                    Name = "FileSystem Static Handler",
                    UrlRegex = @"^/(.*)$",
                    Method = "GET",
                    Callable = new SimpleHttpServer.RouteHandlers.FileSystemRouteHandler()
                    {
                        BasePath = @"./", ShowDirectories=true }.Handle,
                },
            };

            // 포트번호의 의미는..... 304-1호 7호관.... 외우기 힘들까봐
            HttpServer httpServer = new HttpServer(30417, route_config);

            Thread webServerThread = new Thread(new ThreadStart(httpServer.Listen));
            webServerThread.IsBackground = true;
            webServerThread.Start();
            
            this.Browser.Navigate("http://localhost:30417/Web/map.html");

        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                this.Browser.InvokeScript("addMarker", new object[] { "35.47098611", "129.1768722" });
            }
            catch (Exception ex)
            {
                string msg = "Could not call script: " +
                             ex.Message +
                            "\n\nPlease click the 'Load HTML Document with Script' button to load.";
            }
        }
        public void MapInit()
        {
            while (true)
            {
                Console.WriteLine("Map Thread");

                List<string> serial = new List<string>();
                List<string> ppm = new List<string>();
                List<string> ppm2 = new List<string>();
                List<string> ppm3 = new List<string>();
                List<string> ppm4 = new List<string>();
                List<string> ppm5 = new List<string>();
                List<string> ppm6 = new List<string>();
                List<string> date = new List<string>();
                List<string> loading = new List<string>();
                List<string> local = new List<string>();
                List<string> lat = new List<string>();
                List<string> longti = new List<string>();
                List<string> conn = new List<string>();
                try
                {
                    DB map_db = new DB();
                    if (map_db.DBConn() == false)
                    {
                        Console.WriteLine("map_db DB Error");
                    }
                    else
                    {
                        String sql = "SELECT * FROM gas_module ORDER BY idx;";
                        MySqlDataReader rdr = map_db.select(sql);
                        if (rdr != null)
                        {
                            while (rdr.Read())
                            {
                                try
                                {
                                    string dev_serial = rdr.GetString(1);
                                    serial.Add(dev_serial);
                                    DB map_db2 = new DB();
                                    if (map_db2.DBConn() == false)
                                    {
                                        Console.WriteLine("map_db2 DB Error");
                                    }
                                    else
                                    {
                                        String sql2 = "SELECT ppm,ppm2,ppm3,ppm4,ppm5,ppm6,regdate,loading,gas_module.`local`,gas_module.`lat`,gas_module.`long` FROM result_info INNER JOIN gas_log ON result_info.log_idx = gas_log.idx " +
                                        "INNER JOIN gas_module ON gas_log.gas_module_idx = gas_module.serial WHERE gas_module_idx = '" + dev_serial + "' ORDER BY regdate DESC limit 1;";
                                        MySqlDataReader rdr2 = map_db2.select(sql2);
                                        while (rdr2.Read())
                                        {
                                            ppm.Add(rdr2.GetDouble(0).ToString());
                                            ppm2.Add(rdr2.GetDouble(1).ToString());
                                            ppm3.Add(rdr2.GetDouble(2).ToString());
                                            ppm4.Add(rdr2.GetDouble(3).ToString());
                                            ppm5.Add(rdr2.GetDouble(4).ToString());
                                            ppm6.Add(rdr2.GetDouble(5).ToString());
                                            DateTime datetime = rdr2.GetDateTime(6);
                                            date.Add(String.Format("{0:yyyy-MM-dd HH:mm:ss}", datetime));
                                            loading.Add(rdr2.GetInt16(7).ToString());
                                            local.Add(rdr2.GetString(8));
                                            lat.Add(rdr2.GetDouble(9).ToString());
                                            longti.Add(rdr2.GetDouble(10).ToString());

                                            DateTime tmp_now = DateTime.Now.AddMinutes(-1);
                                            int compare = DateTime.Compare(tmp_now, datetime);

                                            if (compare < 0)
                                            {
                                                conn.Add("ON");
                                            }
                                            else
                                            {
                                                conn.Add("OFF");
                                            }
                                        }
                                        rdr2.Close();
                                    }
                                    map_db2.Conn.Close();
                                }catch(Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                            rdr.Close();
                        }
                        map_db.Conn.Close();
                    }
                    Browser.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                            delegate ()
                            {
                                this.Browser.InvokeScript("CallScript", new Object[]{JsonConvert.SerializeObject(new
                                {
                                    serial,ppm,ppm2,ppm3,ppm4,ppm5,ppm6,date,loading,local,lat,longti,conn
                                })});
                            }
                        ));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Thread.Sleep(Properties.Settings.Default.timer);
            }
        }
        private void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            thread.Start();
        }
        public void SetBrowserEmulationMode()
        {
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
                return;
            UInt32 mode = 10000;
            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, mode);
        }
        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {

            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }

        }

    }
}
