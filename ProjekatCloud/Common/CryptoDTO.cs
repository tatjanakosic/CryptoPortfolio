using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CryptoDTO
    {
        public string Currency { get; set; }
        public double Price { get; set; }

        public DateTime Date { get; set; }

        //public int Quantity { get; set; }

        public string Type { get; set; }
    }
}
