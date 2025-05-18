using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Crypto : TableEntity
    {
        public string Email { get; set; }
        public string Currency { get; set; }
        public double StartValue { get; set; }      //kupovna
        public double EndValue { get; set; }        //prodajna
        public double Percentage { get; set; }
        public int Quantity { get; set; }
        public DateTime datumIVrijeme { get; set; }

        public string BuyOrSell { get; set; }

        public Crypto(String indexNo)
        {
            PartitionKey = "Crypto";
            RowKey = indexNo;
        }
        public Crypto() { }
    }
}
