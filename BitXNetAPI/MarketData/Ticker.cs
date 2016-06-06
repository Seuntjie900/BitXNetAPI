using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.MarketData
{
    public class jsonTicker
    {
        public jsonTicker[] tickers { get; set; } = null;
        public string ask { get; set; } = "";
        public long timestamp { get; set; } = 0;
        public string bid { get; set; } = "";
        public string rolling_24_hour_volume { get; set; } = "";
        public string last_trade { get; set; } = "";

        public static Ticker ToTicker(jsonTicker Tick)
        {
            Ticker tmp = new Ticker();
            if (tmp.Tickers == null)
            {
                try
                {
                    tmp.Ask = decimal.Parse(Tick.ask, System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.Bid = decimal.Parse(Tick.ask, System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.Rolling24HAvg = decimal.Parse(Tick.ask, System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.LastTrade = decimal.Parse(Tick.ask, System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.TimeStamp = JSON.FromEpoch(Tick.timestamp);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    List<Ticker> tmpTickers = new List<Ticker>();
                    foreach (jsonTicker tmpTick in Tick.tickers)
                    {
                        tmpTickers.Add(tmpTick.ToTicker());
                    }
                    tmp.Tickers = tmpTickers.ToArray();
                }
                catch
                {
                    return null;
                }
            }
            return tmp;
        }

        public Ticker ToTicker()
        {
            return (ToTicker(this));
        }
        public static jsonTicker FromTicker(Ticker Tick)
        {
            jsonTicker tmp = new jsonTicker();
            try
            {
                if (Tick.Tickers == null)
                {
                    tmp.ask = Tick.Ask.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.bid = Tick.Ask.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.rolling_24_hour_volume = Tick.Ask.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.last_trade = Tick.Ask.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                    tmp.timestamp = JSON.ToEpoch(Tick.TimeStamp);
                }
                else
                {
                    List<jsonTicker> tmpTickers = new List<jsonTicker>();
                    foreach (Ticker tmpTick in Tick.Tickers)
                    {
                        tmpTickers.Add(jsonTicker.FromTicker(tmpTick));
                    }
                    tmp.tickers = tmpTickers.ToArray();
                }
            }
            catch
            { return null; }

            return tmp;
        }
    }
    
    public class Ticker
    {
        public Ticker[] Tickers { get; set; }
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public decimal Rolling24HAvg { get; set; }
        public decimal LastTrade { get; set; }
        public DateTime TimeStamp { get; set; }

        public static jsonTicker ToJsonTicker(Ticker Tick)
        {
            return jsonTicker.FromTicker(Tick);
        }
        public jsonTicker ToJsonTicker()
        {
            return ToJsonTicker(this);
        }
        public Ticker FromJsonTicker(jsonTicker Tick)
        {
            return Tick.ToTicker();
        }
    }
}
