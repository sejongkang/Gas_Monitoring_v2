using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gas_Monitoring2.Models
{
    public class Device: BindableBase
    {
        private int dev_num;
        private string serial;
        private string local_name;
        private string dev_purch_date;
        private bool enable;
        private string conn;

        public string Serial { get => serial; set => serial = value; }
        public string Local_name { get => local_name; set => local_name = value; }
        public string Dev_purch_date { get => dev_purch_date; set => dev_purch_date = value; }
        public bool Enable { get => enable; set => enable = value; }
        public string Conn { get => conn; set => conn = value; }
        public int Dev_num { get => dev_num; set => dev_num = value; }
        
        public Device(int dev_num, string serial, string local_name, string dev_purch_date, string conn="Off")
        {
            Dev_num = dev_num;
            Serial = serial;
            Local_name = local_name;
            Dev_purch_date = dev_purch_date;
            Enable = enable;
            Conn = conn;
        }
    }
}
