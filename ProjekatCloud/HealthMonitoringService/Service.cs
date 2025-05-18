using Common;
using System;
using System.ServiceModel;
using System.Threading;


namespace HealthMonitoringService
{
    public class Service
    {
        public static LogDataRepository repo;

        public Service()
        {
            repo = new LogDataRepository();
        }
        public void CheckStatus()
        {
            NetTcpBinding binding1 = new NetTcpBinding();

            EndpointAddress address1 = new EndpointAddress("net.tcp://localhost:8081/Input_WebRole");

            ChannelFactory<INotify> channelFactory1 = new ChannelFactory<INotify>(binding1, address1);

            INotify proxy1 = channelFactory1.CreateChannel();


            NetTcpBinding binding2 = new NetTcpBinding();

            EndpointAddress address2 = new EndpointAddress("net.tcp://localhost:4008/ISendEmail");

            ChannelFactory<ISendEmail> channelFactory2 = new ChannelFactory<ISendEmail>(binding2, address2);

            ISendEmail proxy2 = channelFactory2.CreateChannel();


            NetTcpBinding binding3 = new NetTcpBinding();

            EndpointAddress address3 = new EndpointAddress("net.tcp://localhost:10101/Input_WorkerRole");

            ChannelFactory<INotify> channelFactory3 = new ChannelFactory<INotify>(binding1, address1);

            INotify proxy3 = channelFactory3.CreateChannel();


            Random rand = new Random();
            Thread nit = new Thread(() =>

            {
                while (true)
                {
                    bool value = false;
                    bool value2 = false;
                    try
                    {
                         value = proxy1.Notify();
                         value2 = proxy3.Notify();
                    }
                    catch(Exception e)
                    {

                    }
                    /*try
                    {
                        bool value = proxy1.Notify();
                    }catch(Exception e)
                    {
                        proxy2.SendListOfEmails("Portfolio");
                        repo.AddLog(new Log(DateTime.Now.ToString() + "_NOT_OK") { LogText = DateTime.Now.ToString() + "_NOT_OK" });
                    }*/
                    /*try
                    {
                        bool value2 = proxy3.Notify();
                    }catch(Exception e)
                    {
                       proxy2.SendListOfEmails("Portfolio");
                       repo.AddLog(new Log(DateTime.Now.ToString() + "_NOT_OK") { LogText = DateTime.Now.ToString() + "_NOT_OK" });
                   }*/

                    if (value == false)
                    {
                        proxy2.SendListOfEmails("Portfolio");
                        repo.AddLog(new Log(DateTime.Now.ToString() + "_NOT_OK" ) { LogText = DateTime.Now.ToString() + "_NOT_OK" });
                    }
                    else if (value2 == false)
                    {
                        proxy2.SendListOfEmails("Notification");
                        repo.AddLog(new Log(DateTime.Now.ToString() + "_NOT_OK") { LogText = DateTime.Now.ToString() + "_NOT_OK" });
                    }
                    else
                    {
                       repo.AddLog(new Log(DateTime.Now.ToString() + "_OK") { LogText = DateTime.Now.ToString() + "_OK" });
                    }

                    Thread.Sleep(rand.Next(1000, 5000));
                }
            });
            nit.IsBackground = true;
            nit.Start();
        }
    }
}
