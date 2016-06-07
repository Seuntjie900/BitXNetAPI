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

        public List<Call> Calls { get; set; } = new List<Call>();

        private bool CheckBurstCalls()
        {
            if (Calls.Count < 5)
                return false;
            else
            {
                int Count = 0;
                for (int i = Calls.Count; i>Calls.Count-5 && Calls.Count>0; i--)
                {

                }
            }
            return false;
        }
        private bool CheckUnAuthdCalls()
        {
            return false;
        }
        private bool CheckAuthdCalls()
        {
            return false;
        }
        private void PreCall()
        {
            bool cansend = true;
            do
            {
                cansend = CheckBurstCalls();
                if (!CheckAuthdCalls())
                    cansend = false;
                if (!CheckAuthdCalls())
                    cansend = false;
                Task.Delay(100).Wait();
            } while (!cansend);
        }

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
            PreCall();
            string s = await BitxClient.GetStringAsync("ticker/pair=" + Pair);
            MarketData.Ticker tmpTicker = null;
            Task<MarketData.Ticker> tmp = new Task<MarketData.Ticker>( () => { tmpTicker=JSON.JsonDeserialize<MarketData.jsonTicker>(s).ToTicker(); return tmpTicker; });
            Calls.Add(new Call("Ticker",false));
            return tmpTicker;
        }

        public async Task<MarketData.Ticker[]> GetAllTickers()
        {
            PreCall();
            string s = await BitxClient.GetStringAsync("tickers");
            MarketData.Ticker tmpTicker = null;
            Task<MarketData.Ticker> tmp = new Task<MarketData.Ticker>(() => { tmpTicker = JSON.JsonDeserialize<MarketData.jsonTicker>(s).ToTicker(); return tmpTicker; });
            Calls.Add(new Call("Tickers", false));
            return tmpTicker.Tickers;
        }
        public async Task<MarketData.OrderBook> GetOrderBook(string Pair)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync("orderbook?pair="+Pair);
            MarketData.OrderBook tmpTicker = null;
            Task<MarketData.OrderBook> tmp = new Task<MarketData.OrderBook>(() => { tmpTicker = JSON.JsonDeserialize<MarketData.jsonOrderBook>(s).ToOrderBook(); return tmpTicker; });
            Calls.Add(new Call("OrderBook", false));
            return tmpTicker;
        }
        public async Task<MarketData.Trades> GetTrades(string Pair)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync("trades?pair=" + Pair);
            MarketData.Trades tmpTicker = null;
            Task<MarketData.Trades> tmp = new Task<MarketData.Trades>(() => { tmpTicker = JSON.JsonDeserialize<MarketData.jsonTrades>(s).ToTrades(); return tmpTicker; });
            Calls.Add(new Call("Trades", false));
            return tmpTicker;
        }
        #endregion

        #region Accounts
        public async Task<Accounts.Balances> GetBalances(string Pair)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync("balance");
            Accounts.Balances tmpTicker = null;
            Task< Accounts.Balances> tmp = new Task<Accounts.Balances> (() => { tmpTicker = JSON.JsonDeserialize<Accounts.jsonBalances>(s).ToBalances(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Accounts.Transactions> GetAccountTransactions(string ID, int MinRow, int MaxRow)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync(string.Format("accounts/{0}/transactions?min_row={1}&max_row={2}",ID,MinRow,MaxRow));
            Accounts.Transactions tmpTicker = null;
            Task<Accounts.Transactions> tmp = new Task<Accounts.Transactions>(() => { tmpTicker = JSON.JsonDeserialize<Accounts.jsonTransactions>(s).ToTransactions(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Accounts.Balances> GetPendingTransactions(string ID)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync(string.Format("accounts/{0}/pending", ID));
            Accounts.Balances tmpTicker = null;
            Task<Accounts.Balances> tmp = new Task<Accounts.Balances>(() => { tmpTicker = JSON.JsonDeserialize<Accounts.jsonBalances>(s).ToBalances(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        #endregion

        #region Orders

        public async Task<Orders.Order> GetOrder(string OrderID)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync(string.Format("orders/"+OrderID));
            Orders.Order tmpTicker = null;
            Task<Orders.Order> tmp = new Task<Orders.Order>(() => { tmpTicker = JSON.JsonDeserialize<Orders.jsonOrder>(s).ToOrder(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Orders.Orders> GetOrders()
        {
            PreCall();
            string s = await BitxClient.GetStringAsync(string.Format("listorders"));
            Orders.Orders tmpTicker = null;
            Task<Orders.Orders> tmp = new Task<Orders.Orders>(() => { tmpTicker = JSON.JsonDeserialize<Orders.jsonOrders>(s).ToOrders(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Orders.Orders> GetOrders(string val, bool Type)
        {
            PreCall();

            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            if (Type)
                pairs.Add(new KeyValuePair<string, string>("type", val));
            else
                pairs.Add(new KeyValuePair<string, string>("pair", val));
            
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);
            string s = await BitxClient.PostAsync(string.Format("listorders"), Content).Result.Content.ReadAsStringAsync();
            //string s = await BitxClient.GetStringAsync(string.Format("listorders?{0}={1}", Type?"type":"pair", val));
            Orders.Orders tmpTicker = null;
            Task<Orders.Orders> tmp = new Task<Orders.Orders>(() => { tmpTicker = JSON.JsonDeserialize<Orders.jsonOrders>(s).ToOrders(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Orders.Orders> GetOrders(string Type, string Pair)
        {
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("pair", Pair));
            pairs.Add(new KeyValuePair<string, string>("type", Type));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);
            string s = await BitxClient.PostAsync(string.Format("listorders"), Content).Result.Content.ReadAsStringAsync();
            Orders.Orders tmpTicker = null;
            Task<Orders.Orders> tmp = new Task<Orders.Orders>(() => { tmpTicker = JSON.JsonDeserialize<Orders.jsonOrders>(s).ToOrders(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Orders.OrderTransaction> PostLimitOrder(string Pair, string Type, decimal Volume,decimal Price)
        {
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("pair", Pair));
            pairs.Add(new KeyValuePair<string, string>("type", Type));
            pairs.Add(new KeyValuePair<string, string>("volume", Volume.ToString("0.00000000", System.Globalization.NumberFormatInfo.InvariantInfo)));
            pairs.Add(new KeyValuePair<string, string>("price", Price.ToString("0.00000000", System.Globalization.NumberFormatInfo.InvariantInfo)));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);

            string s = await BitxClient.PostAsync(string.Format("postorder"), Content).Result.Content.ReadAsStringAsync();
            Orders.OrderTransaction tmpTicker = null;
            Task<Orders.OrderTransaction> tmp = new Task<Orders.OrderTransaction>(() => { tmpTicker = JSON.JsonDeserialize<Orders.jsonOrderTransaction>(s).ToOrderTransaction(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Orders.OrderTransaction> PostMarketOrder(string Pair, string Type, decimal Volume,bool Buy)
        {
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("pair", Pair));
            pairs.Add(new KeyValuePair<string, string>("type", Type));
            if (Buy)
                pairs.Add(new KeyValuePair<string, string>("counter_volume", Volume.ToString("0.00000000", System.Globalization.NumberFormatInfo.InvariantInfo)));
            else
                pairs.Add(new KeyValuePair<string, string>("base_volume", Volume.ToString("0.00000000", System.Globalization.NumberFormatInfo.InvariantInfo)));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);

            string s = await BitxClient.PostAsync(string.Format("marketorder"), Content).Result.Content.ReadAsStringAsync();
            Orders.OrderTransaction tmpTicker = null;
            Task<Orders.OrderTransaction> tmp = new Task<Orders.OrderTransaction>(() => { tmpTicker = JSON.JsonDeserialize<Orders.jsonOrderTransaction>(s).ToOrderTransaction(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Orders.OrderTransaction> StopOrder(string OrderID)
        {
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("order_id", OrderID));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);

            string s = await BitxClient.PostAsync(string.Format("stoporder"), Content).Result.Content.ReadAsStringAsync();
            Orders.OrderTransaction tmpTicker = null;
            Task<Orders.OrderTransaction> tmp = new Task<Orders.OrderTransaction>(() => { tmpTicker = JSON.JsonDeserialize<Orders.jsonOrderTransaction>(s).ToOrderTransaction(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        #endregion

        #region Receive Addresses
        public async Task<ReceiveAddresses.Address> GetDefaultAddress(string Asset)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync(string.Format("funding_address/asset=" + Asset));
            ReceiveAddresses.Address tmpTicker = null;
            Task<ReceiveAddresses.Address> tmp = new Task<ReceiveAddresses.Address>(() => { tmpTicker = JSON.JsonDeserialize<ReceiveAddresses.jsonAddress>(s).ToAddress(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<ReceiveAddresses.Address> CreateReceivingAddress(string Asset)
        {
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("asset", Asset));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);

            string s = await BitxClient.PostAsync(string.Format("funding_address"), Content).Result.Content.ReadAsStringAsync();
            ReceiveAddresses.Address tmpTicker = null;
            Task<ReceiveAddresses.Address> tmp = new Task<ReceiveAddresses.Address>(() => { tmpTicker = JSON.JsonDeserialize<ReceiveAddresses.jsonAddress>(s).ToAddress(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        #endregion

        #region Withdrawals
        public async Task<Withdrawal.Withdrawals> GetWithdrawals()
        {
            PreCall();
            string s = await BitxClient.GetStringAsync(string.Format("withdrawals"));
            Withdrawal.Withdrawals tmpTicker = null;
            Task<Withdrawal.Withdrawals> tmp = new Task<Withdrawal.Withdrawals>(() => { tmpTicker = JSON.JsonDeserialize<Withdrawal.jsonWithdrawals>(s).ToWithdrawals(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Withdrawal.Withdrawal> GetWithdrawalStatus(string WithdrawalID)
        {
            PreCall();
            string s = await BitxClient.GetStringAsync(string.Format("withdrawals/"+WithdrawalID));
            Withdrawal.Withdrawal tmpTicker = null;
            Task<Withdrawal.Withdrawal> tmp = new Task<Withdrawal.Withdrawal>(() => { tmpTicker = JSON.JsonDeserialize<Withdrawal.jsonWithdrawal>(s).ToWithdrawal(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Withdrawal.Withdrawal> RequestWithdrawal(Withdrawal.WithdrawalType Type, decimal Amount)
        {
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("type", Type.ToString()));
            pairs.Add(new KeyValuePair<string, string>("amount", Amount.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);
            string s = await BitxClient.PostAsync(string.Format("withdrawals"), Content).Result.Content.ReadAsStringAsync();
            Withdrawal.Withdrawal tmpTicker = null;
            Task<Withdrawal.Withdrawal> tmp = new Task<Withdrawal.Withdrawal>(() => { tmpTicker = JSON.JsonDeserialize<Withdrawal.jsonWithdrawal>(s).ToWithdrawal(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Withdrawal.Withdrawal> CancelWithdrawal(string WithdrawalID)
        {
            PreCall();
            /*List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("id", WithdrawalID));
            
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);*/
            
            string s = await BitxClient.DeleteAsync(string.Format("withdrawals/"+WithdrawalID)).Result.Content.ReadAsStringAsync();
            Withdrawal.Withdrawal tmpTicker = null;
            Task<Withdrawal.Withdrawal> tmp = new Task<Withdrawal.Withdrawal>(() => { tmpTicker = JSON.JsonDeserialize<Withdrawal.jsonWithdrawal>(s).ToWithdrawal(); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        #endregion

        #region Send
        
        public async Task<Send.jsonSend> Send(decimal Amount, string Currency, string address, string Description=null, string Message=null)
        { 
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("amount", Amount.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            pairs.Add(new KeyValuePair<string, string>("currency", Currency));
            pairs.Add(new KeyValuePair<string, string>("address", address));
            if (Description!=null)
                pairs.Add(new KeyValuePair<string, string>("description", Description));
            if (Message!=null)
                pairs.Add(new KeyValuePair<string, string>("message", Message));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);

            string s = await BitxClient.DeleteAsync(string.Format("send/")).Result.Content.ReadAsStringAsync();
            Send.jsonSend tmpTicker = null;
            Task<Send.jsonSend> tmp = new Task<Send.jsonSend>(() => { tmpTicker = JSON.JsonDeserialize<Send.jsonSend>(s); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        #endregion

        #region Quotes
        public async Task<Quotes.Quote> CreateQuote(Orders.OrderTypes Type, decimal BaseAmount, string Pair)
        {
            
            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("type", Type.ToString()));
            pairs.Add(new KeyValuePair<string, string>("base_amount	", BaseAmount.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)));
            pairs.Add(new KeyValuePair<string, string>("pair", Pair));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);

            string s = await BitxClient.PostAsync(string.Format("quotes/"), Content).Result.Content.ReadAsStringAsync();
            Quotes.Quote tmpTicker = null;
            Task<Quotes.Quote> tmp = new Task<Quotes.Quote>(() => { tmpTicker = JSON.JsonDeserialize<Quotes.Quote>(s); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Quotes.Quote> GetQuote(string QuoteID)
        {

            PreCall();
            /*List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("type", Type.ToString()));
            pairs.Add(new KeyValuePair<string, string>("base_amount	", BaseAmount.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)));
            pairs.Add(new KeyValuePair<string, string>("pair", Pair));
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);*/

            //string s = await BitxClient.DeleteAsync(string.Format("quotes/")).Result.Content.ReadAsStringAsync();
            string s = await BitxClient.GetStringAsync("quotes/"+QuoteID);
            Quotes.Quote tmpTicker = null;
            Task<Quotes.Quote> tmp = new Task<Quotes.Quote>(() => { tmpTicker = JSON.JsonDeserialize<Quotes.Quote>(s); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Quotes.Quote> ExerciseQuote(string QuoteID)
        {

            PreCall();
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("id", QuoteID));
            
            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);

            string s = await BitxClient.PutAsync(string.Format("quotes/"+QuoteID), Content).Result.Content.ReadAsStringAsync();
            Quotes.Quote tmpTicker = null;
            Task<Quotes.Quote> tmp = new Task<Quotes.Quote>(() => { tmpTicker = JSON.JsonDeserialize<Quotes.Quote>(s); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        public async Task<Quotes.Quote> DiscardQuote(string QuoteID)
        {

            PreCall();
            /*List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("id", QuoteID));

            FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);*/

            string s = await BitxClient.DeleteAsync(string.Format("quotes/" + QuoteID)).Result.Content.ReadAsStringAsync();
            Quotes.Quote tmpTicker = null;
            Task<Quotes.Quote> tmp = new Task<Quotes.Quote>(() => { tmpTicker = JSON.JsonDeserialize<Quotes.Quote>(s); return tmpTicker; });
            Calls.Add(new Call("Balanaces", true));
            return tmpTicker;
        }
        #endregion
    }

    public struct Call
    {
        public Call(string Name, bool Authenticated)
        {
            Time = DateTime.Now;
            ApiCall = Name;
            this.Authenticated = Authenticated;
        }
        public DateTime Time { get; set; }
        public string ApiCall { get; set; }
        public bool Authenticated { get; set; }

    }
}
