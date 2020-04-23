using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Gas_Monitoring2.Models;

namespace Gas_Monitoring2.ViewModels
{
    [POCOViewModel]
    public class LogItemViewModel
    {
        public virtual string Level { get; set; }
        public virtual string Forg { get; set; }
        public virtual Log Item_log { get; set; }
        public virtual DelegateCommand ButtonCommand { get; set; }
        public LogItemViewModel()
        {
        }
        public LogItemViewModel(Log log)
        {
            Item_log = log;
            Forg = "Black";
            Level = "정상";

            ButtonCommand = new DelegateCommand(Click);

            if (Item_log.Level == 2)
            {
                Forg = "#FFF1A012";
                Level = "주의";
            }
            else if(Item_log.Level == 3)
            {
                Forg = "Red";
                Level = "위험";
            }
        }
        public void Click()
        {
            Messenger.Default.Send<string, MainViewModel>(Item_log.Serial);
        }
    }
}