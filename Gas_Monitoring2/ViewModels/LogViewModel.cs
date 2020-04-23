using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Gas_Monitoring2.Views;
using Gas_Monitoring2.Models;
using MySql.Data.MySqlClient;

namespace Gas_Monitoring2.ViewModels
{
    [POCOViewModel]
    public class LogViewModel
    {
        public virtual DateTime Start_date { get; set; }
        public virtual DateTime End_date { get; set; }
        public virtual DateTime Min_date { get; set; }
        public virtual DateTime Max_date { get; set; }
        public virtual ObservableCollection<LogItemView> Items { get; set; }
        public virtual DelegateCommand PickCommand { get; set; }
        public virtual DelegateCommand LoadCommand { get; set; }
        public virtual ObservableCollection<String> Num_list { get; set; }
        public virtual ObservableCollection<String> Serial_list { get; set; }
        public virtual ObservableCollection<String> Gas_list { get; set; }
        public virtual ObservableCollection<String> Lvl_list { get; set; }
        public virtual string Selected_num { get; set; }
        public virtual string Selected_serial { get; set; }
        public virtual string Selected_gas { get; set; }
        public virtual string Selected_lvl { get; set; }

        public LogViewModel()
        {
            //Messenger.Default.Register<bool>(this, Add);
            PickCommand = new DelegateCommand(Pick);
            LoadCommand = new DelegateCommand(Load);
            Start_date = DateTime.Now;
            End_date = DateTime.Now;
            Min_date = DateTime.Now.AddDays(-1);
            Max_date = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
            Num_list = new ObservableCollection<string> {"1000", "500", "200", "100"};
            Serial_list = new ObservableCollection<string> { "전체" };
            Gas_list = new ObservableCollection<string> { "전체", "H2S", "NH3", "CH3SH", "CO", "CO2", "CH4" };
            Lvl_list = new ObservableCollection<string> { "전체", "경고", "위험" };
            Selected_num = Num_list[0];
            Selected_serial = Serial_list[0];
            Selected_gas = Gas_list[0];
            Selected_lvl = Lvl_list[0];
            foreach (TabViewModel vm in MainViewModel.Dev_list)
            {
                Serial_list.Add(vm.Dev.Serial);
            }
            
            Load();
        }
        public void Pick()
        {
            if (string.Format("{0:yyyy-MM-dd}", Start_date) == "0001-01-01") //자체 초기화 값 
            {
                return;
            }
            else
            {
                Min_date = Start_date;
            }
        }
        public void Load()
        {
            string col = "%";
            string serial = "%";
            string lvl = "%";
            try
            {
                DB log_db = new DB();
                if (log_db.DBConn() == false)
                {
                    Console.WriteLine("Pick DB Error");
                    Messenger.Default.Send<int, MainViewModel>(1);
                    return;
                }
                else
                {
                    Messenger.Default.Send<int, MainViewModel>(0);
                }
                Items = new ObservableCollection<LogItemView>();
                if (Selected_gas != "전체")
                {
                    col = Selected_gas;
                }
                if (Selected_serial != "전체")
                {
                    serial = Selected_serial;
                }
                if (Selected_lvl == "경고")
                {
                    lvl = "2";
                }
                else if (Selected_lvl == "위험")
                {
                    lvl = "3";
                }
                string sql = "SELECT * FROM alert_log WHERE (gas like '"+col+"') and (dev_serial like '"+ serial + "') and (level like '"+lvl+"') and (datetime between '" + String.Format("{0:yyyy-MM-dd}", Start_date) + "' and '" + String.Format("{0:yyyy-MM-dd}", End_date.AddDays(1)) + "') limit "+ Selected_num + ";";
                MySqlDataReader rdr = log_db.select(sql);
                while (rdr.Read())
                {
                    LogItemView Item = new LogItemView();
                    Item.DataContext = new LogItemViewModel(new Log(rdr.GetDateTime(1), rdr.GetString(2), rdr.GetDouble(3), rdr.GetInt32(4), rdr.GetString(5), rdr.GetString(6)));
                    Items.Add(Item);
                }
                rdr.Close(); ;
                log_db.Conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}