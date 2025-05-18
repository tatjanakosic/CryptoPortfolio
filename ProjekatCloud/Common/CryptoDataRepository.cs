using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common
{
    public class CryptoDataRepository
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        private const string BaseUrl = "https://api.binance.com/api/v3";
        private readonly HttpClient _httpClient;

        public CryptoDataRepository()
        {
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("CryptoConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("CryptoTable"); _table.CreateIfNotExists();
            _httpClient = new HttpClient();

        }
        public IQueryable<Crypto> RetrieveAllCurrency()
        {
            var results = from g in _table.CreateQuery<Crypto>()
                          where g.PartitionKey == "Crypto"
                          select g;
            return results;
        }


        public async Task<Dictionary<string, double>> GetTickerAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/ticker/price");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var prices = JsonConvert.DeserializeObject<List<PriceInfo>>(content);

                // Convert the list to a dictionary for easier lookup
                var cryptoValues = new Dictionary<string, double>();
                foreach (var priceInfo in prices)
                {
                    if (priceInfo.Symbol == "BTCUSDT")
                        cryptoValues["bitcoin"] = priceInfo.Price;
                    if (priceInfo.Symbol == "ETHUSDT")
                        cryptoValues["ethereum"] = priceInfo.Price;
                    if (priceInfo.Symbol == "BNBUSDT")
                        cryptoValues["binance"] = priceInfo.Price;
                }
                return cryptoValues;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<double> GetTickerForCurrencyAsync(string currency)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/ticker/price");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var prices = JsonConvert.DeserializeObject<List<PriceInfo>>(content);

                // Convert the list to a dictionary for easier lookup
                var cryptoValues = new Dictionary<string, double>();
                foreach (var priceInfo in prices)
                {
                    if (priceInfo.Symbol == currency)
                        return priceInfo.Price;
                }

                return 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }


        }

        public async Task<Dictionary<string, double>> GetTickerForCurrencyForProfitAsync(List<string> currency)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/ticker/price");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var prices = JsonConvert.DeserializeObject<List<PriceInfo>>(content);
                Dictionary<String, double> dictionary = new Dictionary<string, double>();
                // Convert the list to a dictionary for easier lookup
                var cryptoValues = new Dictionary<string, double>();
                foreach (var priceInfo in prices)
                {
                    if (currency.Contains(priceInfo.Symbol))
                    {
                        dictionary[priceInfo.Symbol] = priceInfo.Price;
                    }
                }
                return dictionary;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }


        }

        public async Task<List<PriceInfo>> GetTickerAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/ticker/price");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var prices = JsonConvert.DeserializeObject<List<PriceInfo>>(content);

                // Convert the list to a dictionary for easier lookup

                return prices;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
        public void AddCrypto(Crypto newCrypto)
        { // Samostalni rad: izmestiti tableName u konfiguraciju servisa. 
            TableOperation insertOperation = TableOperation.Insert(newCrypto);
            _table.Execute(insertOperation);
        }

        public List<Crypto> GetAllCryptoForEmail(string email)
        {
            return RetrieveAllCurrency().Where(p => p.Email == email && p.BuyOrSell == "K").ToList();
        }

        public void GetAllCryptoForSell(string currency, string value, string email)
        {
            double vrednost = Convert.ToDouble(value);
            List<Crypto> list = RetrieveAllCurrency().Where(p => p.Email == email && p.Currency == currency && p.EndValue <= vrednost && p.Quantity > 0 && p.BuyOrSell == "K").ToList();

            foreach (Crypto c in list)
            {
                if (vrednost >= 0 && c.EndValue < vrednost)
                {
                    double broj = vrednost / c.EndValue;
                    int b = (int)Math.Floor(broj);
                    if (c.Quantity < b)
                    {
                        vrednost -= c.EndValue * c.Quantity;
                        c.Quantity = 0;
                        UpdateCrypto(c);
                    }
                    else
                    {
                        vrednost -= c.EndValue * b;
                        c.Quantity = c.Quantity - b;
                        UpdateCrypto(c);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public List<Crypto> GetAllCryptoForEmailCurrency(string email, string currency)
        {
            return RetrieveAllCurrency().Where(p => p.Email == email && p.Currency == currency && p.BuyOrSell == "K").ToList();
        }


        //public bool Exists(string indexNo)
        //{
        //    return RetrieveAllUsers().Where(s => s.RowKey == indexNo).FirstOrDefault() != null;
        //}

        //public User ExistsUser(string email, string password)
        //{
        //    return RetrieveAllUsers().Where(s => s.RowKey == email && s.Password == password).FirstOrDefault();

        //}

        public void RemoveCrypto(string email, string currency)
        {
            List<Crypto> crypto = RetrieveAllCurrency().Where(s => s.Email == email && s.Currency == currency).ToList();

            if (crypto != null)
            {
                foreach (Crypto c in crypto)
                {
                    TableOperation deleteOperation = TableOperation.Delete(c);
                    _table.Execute(deleteOperation);
                }
            }
        }

        public void RemoveCryptoId(string id)
        {
            Crypto c = RetrieveAllCurrency().Where(s => s.RowKey == id).FirstOrDefault();

            if (c != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(c);
                _table.Execute(deleteOperation);
            }
        }

        //public User GetUser(string index)
        //{
        //    return RetrieveAllUsers().Where(p => p.RowKey == index).FirstOrDefault();
        //}

        public void UpdateCrypto(Crypto crypto)
        {
            TableOperation updateOperation = TableOperation.Replace(crypto);
            _table.Execute(updateOperation);
        }
    }
}






//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using Newtonsoft.Json;

//public class CexIoApiClient
//{
//    private readonly HttpClient _client;

//    public CexIoApiClient()
//    {
//        _client = new HttpClient();
//        _client.DefaultRequestHeaders.Accept.Clear();
//        _client.DefaultRequestHeaders.Add("Accept", "/");
//    }

//    public async Task<string> GetTickerAsync(string symbol1, string symbol2)
//    {
//        var url = $"https://cex.io/api/ticker/%7Bsymbol1%7D/%7Bsymbol2%7D";
//        var response = await _client.GetAsync(url);

//        if (!response.IsSuccessStatusCode)
//        {
//            throw new Exception($"Error fetching ticker: {response.StatusCode}");
//        }

//        var content = await response.Content.ReadAsStringAsync();
//        return content;
//    }
//}

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        var client = new CexIoApiClient();
//        var tickerJson = await client.GetTickerAsync("btc", "usd");
//        Console.WriteLine(tickerJson);
//    }
//}