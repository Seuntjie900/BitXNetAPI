using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace BitXNetAPI
{
    class JSON
    {
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
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
