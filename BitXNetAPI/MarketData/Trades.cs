using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.MarketData
{
    public class jsonTrades
    {
        public jsonTrade[] trades { get; set; } = null;

        public static Trades ToTrades(jsonTrades _Trades)
        {
            
            Trades tmp = new Trades();
            foreach (jsonTrade t in _Trades.trades)
                tmp.RecentTrades.Add(t.ToTrade());

            return tmp;
        }
        public Trades ToTrades()
        {
            return ToTrades(this);
        }
        public static jsonTrades FromTrades(Trades _Trades)
        {
            List<jsonTrade> trades = new List<jsonTrade>();
            foreach (Trade t in _Trades)
            {
                trades.Add(t.ToJsonTrade());
            }
            jsonTrades tmpTrades = new jsonTrades { trades=trades.ToArray() };
            return tmpTrades;
        }
    }
    public class Trades:IEnumerable<Trade>
    {
        public List<Trade> RecentTrades { get; set; } = new List<Trade>();

        public IEnumerator<Trade> GetEnumerator()
        {
            return (IEnumerator<Trade>)RecentTrades;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Trade>)RecentTrades;
        }

        public static jsonTrades ToJsonTrades(Trades _Trades)
        {
            return jsonTrades.FromTrades(_Trades);
        }
        public jsonTrades ToJsonTrades()
        {
            return ToJsonTrades(this);
        }
        public static Trades FromJsonTrades(jsonTrades _Trades)
        {
            return _Trades.ToTrades();
        }
    }
    public class jsonTrade
    {
        public string volume { get; set; }
        public long timestamp { get; set; }
        public string price { get; set; }
        public bool is_buy { get; set; }

        public static Trade ToTrade(jsonTrade _Trade)
        {
            Trade tmp = new Trade {
                IsBuy=_Trade.is_buy,
                 Price=decimal.Parse(_Trade.price, System.Globalization.NumberFormatInfo.InvariantInfo),
                 Volume=decimal.Parse(_Trade.volume, System.Globalization.NumberFormatInfo.InvariantInfo),
                  Timestamp= JSON.FromEpoch(_Trade.timestamp)
            };
            
            return tmp;
        }
        public Trade ToTrade()
        {
            return ToTrade(this);
        }
        public static jsonTrade FromTrade(Trade _Trade)
        {
            jsonTrade tmpTrade = new jsonTrade {
                is_buy=_Trade.IsBuy,
                volume=_Trade.Volume.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                price=_Trade.Price.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                timestamp=JSON.ToEpoch(_Trade.Timestamp)
            };
            return tmpTrade;
        }
    }
    public class Trade
    {
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsBuy { get; set; }

        public static jsonTrade ToJsonTrade(Trade _Trade)
        {
            return jsonTrade.FromTrade(_Trade);
        }
        public jsonTrade ToJsonTrade()
        {
            return ToJsonTrade(this);
        }
        public static Trade FromJsonTrade(jsonTrade _Trade)
        {
            return _Trade.ToTrade();
        }
    }
}
