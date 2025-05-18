using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ISendEmail
    {
        //[OperationContract]
        //void Send(string parameter, string email);

        [OperationContract]
        void SendListOfEmails(string param);
    }
}
