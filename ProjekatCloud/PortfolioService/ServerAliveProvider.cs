using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioService
{
    public class ServerAliveProvider : INotify
    {
        public bool Notify()
        {
            Random rand = new Random();
            int broj = rand.Next(1, 100);

            if (broj % 10 == 0)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
    }
}