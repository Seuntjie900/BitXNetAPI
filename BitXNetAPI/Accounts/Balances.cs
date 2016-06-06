using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.Accounts
{
    public class jsonBalances
    {
        public jsonBalance[] balance { get; set; }

        public static Balances ToBalances(jsonBalances _Balances)
        {

            Balances tmp = new Balances();
            foreach (jsonBalance t in _Balances.balance)
            {
                tmp.MyBalances.Add(t.ToBalance());
            }
            return tmp;
        }
        public Balances ToBalances()
        { return ToBalances(this); }

        public static jsonBalances FromBalances(Balances _Balances)
        {
            List<jsonBalance> tmpBals = new List<jsonBalance>();
            foreach (AssetBalance t in _Balances)
            {
                tmpBals.Add(t.ToJsonBalance());
            }
            jsonBalances tmp = new jsonBalances();
            return tmp;
        }
    }
    public class jsonBalance
    {
        public string account_id { get; set; }
        public string asset { get; set; }
        public string balance { get; set; }
        public string reserved { get; set; }
        public string unconfirmed { get; set; }
        public string name { get; set; }

        public static AssetBalance ToBalance(jsonBalance Balance)
        {
            AssetBalance tmp = new AssetBalance {
                 Account_ID=Balance.account_id,
                 Asset=Balance.asset,
                 Balance= decimal.Parse(Balance.balance, System.Globalization.NumberFormatInfo.InvariantInfo),
                 Name= Balance.name,
                 Reserved= decimal.Parse(Balance.reserved, System.Globalization.NumberFormatInfo.InvariantInfo),
                 Unconfirmed = decimal.Parse(Balance.unconfirmed, System.Globalization.NumberFormatInfo.InvariantInfo)
            };
            return tmp;
        }
        public AssetBalance ToBalance()
        {
            return ToBalance(this);
        }
        public static jsonBalance FromBalance(AssetBalance Balance)
        {
            jsonBalance tmp = new jsonBalance {
                account_id=Balance.Account_ID,
                asset=Balance.Asset,
                balance=Balance.Balance.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                name=Balance.Name,
                reserved=Balance.Reserved.ToString(System.Globalization.NumberFormatInfo.InvariantInfo),
                unconfirmed=Balance.Unconfirmed.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)
            };
            return tmp;
        }
    }

    public class Balances : IEnumerable<AssetBalance>
    {
        public List<AssetBalance> MyBalances { get; set; } = new List<AssetBalance>();
        public IEnumerator<AssetBalance> GetEnumerator()
        {
            return (IEnumerator<AssetBalance>)MyBalances;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<AssetBalance>)MyBalances;
        }
        public static jsonBalances ToJsonBalances(Balances _Balances)
        {
            return jsonBalances.FromBalances(_Balances);
        }
        public jsonBalances ToJsonBalances()
        {
            return ToJsonBalances(this);
        }
        public static Balances FromJsonBalances(jsonBalances _Balances)
        {
            return _Balances.ToBalances();
        }
    }
    public class AssetBalance
    {
        public string Account_ID { get; set; }
        public string Asset { get; set; }
        public decimal Balance { get; set; }
        public decimal Reserved { get; set; }
        public decimal Unconfirmed { get; set; }
        public string Name { get; set; }

        public static jsonBalance ToJsonBalance(AssetBalance Balance)
        {
            return jsonBalance.FromBalance(Balance);
        }
        public jsonBalance ToJsonBalance()
        {
            return ToJsonBalance(this);
        }
        public static AssetBalance FromJsonBalance(jsonBalance Balance)
        {
            return Balance.ToBalance();
        }
    }

}
