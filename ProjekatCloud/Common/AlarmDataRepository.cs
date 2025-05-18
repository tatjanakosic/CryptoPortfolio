using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AlarmDataRepository
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public AlarmDataRepository()
        {
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AlarmConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("AlarmTable"); _table.CreateIfNotExists();
        }
        public IQueryable<Alarm> RetrieveAllAlarms()
        {
            var results = from g in _table.CreateQuery<Alarm>()
                          where g.PartitionKey == "Alarm"
                          select g;
            return results;
        }
        public void AddAlarm(Alarm newAlarm)
        {
            TableOperation insertOperation = TableOperation.Insert(newAlarm);
            _table.Execute(insertOperation);
        }

        public bool Exists(string indexNo)
        {
            return RetrieveAllAlarms().Where(s => s.RowKey == indexNo).FirstOrDefault() != null;
        }

        public void RemoveAlarm(string id)
        {
            Alarm user = RetrieveAllAlarms().Where(s => s.RowKey == id).FirstOrDefault();

            if (user != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(user);
                _table.Execute(deleteOperation);
            }
        }

        /*public User ExistsUser(string email, string password)
        {
            return RetrieveAllAlarms().Where(s => s.RowKey == email && s.Password == password).FirstOrDefault();

        }

        public void RemoveUser(string id)
        {
            User user = RetrieveAllAlarms().Where(s => s.RowKey == id).FirstOrDefault();

            if (user != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(user);
                _table.Execute(deleteOperation);
            }
        }

        public User GetUser(string index)
        {
            return RetrieveAllUsers().Where(p => p.RowKey == index).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            TableOperation updateOperation = TableOperation.Replace(user);
            _table.Execute(updateOperation);
        }*/
    }
}
