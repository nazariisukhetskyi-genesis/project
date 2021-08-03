using Newtonsoft.Json;
using Project_CSharp.Models;
using Project_CSharp.Repositories.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Project_CSharp.Repositories.Implementations
{
    public class CryptoRepository : ICryptoRepository
    { 
        // not secured
        private readonly string API_KEY = "48B2ABDF-D42F-476D-9E92-7CF692778DD6";


        private readonly UriBuilder URL = new UriBuilder("https://rest.coinapi.io/v1/exchangerate/BTC/UAH");
        private readonly WebClient client = new WebClient();

        public async Task<string> GetFullRateAsync_JSON()
        {
            client.Headers.Add("X-CoinAPI-Key", API_KEY);
            return await client.DownloadStringTaskAsync(URL.ToString());
        }

        public async Task<string> GetBtcRateAsync()
        {
            Crypto crypto = JsonConvert.DeserializeObject<Crypto>(await GetFullRateAsync_JSON());
            return crypto.Rate.ToString();
        }
    }
}
