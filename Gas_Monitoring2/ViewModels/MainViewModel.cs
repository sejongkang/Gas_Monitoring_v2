using System;
using DevExpress.Mvvm;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Windows;
using System.ComponentModel;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Core;
using Gas_Monitoring2.Views;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;
using Gas_Monitoring2.Models;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.Generic;
using System.Media;
using System.Windows.Media;

namespace Gas_Monitoring2.ViewModels
{
    [POCOViewModel]
    public class MainViewModel : ViewModelBase
    {
        public virtual string Address { get; set; }
        public virtual string Loading_visible { get; set; }
        public virtual string Error { get; set; }
        public virtual DXTabItem Selected_tab { get; set; }
        public static DB Db { get; set; }
        public virtual ObservableCollection<DXTabItem> TabItems { get; set; }
        public static ObservableCollection<Level> Levels { get; set; }
        public static ObservableCollection<TabViewModel> Dev_list { get; set; }
        public virtual HomeView Home_view { get; set; }
        public virtual HomeViewModel Home_viewmodel { get; set; }
        public virtual IDocumentManagerService DocumentManagerService { get { return null; } }
        public virtual DelegateCommand<CancelEventArgs> HidingCommand { get; private set; }
        public virtual ObjectForScriptingHelper OpenTab { get; set; }
        public static string Is_alert { get; set; }
        public static string Is_mail { get; set; }
        public static int Level { get; set; }
        public MediaPlayer mediaPlayer { get; set; }
        public AlertViewModel Alert_vm { get; set; }
        Process[] procs = Process.GetProcessesByName("Gas_Monitoring2");
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        protected IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        [ComVisible(true)]
        public class ObjectForScriptingHelper
        {
            public void OpenTab(string serial)
            {
                Console.WriteLine("testtab");
                Messenger.Default.Send<string, MainViewModel>(serial);
            }
        }
        public MainViewModel()
        {
            Error = "Hidden";
            Dev_list = new ObservableCollection<TabViewModel>();
            //메신저 등록
            Messenger.Default.Register<string>(this, ClickMessage);
            Messenger.Default.Register<bool>(this, PopSetting);
            Messenger.Default.Register<int>(this, SetError);
            Messenger.Default.Register<Log>(this, PopAlert);

            //프로그램 실행 체크
            if (procs.Length > 1)
            {
                DXMessageBox.Show("프로그램이 이미 실행 중입니다.", "가스 모니터링 시스템 v2.0");
                System.Environment.Exit(0);
            }
            Loading_visible = "False";
            //DB 연결 체크
            DB_Init();
            //인터넷 연결 체크
            //Thread checknet_th = new Thread(new ThreadStart(CheckNet));
            //checknet_th.IsBackground = true;
            //checknet_th.Start();
            //Alert 기준 초기화
            Level_init();
            //디바이스 초기화
            Device_init();
            //지도 초기화
            //MapInit();
            //탭 초기화
            TabInit();
            //탭 종료 확인 함수 바인딩
            HidingCommand = new DelegateCommand<CancelEventArgs>(Hiding);
            OpenTab = new ObjectForScriptingHelper();

            Console.WriteLine("Main Finish");
        }
        public void MapInit()
        {
            Console.WriteLine("Map Init");
            Address = "http://localhost:30417/map.html";
        }
        public void DB_Init()
        {
            Db = new DB();
            if (Db.DBConn() == false)
            {
                Error = "Visible";
                //if (DXMessageBox.Show("DB에 연결할 수 없습니다. DB 서버 상태를 확인 해주세요.", "DB 접속 불가", MessageBoxButton.OK) == MessageBoxResult.OK)
                //{
                //System.Environment.Exit(0);
                //}
            }
            else
            {
                Error = "Hidden";
            }
        }
        public void TabInit()
        {
            Console.WriteLine("Tab Init");
            TabItems = new ObservableCollection<DXTabItem>();
            //홈 탭 생성
            Home_view = new HomeView();
            (Home_view.DataContext as HomeViewModel).Init();
            DXTabItem Tabitem = new DXTabItem();
            Tabitem.Header = "홈";
            Tabitem.Content = Home_view;
            Tabitem.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            TabItems.Add(Tabitem);
            Selected_tab = Tabitem;
        }
        public void CheckNet()
        {
            Console.WriteLine("Check");
            Ping pingSender = new Ping();
            while (true)
            {
                try
                {
                    PingReply reply = pingSender.Send(Properties.Settings.Default.address);
                    if (reply.Status == IPStatus.Success)
                    {
                        //Console.WriteLine("Ping Success");
                        Loading_visible = "False";
                    }
                    else
                    {
                        //Console.WriteLine("Ping Fail");
                        Loading_visible = "True";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Loading_visible = "True";
                    Console.WriteLine(Loading_visible);

                }
                Thread.Sleep(1000);
            }
        }
        public void Device_init()
        {
            foreach (TabViewModel vm in Dev_list)
            {
                vm.Should = false;
            }
            Dev_list.Clear();
            String sql = "SELECT * FROM gas_module ORDER BY idx;";
            MySqlDataReader rdr = Db.select(sql);
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    Dev_list.Add(new TabViewModel(new Device(rdr.GetInt16(0), rdr.GetString(1), rdr.GetString(4), String.Format("{0:yyyy-MM-dd}", rdr.GetDateTime(5)))));
                }
                Console.WriteLine("DeviceVM Init");
            }
        }
        public void Level_init()
        {
            Levels = new ObservableCollection<Level>();
            Levels.Add(new Level("H2S",Properties.Settings.Default.gas_yellow, Properties.Settings.Default.gas_red));
            Levels.Add(new Level("NH3",Properties.Settings.Default.gas2_yellow, Properties.Settings.Default.gas2_red));
            Levels.Add(new Level("CH3SH",Properties.Settings.Default.gas3_yellow, Properties.Settings.Default.gas3_red));
            Levels.Add(new Level("CO",Properties.Settings.Default.gas4_yellow, Properties.Settings.Default.gas4_red));
            Levels.Add(new Level("CO2",Properties.Settings.Default.gas5_yellow, Properties.Settings.Default.gas5_red));
            Levels.Add(new Level("CH4",Properties.Settings.Default.gas6_yellow, Properties.Settings.Default.gas6_red));
            Is_alert = Properties.Settings.Default.alert;
            Is_mail = Properties.Settings.Default.mail;
        }
        //SimpleView에서 버튼 클릭 시 수행
        public void ClickMessage(string serial)
        {
            Console.WriteLine("SimpleView Click");
            //기존에 생성된 탭 탐색
            for (int i = 0; i < TabItems.Count; i++)
            {
                DXTabItem tmp = TabItems[i];
                if (tmp.Header.ToString() != "홈")
                {
                    if (((tmp.Content as TabView).DataContext as TabViewModel).Dev.Serial == serial)
                    {
                        Selected_tab = tmp;
                        return;
                    }
                }
            }
            //새로운 탭 생성
            DXTabItem tabitem = new DXTabItem();
            TabView tabview = new TabView();
            foreach (TabViewModel vm in Dev_list)
            {
                if(vm.Dev.Serial == serial)
                {
                    tabview.DataContext = vm;
                    tabitem.Content = tabview;
                    tabitem.Header = "디바이스" + vm.Dev.Dev_num.ToString();
                }
            }
            TabItems.Add(tabitem);
            Selected_tab = tabitem;
        }
        public void PopSetting(bool bang)
        {
            //ObservableCollection<Level> before_lvs = Levels;
            //DB before_db = new DB(Db.Address, Db.Port, Db.Id, Db.Pw);
            //bool is_change = false;
            try
            {
                SettingViewModel setting_vm = new SettingViewModel();

                UICommand okCommand = new UICommand()
                {
                    Caption = "OK",
                    IsCancel = false,
                    IsDefault = true
                };

                UICommand cancelCommand = new UICommand()
                {
                    Id = MessageBoxResult.Cancel,
                    Caption = "Cancel",
                    IsCancel = true,
                    IsDefault = false,
                };

                IDialogService service = this.GetService<IDialogService>("service1");

                UICommand result = service.ShowDialog(
                    dialogCommands: new List<UICommand>() { okCommand, cancelCommand },
                    title: "Setting",
                    viewModel: setting_vm);
                //셋팅 다이알로그에서 설정한 값들로 Setting 변수 저장
                if (result == okCommand)
                {
                    Properties.Settings.Default.gas_yellow = Levels[0].Yellow;
                    Properties.Settings.Default.gas_red = Levels[0].Red;
                    Properties.Settings.Default.gas2_yellow = Levels[1].Yellow;
                    Properties.Settings.Default.gas2_red = Levels[1].Red;
                    Properties.Settings.Default.gas3_yellow = Levels[2].Yellow;
                    Properties.Settings.Default.gas3_red = Levels[2].Red;
                    Properties.Settings.Default.gas4_yellow = Levels[3].Yellow;
                    Properties.Settings.Default.gas4_red = Levels[3].Red;
                    Properties.Settings.Default.gas5_yellow = Levels[4].Yellow;
                    Properties.Settings.Default.gas5_red = Levels[4].Red;
                    Properties.Settings.Default.gas6_yellow = Levels[5].Yellow;
                    Properties.Settings.Default.gas6_red = Levels[5].Red;
                    Properties.Settings.Default.address = Db.Address;
                    Properties.Settings.Default.port = Db.Port;
                    Properties.Settings.Default.id = Db.Id;
                    Properties.Settings.Default.pw = Db.Pw;
                    Properties.Settings.Default.alert = setting_vm.Ischeck_pop;
                    Properties.Settings.Default.mail = setting_vm.Ischeck_mail;
                    Properties.Settings.Default.toaddress = setting_vm.ToAdress;
                    Properties.Settings.Default.Save();

                    Level_init();
                    DB_Init();
                    Device_init();
                    TabInit();
                    //for (int i = 0; i < Levels.Count; i++)
                    //{
                    //    if ((before_lvs[i].Yellow != Levels[i].Yellow) ||
                    //        (before_lvs[i].Red != Levels[i].Red))
                    //    {
                    //        is_change = true;
                    //    }
                    //}
                    //if ((before_db.Address != Db.Address) || 
                    //    (before_db.Port != Db.Port) ||
                    //    (before_db.Id != Db.Id) ||
                    //    (before_db.Pw != Db.Pw))
                    //{
                    //    is_change = true;
                    //}
                    //if(is_change)
                    //{
                    //    DXMessageBox.Show("DB 연결 속성값이 변경 되었습니다.", "가스 모니터링 시스템 v2.0");
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void PopAlert(Log log)
        {
            Console.WriteLine("Alert");
            if (Is_alert == "True")
            {
                mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri("siren.wav", UriKind.Relative));
                mediaPlayer.Play();
                try
                {
                    IDialogService service = this.GetService<IDialogService>("service2");
                    DispatcherService.BeginInvoke(() => service.ShowDialog(
                                        dialogButtons: MessageButton.OK,
                                        title: "Alert",
                                        viewModel: new AlertViewModel(log)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (Is_mail == "True")
            { 
                Mail mail = new Mail();
                string subject = "<가스 경보 발생>";
                string body = "디바이스 : "+log.Serial+ Environment.NewLine +
                    "장소 : "+log.Location+ Environment.NewLine + Environment.NewLine +
                    "가스 : " +log.Gas+ Environment.NewLine +
                    "농도 : "+log.Ppm+ Environment.NewLine + Environment.NewLine +
                    "날짜 : " +log.Date+ Environment.NewLine +
                    "시간 : "+log.Time;
                Console.WriteLine(mail.SendEmail(subject, body));
            }
        }
        //종료 확인
        public void Hiding(CancelEventArgs e)
        {
            if (DXMessageBox.Show(Application.Current.MainWindow, "탭을 닫으시겠습니까?", "탭 닫기 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        //도움말 열기
        public void PopHelp()
        {
            try
            {
                Console.WriteLine("Help Click");
                System.Diagnostics.Process.Start(Path.GetFullPath("../../help.html"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        //DB 에러 표시
        public void SetError(int i)
        {
            if(i == 1)
            {
                Error = "Visible";
            }
            else
            {
                Error = "Hidden";
            }
        }
    }
}