using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BitXNetAPI
{
    class JSON
    {
        public static DateTime FromEpoch(long Milliseconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Milliseconds);
        }
        public static long ToEpoch(DateTime Date)
        {
            return (long)(Date - DateTime.Parse("1970/01/01 00:00:00", System.Globalization.DateTimeFormatInfo.InvariantInfo)).TotalMilliseconds;
        }
    }
}
