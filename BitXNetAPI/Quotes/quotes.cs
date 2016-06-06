using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.Quotes
{
    public class jsonQuote
    {
        public string id { get; set; }
        public string type{ get; set; }
        public string pair { get; set; }
        public string base_amount { get; set; }
        public string counter_amount { get; set; }
        public long created_at { get; set; }
        public long expires_at { get; set; }
        public bool discarded { get; set; }
        public bool exercised { get; set; }
    }

    public class Quote
    {

    }
}
