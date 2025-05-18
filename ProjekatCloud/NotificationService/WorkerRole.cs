using Common;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService
{
    public class WorkerRole : RoleEntryPoint
    {
        private AlarmDataRepository alarmstable = new AlarmDataRepository();
        private AlarmDoneDataRepository alarmdonestable = new AlarmDoneDataRepository();
        private CryptoDataRepository cryptotable = new CryptoDataRepository();
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("NotificationService is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Use TLS 1.2 for Service Bus connections
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.
            //Kreira se posebna nit za proveru da li je server dostupan 
            Thread nit = new Thread(() =>

            {
                ServerAlive serverr = new ServerAlive();

                serverr.JobServer();

                serverr.Open();

            });
            nit.IsBackground = true;
            nit.Start();

            bool result = base.OnStart();

            Trace.TraceInformation("NotificationService has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("NotificationService is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("NotificationService has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            CloudQueue queue = QueueHelper.GetQueueReference("vezba");
            int brojac = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                try
                {
                    List<PriceInfo> prices = null;
                    IQueryable<Alarm> alarms = alarmstable.RetrieveAllAlarms();
                   
                   
                    alarms = alarms.Take(20);

                    List<Alarm> alarmi = new List<Alarm>();
                    foreach(var a in alarms)
                    {
                        alarmi.Add(a);
                    }
                    
                    if (alarmi.Count() != 0)
                    {
                        prices = await cryptotable.GetTickerAllAsync();
                        foreach (Alarm a in alarms)
                        {
                            if (a.AlarmValue <= (prices.Where(p => p.Symbol == a.Currency)).FirstOrDefault().Price)
                            {
                                Send(a.Currency, a.Email);
                                brojac++;
                                //await queue.AddMessageAsync(new CloudQueueMessage(a.Id.ToString()));
                                queue.AddMessage(new CloudQueueMessage(a.Id.ToString()), null, TimeSpan.FromMilliseconds(30));
                                alarmstable.RemoveAlarm(a.Id.ToString());
                                alarmdonestable.AddAlarm(a);
                                Trace.TraceInformation(DateTime.Now.ToString() + " " + a.Id.ToString() + " " + brojac.ToString());
                            }
                        }
                    }
                   
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                await Task.Delay(10000);
            }
        }

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
                string subject = "Dostignut alarm " + parameter;
                string body = "CLOUD: Vrednost dostignuta.";
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
    }
}
