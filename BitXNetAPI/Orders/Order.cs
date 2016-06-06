using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.Orders
{
    public class jsonOrders
    {
        public jsonOrder[] orders { get; set; }
    }
    public class jsonOrder
    {
        public string order_id { get; set; }
        public long creation_timestamp { get; set; }
        public long expiration_timestamp { get; set; }
        public long completed_timestamp { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string limit_price { get; set; }
        public string limit_volume { get; set; }
        public string @base { get; set; }
        public string counter { get; set; }
        public string fee_base { get; set; }
        public string fee_counter { get; set; }
        public jsonTrade[] trades { get; set; } = new jsonTrade[0];

        public static Order ToOrder(jsonOrder _Order)
        {
            Order tmp = new Order
            {
                Base=decimal.Parse( _Order.@base, System.Globalization.NumberFormatInfo.InvariantInfo),
                FeeBase= decimal.Parse(_Order.fee_base, System.Globalization.NumberFormatInfo.InvariantInfo),
                CompletedTime = JSON.FromEpoch(_Order.completed_timestamp),
                Counter= decimal.Parse(_Order.counter, System.Globalization.NumberFormatInfo.InvariantInfo),
                CreationTime =JSON.FromEpoch(_Order.creation_timestamp),
                ExpirationTime=JSON.FromEpoch(_Order.expiration_timestamp),
                FeeCounter= decimal.Parse(_Order.fee_counter, System.Globalization.NumberFormatInfo.InvariantInfo),
                LimitPrice = decimal.Parse(_Order.limit_price, System.Globalization.NumberFormatInfo.InvariantInfo),
                LimitVolume = decimal.Parse(_Order.limit_volume, System.Globalization.NumberFormatInfo.InvariantInfo),
                OrderID = _Order.order_id
                
            };
            tmp.State = (_Order.state.ToLower() == "pending" ? OrderStates.Pending : _Order.state.ToLower() == "completed" ? OrderStates.Complete : OrderStates.Cancelled);
            tmp.Type = _Order.type.ToLower() == "ask" ? OrderTypes.Ask : OrderTypes.Bid;

            foreach (jsonTrade t in _Order.trades)
            {
                tmp.Trades.Add(t.ToTrade());
            }

            return tmp;
        }

        public Order ToOrder()
        {
            return ToOrder(this);
        }
        public static jsonOrder FromOrder(Order _Order)
        {
            List<jsonTrade> Trades = new List<jsonTrade>();
            foreach (Trade t in _Order.Trades)
                Trades.Add(t.ToJsonTrade());
            jsonOrder tmpOrder = new jsonOrder {
                @base=_Order.Base.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                completed_timestamp=JSON.ToEpoch(_Order.CompletedTime),
                counter=_Order.Counter.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                creation_timestamp=JSON.ToEpoch(_Order.CreationTime),
                expiration_timestamp=JSON.ToEpoch(_Order.ExpirationTime),
                fee_base=_Order.FeeBase.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                fee_counter= _Order.FeeCounter.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                limit_price= _Order.LimitPrice.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                limit_volume= _Order.LimitVolume.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                order_id= _Order.OrderID,
                state= _Order.State.ToString().ToUpper(),
                trades=Trades.ToArray(),
                type= _Order.Type.ToString().ToUpper()
            };
            
            
            return tmpOrder;
        }
    }

    public class Orders : IEnumerable<Order>
    {
        public List<Order> TradeOrders { get; set; } = new List<Order>();
        public IEnumerator<Order> GetEnumerator()
        {
            return (IEnumerator<Order>)TradeOrders;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Order>)TradeOrders;
        }
    }

    public class Order
    {
        public string OrderID { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime CompletedTime { get; set; }
        public OrderTypes Type { get; set; }
        public OrderStates State { get; set; }
        public decimal LimitPrice { get; set; }
        public decimal LimitVolume { get; set; }
        public decimal Base { get; set; }
        public decimal Counter { get; set; }
        public decimal FeeBase { get; set; }
        public decimal FeeCounter { get; set; }
        public List<Trade> Trades { get; set; } = new List<Trade>();

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
    public class jsonTrade
    {
        public string volume { get; set; }
        public long timestamp { get; set; }
        public string price { get; set; }
        public bool is_buy { get; set; }

        public static Trade ToTrade(jsonTrade _Trade)
        {
            Trade tmp = new Trade
            {
                IsBuy = _Trade.is_buy,
                Price = decimal.Parse(_Trade.price, System.Globalization.NumberFormatInfo.InvariantInfo),
                Volume = decimal.Parse(_Trade.volume, System.Globalization.NumberFormatInfo.InvariantInfo),
                Timestamp = JSON.FromEpoch(_Trade.timestamp)
            };

            return tmp;
        }
        public Trade ToTrade()
        {
            return ToTrade(this);
        }
        public static jsonTrade FromTrade(Trade _Trade)
        {
            jsonTrade tmpTrade = new jsonTrade
            {
                is_buy = _Trade.IsBuy,
                volume = _Trade.Volume.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                price = _Trade.Price.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                timestamp = JSON.ToEpoch(_Trade.Timestamp)
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

    public class jsonOrderTransaction
    {
        public string order_id { get; set; }
        public bool success { get; set; }

        public static OrderTransaction ToOrderTransaction(jsonOrderTransaction Tx)
        {
            return new OrderTransaction { OrderID = Tx.order_id, Success = Tx.success };

        }
        public OrderTransaction ToOrderTransaction()
        {
            return ToOrderTransaction(this);
        }
        public  static jsonOrderTransaction FromOrderTransaction(OrderTransaction tx)
        {
            return new jsonOrderTransaction { success = tx.Success, order_id = tx.OrderID };
        }
    }
    public class OrderTransaction
    {
        public string OrderID { get; set; }
        public bool Success { get; set; }

        public static jsonOrderTransaction ToJsonOrderTransaction(OrderTransaction tx)
        {
            return jsonOrderTransaction.FromOrderTransaction(tx);
        }
        public jsonOrderTransaction ToJsonOrderTransaction()
        {
            return ToJsonOrderTransaction(this);
        }
        public static OrderTransaction FromJsonOrderTransaction(jsonOrderTransaction tx)
        { return tx.ToOrderTransaction();  }
    }

    public enum OrderTypes
    {
        Ask,Bid
    }
    public enum OrderStates
    {
        Pending,Complete,Cancelled
    }
}
