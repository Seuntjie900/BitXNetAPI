using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.ReceiveAddresses
{
    public class jsonAddress
    {
        public string asset { get; set; }
        public string address { get; set; }
        public string total_received { get; set; }
        public string total_unconfirmed { get; set; }

        public static Address ToAddress(jsonAddress _Address)
        {
            Address tmp = new Address { Asset = _Address.asset, AssetAddress = _Address.address, TotalReceived = decimal.Parse(_Address.total_received, System.Globalization.NumberFormatInfo.InvariantInfo), TotalUnconfirmed = decimal.Parse(_Address.total_unconfirmed, System.Globalization.NumberFormatInfo.InvariantInfo) };
            return tmp;
        }
        public Address ToAddress()
        {
            return ToAddress(this);
        }
        public static jsonAddress FromAddress(Address _Address)
        {
            jsonAddress tmp = new jsonAddress
            {
                 address=_Address.AssetAddress,
                  asset=_Address.Asset,
                   total_received=_Address.TotalReceived.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                    total_unconfirmed = _Address.TotalUnconfirmed.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)
            };
            return tmp;
        }

    }

    public class Address
    {
        public string Asset { get; set; }
        public string AssetAddress { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal TotalUnconfirmed { get; set; }

        public static jsonAddress ToJsonAddress(Address _Address)
        {
            return jsonAddress.FromAddress(_Address);
        }
        public jsonAddress ToJsonAddress() { return ToJsonAddress(this); }
        public static Address FromJsonAddress(jsonAddress _Address) { return _Address.ToAddress(); }
    }
}
