using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;

namespace Common
{
    public class LogDataRepository
    {
        private static CloudStorageAccount _storageAccount;
        private static CloudTable _table;
        public LogDataRepository()
        {
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("LogConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("LogTextTable");
            _table.CreateIfNotExists();
        }
        public IQueryable<Log> RetrieveAllUsers()
        {
            var results = from g in _table.CreateQuery<Log>()
                          where g.PartitionKey == "Log"
                          select g;
            return results;
        }
        public void AddLog(Log newLog)
        { 
            TableOperation insertOperation = TableOperation.Insert(newLog);
            _table.Execute(insertOperation);
        }

        /*public bool Exists(string indexNo)
        {
            return RetrieveAllUsers().Where(s => s.RowKey == indexNo).FirstOrDefault() != null;
        }

        public User ExistsUser(string email, string password)
        {
            return RetrieveAllUsers().Where(s => s.RowKey == email && s.Password == password).FirstOrDefault();

        }

        public void RemoveUser(string id)
        {
            User user = RetrieveAllUsers().Where(s => s.RowKey == id).FirstOrDefault();

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
