using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.Withdrawal
{
    public class jsonWithdrawals
    {
        public jsonWithdrawal[] withdrawals { get; set; }

        public static Withdrawals ToWithdrawals(jsonWithdrawals _Withdrawals)
        {
            Withdrawals tmp = new Withdrawals();
            foreach (jsonWithdrawal t in _Withdrawals.withdrawals)
                tmp.MyWithdrawals.Add(t.ToWithdrawal());
            return tmp;
        }
        public Withdrawals ToWithdrawals() { return ToWithdrawals(this); }
        public static jsonWithdrawals FromWithdrawals(Withdrawals _Withdrawals)
        {
            List<jsonWithdrawal> tmpWithdrawals = new List<jsonWithdrawal>();
            foreach (Withdrawal t in _Withdrawals)
                tmpWithdrawals.Add(t);
            jsonWithdrawals tmp = new jsonWithdrawals { withdrawals = tmpWithdrawals.ToArray() };
            return tmp;
        }
    }
    public class jsonWithdrawal
    {
        public string status { get; set; }
        public string id { get; set; }

        public static Withdrawal ToWithdrawal(jsonWithdrawal _Withdrawal)
        {
            Withdrawal tmp = new Withdrawal { ID=_Withdrawal.id, Status=_Withdrawal.status };
            return tmp;
        }
        public Withdrawal ToWithdrawal() { return ToWithdrawal(this); }
        public static jsonWithdrawal FromWithdrawal(Withdrawal _Withdrawal)
        {
            return new jsonWithdrawal { id=_Withdrawal.ID, status=_Withdrawal.Status };
        }
    }
    public class Withdrawals : IEnumerable<Withdrawal>
    {
        public List<Withdrawal> MyWithdrawals { get; set; } = new List<Withdrawal>();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Withdrawal>)MyWithdrawals;
        }

        IEnumerator<Withdrawal> IEnumerable<Withdrawal>.GetEnumerator()
        {
            return (IEnumerator<Withdrawal>)MyWithdrawals;
        }
        public static jsonWithdrawals ToJsonWithdrawals(Withdrawals _Withdrawals)
        {
            return jsonWithdrawals.FromWithdrawals(_Withdrawals);
        }
        public jsonWithdrawals ToJsonWithdrawals()
        {
            return ToJsonWithdrawals(this);
        }
        public Withdrawals FromJsonWithdrawals(jsonWithdrawals _Withdrawals)
        {
            return _Withdrawals.ToWithdrawals();
        }

    }
    public class Withdrawal
    {
        public string Status { get; set; }
        public string ID { get; set; }
        public static jsonWithdrawal ToJsonWithdrawal(Withdrawal _Withdrawal)
        {
            return jsonWithdrawal.FromWithdrawal(_Withdrawal);
        }
        public jsonWithdrawal ToJsonWithdrawal() { return ToJsonWithdrawal(this); }
        public Withdrawal FromJsonWithdrawal(jsonWithdrawal _Withdrawal)
        {
            return _Withdrawal.ToWithdrawal();
        }
    }
    public enum WithdrawalType
    {
        ZAR_EFT, NAD_EFT, KES_MPESA, MYR_IBG, IDR_LLG
    }
    

}
