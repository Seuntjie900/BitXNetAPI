using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.Accounts
{
    public class jsonTransactions
    {
        public string id { get; set; } = "";
        public jsonTransaction[] transactions { get; set; } = null;

        public static Transactions ToTransactions(jsonTransactions _Transactions)
        {
            Transactions tmp = new Transactions { ID = _Transactions.id };
            foreach (jsonTransaction s in _Transactions.transactions)
                tmp.AccountTransactions.Add(s.ToTransaction());
            return tmp;
        }
        public Transactions ToTransactions()
        {
            return ToTransactions(this);
        }
        public static jsonTransactions FromTransactsion(Transactions _Transactions)
        {
            List<jsonTransaction> tmpTransactins = new List<jsonTransaction>();
            foreach (Transaction t in _Transactions)
                tmpTransactins.Add(t.ToJsonTransaction());
            jsonTransactions tmp = new jsonTransactions { id=_Transactions.ID, transactions=tmpTransactins.ToArray() };

            return tmp;
        }
    }
    public class jsonTransaction
    {
        public int row_index { get; set; }
        public long timestamp { get; set; }
        public decimal balance { get; set; }
        public decimal available { get; set; }
        public decimal balance_delta { get; set; }
        public decimal available_delta { get; set; }
        public string currency { get; set; }
        public string description { get; set; }

        public static Transaction ToTransaction(jsonTransaction Transaction)
        {
            Transaction tmp = new Accounts.Transaction {
                /*Available=decimal.Parse(Transaction.available, System.Globalization.NumberFormatInfo.InvariantInfo),
                AvailableDelta= decimal.Parse(Transaction.available_delta, System.Globalization.NumberFormatInfo.InvariantInfo),
                Balance= decimal.Parse(Transaction.balance, System.Globalization.NumberFormatInfo.InvariantInfo),
                BalanceDelta= decimal.Parse(Transaction.balance_delta, System.Globalization.NumberFormatInfo.InvariantInfo),*/
                Available = Transaction.available,
                AvailableDelta = Transaction.available_delta,
                Balance = Transaction.balance,
                BalanceDelta = Transaction.balance_delta,                
                Currency =Transaction.currency,
                Description=Transaction.description,
                RowIndex=Transaction.row_index,
                TimeStamp=JSON.FromEpoch(Transaction.timestamp)
            };
            return tmp;
        }
        public Transaction ToTransaction()
        {
            return ToTransaction(this);
        }
        public static jsonTransaction FromTransaction(Transaction _Transaction)
        {
            jsonTransaction tmp = new jsonTransaction {
                available=_Transaction.Available,
                available_delta=_Transaction.AvailableDelta,
                balance=_Transaction.Balance,
                balance_delta=_Transaction.BalanceDelta,
                currency=_Transaction.Currency,
                description=_Transaction.Description,
                row_index=_Transaction.RowIndex,
                timestamp=JSON.ToEpoch(_Transaction.TimeStamp)
            };
            return tmp;
        }
    }

    public class Transactions : IEnumerable<Transaction>
    {

        public string ID { get; set; }
        public List<Transaction> AccountTransactions { get; set; }
        public IEnumerator<Transaction> GetEnumerator()
        {
            return (IEnumerator<Transaction>)AccountTransactions;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Transaction>)AccountTransactions;
        }

        public static jsonTransactions ToJsonTransactions(Transactions _Transactions)
        {
            return jsonTransactions.FromTransactsion(_Transactions);
        }
        public jsonTransactions ToJsonTransactions()
        {
            return ToJsonTransactions(this);

        }
        public static Transactions FromJsonTransactions(jsonTransactions _Transactions)
        {
            return _Transactions.ToTransactions();
        }
    }

    public class Transaction
    {
        public int RowIndex { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Balance { get; set; }
        public decimal Available { get; set; }
        public decimal BalanceDelta { get; set; }
        public decimal AvailableDelta { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }

        public static jsonTransaction ToJsonTransaction(Transaction _Transaction)
        {
            return jsonTransaction.FromTransaction(_Transaction);
        }
        public jsonTransaction ToJsonTransaction()
        {
            return ToJsonTransaction(this);
        }
        public Transaction FromJsonTransaction(jsonTransaction _Transaction)
        {
            return _Transaction.ToTransaction();
        }
    }

    public class jsonPendingTransactions
    {
        public string id { get; set; } = "";
        public jsonTransaction[] pending { get; set; } = null;

        public static PendingTransactions ToTransactions(jsonPendingTransactions _Transactions)
        {
            PendingTransactions tmp = new PendingTransactions { ID = _Transactions.id };
            foreach (jsonTransaction s in _Transactions.pending)
                tmp.PendingAccountTransactions.Add(s.ToTransaction());
            return tmp;
        }
        public PendingTransactions ToTransactions()
        {
            return ToTransactions(this);
        }
        public static jsonPendingTransactions FromTransactsion(PendingTransactions _Transactions)
        {
            List<jsonTransaction> tmpTransactins = new List<jsonTransaction>();
            foreach (Transaction t in _Transactions)
                tmpTransactins.Add(t.ToJsonTransaction());
            jsonPendingTransactions tmp = new jsonPendingTransactions { id = _Transactions.ID, pending = tmpTransactins.ToArray() };

            return tmp;
        }
    }

    public class PendingTransactions:IEnumerable<Transaction>
    {
        public string ID { get; set; }
        public List<Transaction> PendingAccountTransactions { get; set; }
        public IEnumerator<Transaction> GetEnumerator()
        {
            return (IEnumerator<Transaction>)PendingAccountTransactions;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Transaction>)PendingAccountTransactions;
        }

        public static jsonPendingTransactions ToJsonTransactions(PendingTransactions _Transactions)
        {
            return jsonPendingTransactions.FromTransactsion(_Transactions);
        }
        public jsonPendingTransactions ToJsonTransactions()
        {
            return ToJsonTransactions(this);

        }
        public static PendingTransactions FromJsonTransactions(jsonPendingTransactions _Transactions)
        {
            return _Transactions.ToTransactions();
        }
    }

}
