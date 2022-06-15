using System.Net.Mail;

namespace ETicaret.BusinessLayer.Email_Entegrasyon
{
    public  class MailSend
    {
        public static void SendMail(string sendmailadress, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("support@giyimover.com", "1425369As");
            client.Port = 587;
            client.Host = "mail.giyimover.com";
            client.EnableSsl = false;
            mail.To.Add(sendmailadress);
            mail.From = new MailAddress("support@giyimover.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            client.Send(mail);
        }
    }
}
