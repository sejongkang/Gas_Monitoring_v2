using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gas_Monitoring2.Models
{
    public class Mail
    {
        private MailAddress sendAddress = null;
        private MailAddress toAddress = null;
        private string sendPassword = Properties.Settings.Default.sendpw;

        public Mail()
        {
            this.sendAddress = new MailAddress(Properties.Settings.Default.sendaddress);
            this.toAddress = new MailAddress(Properties.Settings.Default.toaddress);
        }
        public void SetToAddress(string toMail)
        {
            this.toAddress = new MailAddress(toMail);
        }
        public string SendEmail(string subject, string body)
        {
            SmtpClient smtp = null;
            MailMessage message = null;
            try
            {
                smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(sendAddress.Address, sendPassword),
                    Timeout = 20000
                };
                message = new MailMessage(sendAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };
                smtp.Send(message);
                return "send mail ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "send mail fail";
            }
            finally
            {
                if (smtp != null) { smtp.Dispose(); }
                if (message != null) { message.Dispose(); }
            }
        }
    }
}
