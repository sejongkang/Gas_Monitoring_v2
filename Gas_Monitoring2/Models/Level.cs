using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gas_Monitoring2.Models
{
    public class Level : BindableBase
    {
        public string Gas { get; set; }
        public int Yellow { get; set; }
        public int Red { get; set; }
        public Level()
        {
            Gas = "None";
            Yellow = 15;
            Red = 30;
        }
        public Level(string gas)
        {
            Gas = gas;
            Yellow = 15;
            Red = 30;
        }
        public Level(string gas, int y, int r)
        {
            Gas = gas;
            Yellow = y;
            Red = r;
        }
    }
}
