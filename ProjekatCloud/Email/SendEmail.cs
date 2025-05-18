using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Email
{
    public class SendEmail : ISendEmail
    {
        const string emailFilePath = "emails.txt";
        const string userFilePath = "users.txt";


        public void Send(string parameter, string email)
        {
            //PROMIJENI NA EMAILFROM I EMAILTO
            try
            {
                string fromEmail = "drslastnamedrsfirstname@gmail.com";
                string password = "dqxv gnrh ktff xxym";

                // SMTP klijent
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true,
                };
                string subject = "Verifikacija " + parameter;
                string body = "CLOUD: Poruka nije primljena.";
                MailMessage mailMessage = new MailMessage(fromEmail, "drslastnamedrsfirstname@gmail.com", subject, body)
                {
                    IsBodyHtml = true 
                };
                /*MailMessage mailMessage = new MailMessage(fromEmail, email, subject, body)
                {
                    IsBodyHtml = true
                };*/

                // Slanje emaila
                smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri slanju emaila: {ex.Message}");
                throw;
            }
        }

        public List<string> LoadEmails()
        {
            if (!File.Exists(emailFilePath))
            {
                return new List<string>();
            }

            return File.ReadAllLines(emailFilePath).ToList();
        }

        public void SendListOfEmails(string param)
        {
            List<string> list = new List<string>();
            list = LoadEmails();

            foreach(string s in list)
            {
                Send(param, s);
            }
        }


    }
}
