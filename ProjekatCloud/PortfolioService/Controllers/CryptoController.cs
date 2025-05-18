using Common;
using Microsoft.Ajax.Utilities;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortfolioService.Controllers
{
    public class CryptoController : Controller
    {
        // GET: Crypo
        CryptoDataRepository client = new CryptoDataRepository();
        AlarmDataRepository alarms = new AlarmDataRepository();
        AlarmDoneDataRepository alarmdone = new AlarmDoneDataRepository();
        public async Task<ActionResult> Index()
        {
            //Dictionary<string, double> dictionary = await client.GetTickerAsync();
            //ViewBag.data = dictionary.Values;
            //ViewBag.datastring = dictionary.Keys;
            //foreach (var valuta in dictionary)
            //{
            //    DateTime time = DateTime.Now;
            //    client.AddCrypto(new Crypto(time.ToString()) { Currency = valuta.Key, datumIVrijeme = time, Email="A", StartValue=valuta.Value, EndValue=100});
            //}
            return View();
        }

        public ActionResult GetCurrencyForUser()
        {
            User user = (User)HttpContext.Application["user"];
            if (user != null)
            {
                List<Crypto> lista = client.GetAllCryptoForEmail(user.RowKey);
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                foreach (Crypto c in lista)
                {
                    if (dictionary.Keys.Contains(c.Currency))
                    {
                        dictionary[c.Currency] += c.Quantity;
                    }
                    else
                    {
                        dictionary[c.Currency] = c.Quantity;
                    }
                }
                ViewBag.Currency = dictionary;
            }
            else
            {
                ViewBag.Currency = new Dictionary<string, int>();
            }
            return View();
        }
        public async Task<ActionResult> BuyOrSell()
        {
            if (TempData["ind"] == null || TempData["ind"].ToString() != "filter")
            {
                List<PriceInfo> prices = await client.GetTickerAllAsync();
                List<string> symbols = new List<string>();
                foreach (var p in prices)
                {
                    symbols.Add(p.Symbol);
                }
                IQueryable<Crypto> list = client.RetrieveAllCurrency();
                foreach (var v in list)
                {
                    v.EndValue = (prices.Where(p => p.Symbol == v.Currency).FirstOrDefault()).Price;
                    client.UpdateCrypto(v);
                }
                TempData["Filter"] = prices.ToList();
                TempData["Symbols"] = symbols.ToList();
                ViewBag.Symbols = symbols;
                ViewBag.AllInfo = prices;

            }
            else
            {
                ViewBag.AllInfo = (List<PriceInfo>)TempData["FilterList"];
                ViewBag.Symbols = (List<string>)TempData["Symbols"];
                TempData["ind"] = "base";
            }
            return View();
        }

        public async Task<ActionResult> Filter(string filter)
        {
            List<PriceInfo> filters = (List<PriceInfo>)TempData["Filter"];
            List<PriceInfo> pov = new List<PriceInfo>();
            foreach(var v in filters)
            {
                if (v.Symbol.Contains(filter))
                {
                    pov.Add(v);
                }
            }

            TempData["FilterList"] = pov.ToList();
            TempData["ind"] = "filter";
            return RedirectToAction("BuyOrSell");
        }
        public async Task<ActionResult> BuyOrSellCrypto(CryptoDTO crypto)
        {
            try
            {
                double value = await client.GetTickerForCurrencyAsync(crypto.Currency);
                int quantity = Convert.ToInt32(crypto.Price / value);
                if (quantity > 0)
                {
                    if (crypto.Type == "K")
                    {
                        Crypto c = new Crypto(DateTime.Now.ToString()) { Currency = crypto.Currency, datumIVrijeme = crypto.Date, Email = ((User)HttpContext.Application["user"]).RowKey, StartValue = value, EndValue = value, Quantity = quantity, BuyOrSell = crypto.Type };
                        client.AddCrypto(c);
                        ViewBag.Kupljeno = "Uspesno kupljeno";
                    }
                    else if (crypto.Type == "P")
                    {
                        Crypto c = new Crypto(DateTime.Now.ToString()) { Currency = crypto.Currency, datumIVrijeme = crypto.Date, Email = ((User)HttpContext.Application["user"]).RowKey, StartValue = value, EndValue = value, Quantity = quantity, BuyOrSell = crypto.Type };
                        client.AddCrypto(c);
                        client.GetAllCryptoForSell(crypto.Currency, crypto.Price.ToString(), ((User)HttpContext.Application["user"]).RowKey);
                        ViewBag.Kupljeno = "Uspesno ste prodali valutu";
                    }
                }
                else
                {
                    ViewBag.Kupljeno = "Nemate dovoljno novca za ovu transakciju";
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View();
        }
        public async Task<ActionResult> AddAlarm(CryptoDTO crypto)
        {
            try
            {
                User user = (User)HttpContext.Application["user"];
                if (user != null)
                {
                    alarms.AddAlarm(new Alarm() { Currency = crypto.Currency, Email = user.RowKey, AlarmValue = crypto.Price });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return RedirectToAction("UpdateAlarmView");
        }

        public async Task<ActionResult> DeleteCrypto()
       {
            User user = (User)HttpContext.Application["user"];
            if (user != null)
            {
                List<Crypto> currency = client.GetAllCryptoForEmail(user.RowKey);
                List<string> lista = new List<string>();
                foreach (var c in currency)
                {

                    lista.Add(c.Currency);
                }
                List<string> novalista = lista.Distinct().ToList();
                ViewBag.Symbols = novalista;
                ViewBag.Currency = currency;
            }
            else
            {
                ViewBag.Symbols = new List<string>();
                ViewBag.Currency = new List<Crypto>();
            }
            return View();
       }


        public ActionResult DeleteCurrency(CurrencyNameDTO currency)
        {
            User user = (User)HttpContext.Application["user"];
            if (user != null)
            {
                try
                {
                    client.RemoveCrypto(user.RowKey, currency.Name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return RedirectToAction("DeleteCrypto");
        }

        public ActionResult DeletePartialCrypto(string deleteCrypto)
        {
            User user = (User)HttpContext.Application["user"];
            if (user != null)
            {
                try
                {
                    client.RemoveCryptoId(deleteCrypto);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return RedirectToAction("DeleteCrypto");
        }

        public ActionResult ChooseCurrencyToShow()
        {
            User user = (User)HttpContext.Application["user"];
            try
            {
                List<Crypto> currency = client.GetAllCryptoForEmail(user.RowKey);
                List<string> lista = new List<string>();
                foreach (var c in currency)
                {
                    lista.Add(c.Currency);
                }
                List<string> novalista = lista.Distinct().ToList();
                ViewBag.Symbols = novalista;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
      
            if (TempData["result"] != null)
            {
                ViewBag.Result = TempData["result"];
                ViewBag.Currency = TempData["currency"];
                ViewBag.NotResult = false;
            }
            else
            {
                ViewBag.NotResult = true;
            }
            TempData["result"] = null;
            TempData["currency"] = null;
            return View();
        }

        public async Task<ActionResult> ViewProfitOrLoss(CurrencyNameDTO currency)
        {
            User user = (User)HttpContext.Application["user"];
            List<Crypto> list = client.GetAllCryptoForEmailCurrency(user.RowKey, currency.Name);
            double value = await client.GetTickerForCurrencyAsync(currency.Name);
            double result = 0;
            foreach (var v in list)
            {
                result += (value - v.StartValue)*v.Quantity;
                v.EndValue = value;
                client.UpdateCrypto(v);
            }
            TempData["result"] = result;
            TempData["currency"] = currency.Name;
            return RedirectToAction("ChooseCurrencyToShow");
        }

        public async Task<ActionResult> ViewEntireProfitOrLoss()
        {
            User user = (User)HttpContext.Application["user"];
            List<Crypto> list = new List<Crypto>();
            list = client.GetAllCryptoForEmail(user.RowKey);
            List<string> names = new List<string>();
            foreach(var v in list)
            {
                names.Add(v.Currency);
            }
            List<string> novalista = names.Distinct().ToList();
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            dictionary = await client.GetTickerForCurrencyForProfitAsync(novalista);

            double result = 0;
            foreach(var v in list)
            {
                result += (dictionary[v.Currency] - v.StartValue)*v.Quantity;
                if (v.BuyOrSell != "P")
                {
                    v.EndValue = dictionary[v.Currency];
                    client.UpdateCrypto(v);
                }
            }

            ViewBag.Result = result;
            return View();
        }

        public ActionResult UpdateAlarmView()
        {
            CloudQueue queue = QueueHelper.GetQueueReference("vezba");
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("ImageConverter_WorkerRole entry point called", "Information");
            List<string> AlarmDoneList = new List<string>();

            while (true)
            {
                CloudQueueMessage message = queue.GetMessage();
                if (message == null)
                {
                    Trace.TraceInformation("Trenutno ne postoji poruka u redu.", "Information");
                    break;
                }
                else
                {
                    Trace.TraceInformation(String.Format("Poruka glasi: {0}", message.AsString), "Information");
                    AlarmDoneList.Add(message.AsString);
                    queue.DeleteMessage(message);
                    Trace.TraceInformation(String.Format("Poruka procesuirana: {0}", message.AsString), "Information");
                }
            }

            User user = (User)HttpContext.Application["user"];
            List<Alarm> alarmForUser = new List<Alarm>();
            alarmForUser = alarmdone.GetAlarmEmail(user.RowKey);
            foreach (string s in AlarmDoneList)
            {
                Alarm alarm = alarmdone.GetAlarm(s, user.RowKey);
                if (!alarmForUser.Contains(alarm))
                {
                    alarmForUser.Add(alarm);
                }
            }
            ViewBag.AlarmForUser = alarmForUser;
            return View();
        }
        

    }
}