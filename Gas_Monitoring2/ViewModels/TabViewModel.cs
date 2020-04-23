using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using Gas_Monitoring2.Models;
using MySql.Data.MySqlClient;

namespace Gas_Monitoring2.ViewModels
{
    [POCOViewModel]
    public class TabViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        private Device dev;
        private string dev_name;
        private DateTime date_time;
        private DateTime max_date;
        private bool is_loading;
        private string conn_img;
        private string conn_stk;
        private string conn_text;
        private string loading;
        private DateTime selected_date;
        private string selected_term;
        private string selected_type;
        private string measureunit;
        private string multi;
        private string aggreate;
        private int level;
        private AlertSetting alert_set;

        private DelegateCommand pickcommand;
        private DelegateCommand buttonCommand;
        private DelegateCommand termcommand;
        private DelegateCommand typecommand;

        public bool Should;
        public Device Dev { get => dev; set => dev = value; }
        public virtual ObservableCollection<double> Ppm_list { get; set; }
        public virtual ObservableCollection<ResultSetting> Setting { get; set; }
        public virtual ObservableCollection<String> Term_list { get; set; }
        public virtual ObservableCollection<String> Type_list { get; set; }
        public virtual ObservableCollection<DataPoint> his_tmp { get; set; }
        public virtual ObservableCollection<DataPoint> Gas_his { get; set; }
        public virtual ObservableCollection<DataPoint> Gas_his2 { get; set; }
        public virtual ObservableCollection<DataPoint> Gas_his3 { get; set; }
        public virtual ObservableCollection<DataPoint> Gas_his4 { get; set; }
        public virtual ObservableCollection<DataPoint> Gas_his5 { get; set; }
        public virtual ObservableCollection<DataPoint> Gas_his6 { get; set; }
        public virtual ObservableCollection<Level> Levels { get; set; }
        public DateTime Date_time { get => date_time; set => date_time = value; }
        public virtual string Date_time_str { get; set; }
        public bool Is_loading { get => is_loading; set => is_loading = value; }
        public string Conn_img { get => conn_img; set => conn_img = value; }
        public string Conn_stk { get => conn_stk; set => conn_stk = value; }
        public string Conn_text { get => conn_text; set => conn_text = value; }
        public DelegateCommand Pickcommand { get => pickcommand; set => pickcommand = value; }
        public DelegateCommand ButtonCommand { get => buttonCommand; set => buttonCommand = value; }
        public DelegateCommand Termcommand { get => termcommand; set => termcommand = value; }
        public DelegateCommand Typecommand { get => typecommand; set => typecommand = value; }
        public string Loading { get => loading; set => loading = value; }
        public DateTime Selected_date { get => selected_date; set => selected_date = value; }
        public string Selected_term { get => selected_term; set => selected_term = value; }
        public string Selected_type { get => selected_type; set => selected_type = value; }
        public string Measureunit { get => measureunit; set => measureunit = value; }
        public string Multi { get => multi; set => multi = value; }
        public string Aggreate { get => aggreate; set => aggreate = value; }
        public DateTime Max_date { get => max_date; set => max_date = value; }
        public int Level { get => level; set => level = value; }
        public AlertSetting Alert_set { get => alert_set; set => alert_set = value; }
        public string Dev_name { get => dev_name; set => dev_name = value; }
        public Log Dev_log { get; set; }
        public string Gas { get; set; }
        public List<string> Src_enable = new List<string> {
                Path.GetFullPath("../../Images/nose.png"),
                Path.GetFullPath("../../Images/hazard.png"),
            };
        public List<string> Src_disable = new List<string> {
                Path.GetFullPath("../../Images/nose2.png"),
                Path.GetFullPath("../../Images/hazard2.png"),
            };
        public List<string> Src_conn = new List<string> {
                Path.GetFullPath("../../Images/conn.png"),
                Path.GetFullPath("../../Images/disconn.png"),
            };

        public TabViewModel()
        {
            //Console.WriteLine("TabVIewModel 기본생성자");
        }
        public TabViewModel(Device dev)
        {
            Should = true;
            Dev = dev;
            Dev_name = "Device " + Dev.Dev_num;
            Console.WriteLine("TabVIewModel "+Dev_name);
            Selected_date = DateTime.Now;
            Selected_term = "6시간";
            Selected_type = "Maximum";
            Measureunit = "Hour";
            Multi = "6";
            Date_time_str = "-";
            Loading = "Hidden";
            Aggreate = Selected_type;
            Level = 1;
            Alert_set = new AlertSetting();
            Alert_set.Set("정상", "#FF43C743", 1, 40, 30, 30);
            Max_date = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
            Ppm_list = new ObservableCollection<double>();
            Gas_his = new ObservableCollection<DataPoint>();
            Gas_his2 = new ObservableCollection<DataPoint>();
            Gas_his3 = new ObservableCollection<DataPoint>();
            Gas_his4 = new ObservableCollection<DataPoint>();
            Gas_his5 = new ObservableCollection<DataPoint>();
            Gas_his6 = new ObservableCollection<DataPoint>();
            Term_list = new ObservableCollection<string> { "10분", "30분", "1시간", "3시간", "6시간" };
            Type_list = new ObservableCollection<string> { "Average", "Minimum", "Maximum" };
            Levels = MainViewModel.Levels;
            Setting = new ObservableCollection<ResultSetting>
            {
                new ResultSetting(),
                new ResultSetting(),
                new ResultSetting(),
                new ResultSetting(),
                new ResultSetting(),
                new ResultSetting()
            };
            //그래프 관련 커맨드 바인딩
            Pickcommand = new DelegateCommand(Pick);
            ButtonCommand = new DelegateCommand(Click);
            Termcommand = new DelegateCommand(Pick_Term);
            Typecommand = new DelegateCommand(Pick_Type);
            //5분마다 맥스 날짜 설정
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(5);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            //실시간 데이터 쓰레드 생성
            Thread realtime_th = new Thread(new ThreadStart(Refresh));
            realtime_th.IsBackground = true;
            realtime_th.Start();
        }
        ~TabViewModel()
        {
            Console.WriteLine("잘가"+ Dev_name);
        }
        public void Refresh()
        {
            int compare = 0;
            DateTime tmp_now;
            while (Should)
            {
                Console.WriteLine("Refresh"+Dev_name);
                try
                {
                    DB refresh_db = new DB();
                    if (refresh_db.DBConn() == false)
                    {
                        Console.WriteLine("Refresh DB Error");
                        Messenger.Default.Send<int, MainViewModel>(1);
                    }
                    else
                    {
                        Messenger.Default.Send<int, MainViewModel>(0);
                        String sql = "SELECT ppm,ppm2,ppm3,ppm4,ppm5,ppm6,regdate,loading FROM result_info INNER JOIN gas_log ON result_info.log_idx = gas_log.idx " +
                            "WHERE gas_module_idx = '" + Dev.Serial + "' ORDER BY regdate DESC limit 1;";
                        MySqlDataReader rdr = refresh_db.select(sql);
                        Ppm_list.Clear();
                        while (rdr.Read())
                        {
                            Console.WriteLine("rdr");

                            for (int i = 0; i < 6; i++)
                            {
                                //가스별 PPM 제한
                                double tmp = Math.Truncate(rdr.GetDouble(i));
                                if (tmp < 0)
                                {
                                    Ppm_list.Add(0);
                                }
                                else
                                {
                                    if (i == 0 || i == 2)
                                    {
                                        if (tmp > 30)
                                        {
                                            Ppm_list.Add(30);
                                        }
                                        else
                                        {
                                            Ppm_list.Add(tmp);
                                        }
                                    }
                                    else if (i == 1 || i == 3)
                                    {
                                        if (tmp > 300)
                                        {
                                            Ppm_list.Add(300);
                                        }
                                        else
                                        {
                                            Ppm_list.Add(tmp);
                                        }
                                    }
                                    else if (i == 4)
                                    {
                                        if (tmp > 3000)
                                        {
                                            Ppm_list.Add(3000);
                                        }
                                        else
                                        {
                                            Ppm_list.Add(tmp);
                                        }
                                    }
                                    else
                                    {
                                        if (tmp > 10000)
                                        {
                                            Ppm_list.Add(10000);
                                        }
                                        else
                                        {
                                            Ppm_list.Add(tmp);
                                        }
                                    }
                                }
                            }
                            Date_time = rdr.GetDateTime(6);
                            Date_time_str = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Date_time);
                            Is_loading = rdr.GetBoolean(7);
                        }
                        rdr.Close();
                        tmp_now = DateTime.Now.AddMinutes(-1);
                        compare = DateTime.Compare(tmp_now, Date_time);

                        if (compare < 0)
                        {
                            Dev.Conn = "ON";
                            Alert_Check();
                        }
                        else
                        {
                            Dev.Conn = "OFF";
                        }
                        Result_Set();
                        refresh_db.Conn.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Thread.Sleep(Properties.Settings.Default.timer);
            }
        }
        //실시간 결과에 따라서 UI 바인딩 변경
        public void Result_Set()
        {
            if (Dev.Conn == "ON")
            {
                //Conn_img = Src_conn[0];
                Conn_stk = "#FF64C54F";
                Conn_text = "Hidden";
            }
            else
            {
                //Conn_img = Src_conn[1];
                Conn_stk = "#FFB2B2B2";
                Conn_text = "Visible";
                Is_loading = false;
            }
            if (Is_loading == true)
            {
                Loading = "Visible";
            }
            else
            {
                Loading = "Hidden";
            }
            if (Ppm_list[0] == 0)
            {
                Setting[0].Set("황화수소", "H2S", "#FFDAE5E6", "{x:Null}", Src_disable[0], "White", "White", "Hidden");
            }
            else if (Ppm_list[0] == 30)
            {
                Setting[0].Set("황화수소", "H2S", "#FF63ADB8", "Black", Src_enable[0], "Black", "#FF2C3FF3", "Visible");
            }
            else
            {
                Setting[0].Set("황화수소", "H2S", "#FF63ADB8", "Black", Src_enable[0], "Black", "Black", "Hidden");
            }
            if (Ppm_list[1] == 0)
            {
                Setting[1].Set("암모니아", "NH3", "#FFF5FBF3", "{x:Null}", Src_disable[0], "White", "White", "Hidden");
            }
            else if (Ppm_list[1] == 300)
            {
                Setting[1].Set("암모니아", "NH3", "#FF93A68D", "Black", Src_enable[0], "Black", "#FF2C3FF3", "Visible");
            }
            else
            {
                Setting[1].Set("암모니아", "NH3", "#FF93A68D", "Black", Src_enable[0], "Black", "Black", "Hidden");
            }
            if (Ppm_list[2] == 0)
            {
                Setting[2].Set("메틸메르캅탄", "CH3SH", "#FFF2ECF7", "{x:Null}", Src_disable[0], "White", "White", "Hidden");
            }
            else if (Ppm_list[2] == 30)
            {
                Setting[2].Set("메틸메르캅탄", "CH3SH", "#FF85788F", "Black", Src_enable[0], "Black", "#FF2C3FF3", "Visible");
            }
            else
            {
                Setting[2].Set("메틸메르캅탄", "CH3SH", "#FF85788F", "Black", Src_enable[0], "Black", "Black", "Hidden");
            }
            if (Ppm_list[3] == 0)
            {
                Setting[3].Set("일산화탄소", "CO", "#FFFFF8EF", "{x:Null}", Src_disable[1], "White", "White", "Hidden");
            }
            else if (Ppm_list[3] == 300)
            {
                Setting[3].Set("일산화탄소", "CO", "#FFC59F71", "Black", Src_enable[1], "Black", "#FF2C3FF3", "Visible");
            }
            else
            {
                Setting[3].Set("일산화탄소", "CO", "#FFC59F71", "Black", Src_enable[1], "Black", "Black", "Hidden");
            }
            if (Ppm_list[4] == 0)
            {
                Setting[4].Set("이산화탄소", "CO2", "#FFFBF0F1", "{x:Null}", Src_disable[1], "White", "White", "Hidden");
            }
            else if (Ppm_list[4] == 3000)
            {
                Setting[4].Set("이산화탄소", "CO2", "#FFA27076", "Black", Src_enable[1], "Black", "#FF2C3FF3", "Visible");
            }
            else
            {
                Setting[4].Set("이산화탄소", "CO2", "#FFA27076", "Black", Src_enable[1], "Black", "Black", "Hidden");
            }
            if (Ppm_list[5] == 0)
            {
                Setting[5].Set("메탄", "CH4", "#FFF3F0E9", "{x:Null}", Src_disable[1], "White", "White", "Hidden");
            }
            else if (Ppm_list[5] == 10000)
            {
                Setting[5].Set("메탄", "CH4", "#FF918262", "Black", Src_enable[1], "Black", "#FF2C3FF3", "Visible");
            }
            else
            {
                Setting[5].Set("메탄", "CH4", "#FF918262", "Black", Src_enable[1], "Black", "Black", "Hidden");
            }
        }
        public void Alert_Check()
        {
            Console.WriteLine("체크");

            //Alert Level 셋팅
            //Console.WriteLine("Alert Check");
            int idx = 0;
            int gas_idx = 0;
            int max_level = 1;
            foreach(double ppm in Ppm_list)
            {
                int level = 1;
                if (ppm >= Levels[idx].Yellow)
                {
                    level = 2;
                    gas_idx = idx;
                }
                if (ppm >= Levels[idx].Red)
                {
                    level = 3;
                    gas_idx = idx;
                }
                if (max_level<level)
                {
                    max_level = level;
                }
                idx = idx + 1;
            }
            //Alert Level에 따른 UI 셋팅
            Level = max_level;
            if (Level == 1)
            {
                Alert_set.Set("정상", "#FF43C743", 1, 40, 30, 30);
            }
            else
            {
                if (gas_idx == 0)
                {
                    Gas = "H2S";
                }
                else if (gas_idx == 1)
                {
                    Gas = "NH3";
                }
                else if (gas_idx == 2)
                {
                    Gas = "CH3SH";
                }
                else if (gas_idx == 3)
                {
                    Gas = "CO";
                }
                else if (gas_idx == 4)
                {
                    Gas = "CO2";
                }
                else if (gas_idx == 5)
                {
                    Gas = "CH4";
                }
                try
                {
                    DB log_db = new DB();
                    if (log_db.DBConn() == false)
                    {
                        Console.WriteLine("Log DB Error");
                        Messenger.Default.Send<int, MainViewModel>(1);
                        return;
                    }
                    else
                    {
                        Messenger.Default.Send<int, MainViewModel>(0);
                    }
                    string sql = "INSERT INTO alert_log (datetime,gas,ppm,level,location,dev_serial) " +
                        "VALUES ('"+ Date_time_str + "','"+Gas+"',"+Ppm_list[gas_idx].ToString()+","+Level+",'"+Dev.Local_name+"','"+Dev.Serial+"');";
                    log_db.update(sql);
                    log_db.Conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                //Messenger.Default.Send<bool, LogViewModel>(true);
                //new Log(Date_time, Gas, Ppm_list[gas_idx], Level, Dev.Local_name, Dev.Serial);
                if (Level == 2)
                {
                    Alert_set.Set("주의", "#FFDEC039", 3, 30, 40, 30);
                }
                else
                {
                    Alert_set.Set("위험", "#FFF30000", 5, 30, 30, 40);
                    Console.WriteLine("보냄");

                    Messenger.Default.Send<Log, MainViewModel>(new Log(Date_time, Gas, Ppm_list[gas_idx], Level, Dev.Local_name, Dev.Serial));
                }
            }
        }
        public void Pick()
        {
            Console.WriteLine("날짜 선택");
            if (string.Format("{0:yyyy-MM-dd}", Selected_date) == "0001-01-01") //자체 초기화 값 
            {
                return;
            }
            try
            {
                DB pick_db = new DB();
                if (pick_db.DBConn() == false)
                {
                    Console.WriteLine("Pick DB Error");
                    Messenger.Default.Send<int, MainViewModel>(1);
                    return;
                }
                else
                {
                    Messenger.Default.Send<int, MainViewModel>(0);
                }
                his_tmp = new ObservableCollection<DataPoint>();
                string sql = "SELECT regdate, ppm, ppm2, ppm3, ppm4, ppm5, ppm6 FROM result_info INNER JOIN gas_log ON result_info.log_idx = gas_log.idx " +
                    "WHERE gas_module_idx = '" + Dev.Serial + "' AND regdate LIKE '" + String.Format("{0:yyyy-MM-dd}", Selected_date) + "%';";
                MySqlDataReader rdr = pick_db.select(sql);
                while (rdr.Read())
                {
                    his_tmp.Add(new DataPoint(rdr.GetDateTime(0), rdr.GetDouble(1), rdr.GetDouble(2), rdr.GetDouble(3), rdr.GetDouble(4), rdr.GetDouble(5), rdr.GetDouble(6)));
                }
                rdr.Close(); ;
                pick_db.Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Gas_his = his_tmp; //얕은 복사 필요-한번에 교체
            Gas_his2 = his_tmp;
            Gas_his3 = his_tmp;
            Gas_his4 = his_tmp;
            Gas_his5 = his_tmp;
            Gas_his6 = his_tmp;
        }
        public void Pick_Term()
        {
            if (Selected_term == Term_list[0])
            {
                Measureunit = "Minute";
                Multi = "10";
            }
            else if (Selected_term == Term_list[1])
            {
                Measureunit = "Minute";
                Multi = "30";
            }
            else if (Selected_term == Term_list[2])
            {
                Measureunit = "Hour";
                Multi = "1";
            }
            else if (Selected_term == Term_list[3])
            {
                Measureunit = "Hour";
                Multi = "3";
            }
            else
            {
                Measureunit = "Hour";
                Multi = "6";
            }
        }
        public void Pick_Type()
        {
            Aggreate = Selected_type;
        }
        public void Timer_Tick(object sender, EventArgs e)
        {
            Max_date = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
        }
        //버튼 클릭 시 MainViewModel로 메신저 실행
        public void Click()
        {
            Messenger.Default.Send<string, MainViewModel>(Dev.Serial);
        }
        protected void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}