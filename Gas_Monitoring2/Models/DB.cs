using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using MySql.Data.MySqlClient;

namespace Gas_Monitoring2.Models
{
    public class DB : BindableBase
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }
        public string StrConn { get; set; }
        public string Schema { get; set; }
        public MySqlConnection Conn { get; set; }

        public DB()
        {
            Address = Properties.Settings.Default.address;
            Port = Properties.Settings.Default.port;
            Id = Properties.Settings.Default.id;
            Pw = Properties.Settings.Default.pw;

            //Address = "203.250.78.169";
            //Port = 3307;
            //Id = "root";
            //Pw = "offset01";

            Schema = "gas_v2";
            StrConn = "Server=" + Address + ";Port=" + Port + ";User ID=" + Id + ";Password=" + Pw + ";Database=" + Schema + ";Min Pool Size=10;";
        }

        public DB(string address, int port, string id, string pw)
        {
            Address = address;
            Port = port;
            Id = id;
            Pw = pw;
            Schema = "gas_v2";
            StrConn = "Server=" + Address + ";User ID=" + Id + ";Password=" + Pw + ";Database=" + Schema + ";Min Pool Size=10;";
        }
        public void Reset()
        {
            //Address = "192.168.0.157";
            //port = 3306;
            //id = "root";
            //pw = "ubimicro";

            //Address = "192.168.2.2";
            //Port = 3306;
            //Id = "root";
            //Pw = "ubimicro";

            //Address = "127.0.0.1";
            //Port = 3307;
            //Id = "root";
            //Pw = "offset01";

            //Address = "106.252.240.216";
            //Port = 23306;
            //Id = "root";
            //Pw = "ubimicro";

            Address = "203.250.78.168";
            Port = 3307;
            Id = "root";
            Pw = "offset01";

            Schema = "gas_v2";
            StrConn = "Server=" + Address + ";Port=" + Port + ";User ID=" + Id + ";Password=" + Pw + ";Database=" + Schema + ";Min Pool Size=10;";
        }
        public bool DBConn()
        {
            try
            {
                Conn = new MySqlConnection(StrConn);
                Conn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("DB Connect Fail");
                return false;
            }
        }
        public MySqlDataReader select(string sql)
        {
            MySqlDataReader rdr = null;
            try
            {
                var cmd = new MySqlCommand(sql, Conn);
                rdr = cmd.ExecuteReader();
                return rdr;
            }
            catch(Exception e)
            {
                Console.WriteLine("DB Connect Fail");
                return rdr;
            }
        }
        public void update(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            cmd.ExecuteNonQuery();
        }
    }
}
