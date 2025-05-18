using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Alarm : TableEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Currency { get; set; }
        public double AlarmValue { get; set; }   
       
        public Alarm()
        {
            Id = Guid.NewGuid();
            PartitionKey = "Alarm";
            RowKey = Id.ToString();
        }
    }
}
