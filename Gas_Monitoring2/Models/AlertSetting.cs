using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gas_Monitoring2.Models
{
    public class AlertSetting : BindableBase
    {
        private string txt;
        private string back;
        private int level;
        private int g_thick;
        private int y_thick;
        private int r_thick;

        public string Txt { get => txt; set => txt = value; }
        public string Back { get => back; set => back = value; }
        public int Level { get => level; set => level = value; }
        public int G_thick { get => g_thick; set => g_thick = value; }
        public int Y_thick { get => y_thick; set => y_thick = value; }
        public int R_thick { get => r_thick; set => r_thick = value; }
        public AlertSetting()
        {
            Txt = "정상";
            Back = "#FF2FDC1E";
            Level = 1;
            G_thick = 40;
            Y_thick = 30;
            R_thick = 30;
        }
        public void Set(string txt, string back, int level, int g, int y, int r )
        {
            Txt = txt;
            Back = back;
            Level = level;
            G_thick = g;
            Y_thick = y;
            R_thick = r;
        }
    }
}
