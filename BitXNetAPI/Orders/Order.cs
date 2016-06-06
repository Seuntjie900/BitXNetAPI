using System;
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
        public jsonTrade[] trades { get; set; }
    }
    public class jsonTrade:MarketData.jsonTrade
    {

    }
    public class Trade:MarketData.Trade
    {

    }

    public class jsonOrderTransaction
    {
        public string order_id { get; set; }
        public bool success { get; set; }
    }
    public class OrderTransaction
    {
        public string OrderID { get; set; }
        public bool Success { get; set; }
    }
}
