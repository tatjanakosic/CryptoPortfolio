using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioService.Models
{
    public class SetContext
    {
        public void Set(User u)
        {
            HttpContext.Current.Application["user"] = u;
        }
    }
}