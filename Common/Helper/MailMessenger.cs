using Common.HelperContract;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common.Helper
{
    public class MailMessenger : IMailMessenger
    {
        #region Declarations
        private SmtpClient MailClient{
            get{
                return new SmtpClient{
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("info.Hospitalia247@yahoo.com", "ihoyfkkzhzxthdid"),
                    Port = 587,
                    Host = "smtp.mail.yahoo.com",
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };
            }
        }
        #endregion
        #region Implementation
        public Task SendMail(string targetMail, string Title, string Message){
            var message = new MailMessage{
                From = new MailAddress("info.Hospitalia247@yahoo.com", @"Floward Announcement", Encoding.Default),
                Subject = Title,
                Body = Message,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                Priority = MailPriority.High,
            };
            message.To.Add(targetMail);
            return (MailClient.SendMailAsync(message));
        } 
        #endregion
    }
}
