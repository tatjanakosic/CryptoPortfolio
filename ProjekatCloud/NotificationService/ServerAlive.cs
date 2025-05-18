using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService
{
    public class ServerAlive
    {
        private ServiceHost serviceHost;

        private string externalEndpointName = "Input_WorkerRole";

        public void JobServer()
        {
            /*NetTcpBinding binding = new NetTcpBinding();

            RoleInstanceEndpoint roleInstance = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[externalEndpointName];
            
            serviceHost = new ServiceHost(typeof(ServerAliveProvider));

            serviceHost.AddServiceEndpoint(typeof(INotify), binding, String.Format("net.tcp://{0}/{1}", roleInstance, externalEndpointName));*/

            RoleInstanceEndpoint inputEndPoint = RoleEnvironment.
            CurrentRoleInstance.InstanceEndpoints[externalEndpointName];
            string endpoint = string.Format("net.tcp://{0}/{1}", inputEndPoint.IPEndpoint,
            externalEndpointName);
            serviceHost = new ServiceHost(typeof(ServerAliveProvider));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(INotify), binding, endpoint);

        }

        public void Open()
        {
            try
            {
                serviceHost.Open();

                Trace.TraceInformation(String.Format("Host for {0} endpoint type opened successfully at {1}", externalEndpointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host open error for {0} endpoint type. Error message is: {1}. ", externalEndpointName, e.Message);
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();

                Trace.TraceInformation(String.Format("Host for {0} endpoint type closed successfully at {1}", externalEndpointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host close error for {0} endpoint type. Error message is: {1}. ", externalEndpointName, e.Message);
            }
        }
    }
}
