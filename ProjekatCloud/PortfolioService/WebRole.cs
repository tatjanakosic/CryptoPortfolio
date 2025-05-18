using Common;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Queue;
using PortfolioService.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PortfolioService
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.
            Thread nit = new Thread(() =>

            {
                ServerAlive serverr = new ServerAlive();

                serverr.JobServer();

                serverr.Open();

            });
            nit.IsBackground = true;
            nit.Start();
            //Run();

            return base.OnStart();
        }

        //public override void Run()
        //{
        //    CloudQueue queue = QueueHelper.GetQueueReference("vezba");
        //    // This is a sample worker implementation. Replace with your logic.
        //    Trace.TraceInformation("ImageConverter_WorkerRole entry point called", "Information");

        //    while (true)
        //    {
        //        CloudQueueMessage message = queue.GetMessage();
        //        if (message == null)
        //        {
        //            Trace.TraceInformation("Trenutno ne postoji poruka u redu.", "Information");
        //        }
        //        else
        //        {
        //            if(CryptoController.AlarmDoneList == null)
        //            {
        //                CryptoController.AlarmDoneList = new List<string>();
        //            }
        //            Trace.TraceInformation(String.Format("Poruka glasi: {0}", message.AsString), "Information");
        //            CryptoController.AlarmDoneList.Add(message.ToString());
        //            queue.DeleteMessage(message);
        //            Trace.TraceInformation(String.Format("Poruka procesuirana: {0}", message.AsString), "Information");
        //        }

        //        Thread.Sleep(15000);
        //        Trace.TraceInformation("Working", "Information");
        //    }

        //}


    }
}
