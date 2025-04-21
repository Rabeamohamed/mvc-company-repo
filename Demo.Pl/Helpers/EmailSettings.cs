using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.Pl.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("zarzora.rabea@gmail.com", "qerznsohtkbjprvu");
            //فاضل جزء ال 2 Step Verification اعمله  Turn on بس يكون النت شغال
            Client.Send("zarzora.rabea@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
