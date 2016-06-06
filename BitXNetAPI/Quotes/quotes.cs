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

        public static Quote ToQuote(jsonQuote _Quote)
        {
            Quote tmp = new Quote {
                BaseAmount=decimal.Parse(_Quote.base_amount, System.Globalization.NumberFormatInfo.InvariantInfo),
                CounterAmount=decimal.Parse(_Quote.counter_amount, System.Globalization.NumberFormatInfo.InvariantInfo),
                CreatedAt = JSON.FromEpoch(_Quote.created_at),
                Discareded=_Quote.discarded,
                 Exercised=_Quote.exercised,
                  ExpiresAt=JSON.FromEpoch(_Quote.expires_at),
                   ID=_Quote.id,
                    Pair=_Quote.pair,
                     Type=_Quote.type
            };
            return tmp;
        }
        public Quote ToQuote()
        {
            return ToQuote(this);
        }
        public static jsonQuote FromQuote(Quote _Quote)
        {
            return  new jsonQuote{
                base_amount = _Quote.BaseAmount.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                 counter_amount= _Quote.CounterAmount.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                  created_at=JSON.ToEpoch(_Quote.CreatedAt),
                   discarded=_Quote.Discareded, 
                    exercised= _Quote.Exercised,
                     expires_at=JSON.ToEpoch(_Quote.ExpiresAt),
                      id=_Quote.ID,
                       pair=_Quote.Pair,
                        type=_Quote.Type
            };
        }
    }

    public class Quote
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Pair { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal CounterAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Discareded { get; set; }
        public bool Exercised { get; set; }

        public static jsonQuote ToJsonQuote(Quote _Quote)
        {
            return jsonQuote.FromQuote(_Quote);
        }
        public jsonQuote ToJsonQuote()
        {
            return ToJsonQuote(this);
        }
        public static Quote FromJsonQuote(jsonQuote _Quote)
        {
            return _Quote.ToQuote();
        }
    }
}
