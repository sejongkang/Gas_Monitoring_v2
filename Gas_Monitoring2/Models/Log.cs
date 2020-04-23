using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gas_Monitoring2.Models
{
    public class Log:BindableBase
    {
        public virtual string Date { get; set; }
        public virtual string Time { get; set; }
        public virtual string Gas { get; set; }
        public virtual double Ppm { get; set; }
        public virtual int Level { get; set; }
        public virtual string Location { get; set; }
        public virtual string Serial { get; set; }
        public Log()
        {
        }
        public Log(DateTime dateTime, string gas, double ppm, int level, string location, string serial)
        {
            Date = String.Format("{0:yyyy-MM-dd}", dateTime);
            Time = String.Format("{0:HH:mm:ss}", dateTime);
            Gas = gas;
            Ppm = ppm;
            Level = level;
            Location = location;
            Serial = serial;
        }
    }
}
