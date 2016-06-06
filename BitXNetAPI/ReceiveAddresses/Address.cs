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
    }

    public class Address
    {

    }
}
