using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthStatusService.Controllers
{
    public class HomeController : Controller
    {
        public static LogDataRepository repodata = new LogDataRepository();
        public ActionResult Index()
        {
            IQueryable<Log> list =  repodata.RetrieveAllUsers();
            Dictionary<string, string> logs = new Dictionary<string, string>(); 
            foreach(Log l in list)
            {
                string[] ok = l.LogText.Split('_');
                logs.Add(ok[0], ok[1]);
            }
            
            DateTime parsedDate;

            Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>(); //kljuc je sat, razlika izmedju sada i vremena upisanog u bazu,
                                                                                      //vrednost je lista koja ima 100 ili 0, 100 ako je ok, 0 ako je not ok
            for(int i = 0; i < 24; i++)
            {
                dictionary[i] = new List<int>();
            }
            foreach (KeyValuePair<string, string> dateString in logs)
            {
                if (DateTime.TryParseExact(dateString.Key, "d.M.yyyy. HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    DateTime now = DateTime.Now;
                    ViewBag.now = now.Hour;

                    if (parsedDate >= now.AddHours(-24) && parsedDate <= now)
                    {
                        System.TimeSpan d = now - parsedDate;
                        int hour = Convert.ToInt32(Math.Floor(d.TotalHours));
                        //if (hour >= 0 && hour < 24)
                        //{
                            dictionary[23 - hour].Add(dateString.Value == "OK" ? 100 : 0);
                        //}
                        Console.WriteLine("Datum je unutar poslednja 24 sata.");
                    }
                    else
                    {
                        Console.WriteLine("Datum nije unutar poslednja 24 sata.");
                    }
                }
                else
                {
                    Console.WriteLine("Neuspešno parsiranje datuma.");
                }
            }

            List<double> percentage = new List<double>();
            foreach(var i in dictionary.Values)
            {
                if (i.Count == 0)
                {
                    percentage.Add(0);
                }
                else
                {
                    percentage.Add(i.Average());
                }
            }

            ViewBag.Percentages = percentage;
            return View();
        }

        public ActionResult About()
        {
            IQueryable<Log> list = repodata.RetrieveAllUsers();
            Dictionary<string, string> logs = new Dictionary<string, string>();  // datum i vrijeme, drugo OK/NOT_OK
            foreach (Log l in list)
            {
                string[] ok = l.LogText.Split('_');
                logs.Add(ok[0], ok[1]);
            }

            DateTime parsedDate;
            Dictionary<string, string> logs2 = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> dateString in logs)
            {
                if (DateTime.TryParseExact(dateString.Key, "d.M.yyyy. HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    DateTime now = DateTime.Now;
                    ViewBag.now = now.Hour;
                    if (parsedDate >= now.AddHours(-1) && parsedDate <= now)
                    {
                        logs2[dateString.Key]=dateString.Value;
                    }
                    else
                    {
                        Console.WriteLine("Datum nije unutar poslednja 24 sata.");
                    }
                }
                else
                {
                    Console.WriteLine("Neuspešno parsiranje datuma.");
                }
            }
            List<int> percentage = new List<int>();
            int x = 0;
            int y = 0;
            foreach (var i in logs2)
            {
                if (i.Value == "OK")
                {
                    x++;
                }
                else {
                    y++;
                }
                //percentage.Add($"{i.Key}_{i.Value}");
            }
            percentage.Add(x);
            percentage.Add(y);

            ViewBag.Percentages = percentage;


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}