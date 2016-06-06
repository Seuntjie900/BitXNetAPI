using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;


namespace BitXNetAPI
{
    public class BitX
    {
        public string APIID { get; set; } = "";
        public string APIKey { get; set; } = "";

        HttpClient BitxClient = new HttpClient();//https://api.mybitx.com/api/1
        HttpClientHandler ClientHandlr = new HttpClientHandler();

        public bool Connect(string APIID, string APIKey)
        {
            bool Success = false;
            this.APIID = APIID;
            this.APIKey = APIKey;

            return Success;
        }
    }
}
