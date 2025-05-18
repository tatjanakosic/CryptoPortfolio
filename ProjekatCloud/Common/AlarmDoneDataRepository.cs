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
    public class AlarmDoneDataRepository 
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public AlarmDoneDataRepository()
        {
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AlarmDoneConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("AlarmDoneTable"); _table.CreateIfNotExists();
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

        public Alarm GetAlarm(string index, string email)
        {
            return RetrieveAllAlarms().Where(p => p.RowKey == index && p.Email == email).FirstOrDefault();
        }

        public List<Alarm> GetAlarmEmail(string email)
        {
            return (List<Alarm>)(RetrieveAllAlarms().Where(p => p.Email == email).ToList());
        }
    }
}
