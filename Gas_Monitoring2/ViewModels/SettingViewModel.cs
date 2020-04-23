using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Gas_Monitoring2.Models;
using System.ComponentModel;

namespace Gas_Monitoring2.ViewModels
{
    [POCOViewModel]
    public class SettingViewModel
    {
        public virtual ObservableCollection<Level> Set_Levels { get; set; }
        public virtual DB Set_db { get; set; }
        public virtual string Ischeck_pop { get; set; }
        public virtual string Ischeck_mail { get; set; }
        public virtual string Enable_mail { get; set; }
        public virtual string ToAdress { get; set; }
        public DelegateCommand AlertResetCommand { get; set; }
        public DelegateCommand DBResetCommand { get; set; }


        public SettingViewModel()
        {
            Console.WriteLine("Setting ctor2");
            AlertResetCommand = new DelegateCommand(AlertReset);
            DBResetCommand = new DelegateCommand(DBReset);
            Ischeck_pop = MainViewModel.Is_alert;
            Ischeck_mail = MainViewModel.Is_mail;
            Console.WriteLine(Ischeck_pop);
            Console.WriteLine(Ischeck_mail);
            Set_Levels = MainViewModel.Levels;
            Set_db = MainViewModel.Db;
            ToAdress = Properties.Settings.Default.toaddress;
        }
        //경보 기준 초기화 버튼
        public void AlertReset()
        {
            Console.WriteLine("Alert_reset");
            Set_Levels.Clear();
            Set_Levels.Add(new Level("H2S",10,15));
            Set_Levels.Add(new Level("NH3",25,35));
            Set_Levels.Add(new Level("CH3SH",5,10));
            Set_Levels.Add(new Level("CO",30,200));
            Set_Levels.Add(new Level("CO2",5000,30000));
            Set_Levels.Add(new Level("CH4",10000,50000));
            //ToAdress = "sejong055@gmail.com";
        }
        //DB 정보 초기화 버튼
        public void DBReset()
        {
            Console.WriteLine("DB_reset");
            Set_db.Reset();
        }
    }
}