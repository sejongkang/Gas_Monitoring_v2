using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Gas_Monitoring2.Models;
using System.IO;

namespace Gas_Monitoring2.ViewModels
{
    [POCOViewModel]
    public class AlertViewModel
    {
        public virtual Log Alert_log { get; set; }
        public virtual string Content_src { get; set; }
        public virtual string Exposure_src { get; set; }
        public virtual string Emergency_src { get; set; }
        public virtual DelegateCommand ButtonCommand { get; set; }
        public AlertViewModel()
        {
        }
        public AlertViewModel(Log log)
        {
            Console.WriteLine("alert ctor2");
            ButtonCommand = new DelegateCommand(Click);
            Alert_log = log;
            Content_src = Path.GetFullPath("../../Images/"+Alert_log.Gas+"/content.png");
            Exposure_src = Path.GetFullPath("../../Images/" + Alert_log.Gas + "/exposure.png");
            Emergency_src = Path.GetFullPath("../../Images/" + Alert_log.Gas + "/emergency.png");
        }
        public void Click()
        {
            Console.WriteLine("Clicked");
        }
    }
}