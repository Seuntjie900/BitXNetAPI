using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BitXNetAPI
{
    public class BitX
    {
        public string APIID { get; set; } = "";
        public string APIKey { get; set; } = "";

        HttpClient BitxClient = new HttpClient();//https://api.mybitx.com/api/1
        HttpClientHandler ClientHandlr = new HttpClientHandler();
        CookieContainer Cookies = new CookieContainer();
        public bool Connect(string APIID, string APIKey)
        {
            bool Success = false;
            this.APIID = APIID;
            this.APIKey = APIKey;

            ClientHandlr = new HttpClientHandler() { CookieContainer = Cookies, UseCookies = true };
            BitxClient = new HttpClient(ClientHandlr) { BaseAddress = new Uri("https://api.mybitx.com/api/1") };
            BitxClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("basic", APIID + ":" + APIKey);

            return Success;
        }


        #region Market Data
        public async Task<MarketData.Ticker> GetTicker(string Pair)
        {
            string s = await BitxClient.GetStringAsync("ticker/pair=" + Pair);
            MarketData.Ticker tmpTicker = null;
            Task<MarketData.Ticker> tmp = new Task<MarketData.Ticker>( () => { tmpTicker=JSON.JsonDeserialize<MarketData.jsonTicker>(s).ToTicker(); return tmpTicker; });
            return tmpTicker;
        }

        #endregion
    }
}
