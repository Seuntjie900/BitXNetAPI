using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.MarketData
{
    public class jsonOrderBook
    {
        public long timstamp { get; set; }
        public jsonOrder[] bids { get; set; } = null;
        public jsonOrder[] asks { get; set; } = null;

        public static OrderBook ToOrderBook(jsonOrderBook Book)
        {
            OrderBook tmpBook = new OrderBook()
            {
                TimeStamp=JSON.FromEpoch(Book.timstamp),
                
            };
            foreach (jsonOrder tmpOrder in Book.asks)
            {
                tmpBook.Bids.Add(tmpOrder.ToOrder());
            }
            foreach (jsonOrder tmpOrder in Book.asks)
            {
                tmpBook.Asks.Add(tmpOrder.ToOrder());
            }
            return tmpBook;
        }
        public OrderBook ToOrderBook()
        {
            return ToOrderBook(this);
        }
        public static jsonOrderBook FromOrderBook(OrderBook Book)
        {
            List<jsonOrder> asks = new List<jsonOrder>();
            List<jsonOrder> bids = new List<jsonOrder>();
            foreach (Order x in Book.Asks)
                asks.Add(x.ToJsonOrder());
            foreach (Order x in Book.Bids)
                bids.Add(x.ToJsonOrder());
            jsonOrderBook tmpBook = new jsonOrderBook()
            {
                timstamp= JSON.ToEpoch(Book.TimeStamp),
                asks=asks.ToArray(),
                bids=bids.ToArray()
            };
            return tmpBook;
        }
    }
    public class jsonOrder
    {
        public string volume { get; set; }
        public string price { get; set; }

        public static Order ToOrder(jsonOrder _Order)
        {
            Order tmpOrder = new Order()
            {
                Price=decimal.Parse(_Order.price, System.Globalization.NumberFormatInfo.InvariantInfo),
                 Volume = decimal.Parse(_Order.volume, System.Globalization.NumberFormatInfo.InvariantInfo)
            };
            return tmpOrder;
        }
        public Order ToOrder()
        {
            return ToOrder(this);
        }
        public static jsonOrder FromOrder(Order _Order)
        {
            jsonOrder tmpOrder = new jsonOrder()
            {
                price = _Order.Price.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                volume = _Order.Volume.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)
            };
            return tmpOrder;
        }
    }

    public class OrderBook
    {
        public DateTime TimeStamp { get; set; }
        public List<Order> Bids { get; set; } = new List<Order>();
        public List<Order> Asks { get; set; } = new List<Order>();
        public static jsonOrderBook ToJsonOrderBook(OrderBook Book)
        {
            return jsonOrderBook.FromOrderBook(Book);
        }
       public jsonOrderBook ToJsonOrderBook()
        {
            return ToJsonOrderBook(this);
        }
        public static OrderBook FromJsonOrderBook(jsonOrderBook Book)
        {
            return Book.ToOrderBook();
        }

    }

    public class Order
    {
        public decimal Volume { get; set; }
        public decimal Price { get; set; }

        public static jsonOrder ToJsonOrder(Order _Order)
        {
            return jsonOrder.FromOrder(_Order);
        }

        public jsonOrder ToJsonOrder()
        {
            return ToJsonOrder(this);
        }
        public static Order FromJsonOrder(jsonOrder _Order)
        {
            return _Order.ToOrder();
        }
    }

}
