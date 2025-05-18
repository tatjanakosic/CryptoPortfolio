using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Log : TableEntity
    {
        public String LogText { get; set; }

        public Log(String indexNo)
        {
            PartitionKey = "Log";
            RowKey = indexNo;
        }
        public Log() { }
    }
}
