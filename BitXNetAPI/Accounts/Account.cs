using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.Accounts
{
    public class jsonCreateAccount
    {
        public string id { get; set; }
        public string name { get; set; }
        public string currency { get; set; }

        public static CreatedAccount ToCreatedAccount(jsonCreateAccount Account)
        {
            CreatedAccount tmp = new CreatedAccount { ID = Account.id, Currency = Account.currency, Name = Account.name };
            return tmp;
        }
        public CreatedAccount ToCreatedAccount()
        {
            return ToCreatedAccount(this);
        }
        public static jsonCreateAccount FromCreatedAccount(CreatedAccount Account)
        {
            jsonCreateAccount tmp = new jsonCreateAccount { currency = Account.Currency, id = Account.ID, name = Account.Name };
            return tmp;
        }
    }
        
    public class CreatedAccount
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public static jsonCreateAccount ToJsonCreateAccount(CreatedAccount Account)
        {
            return jsonCreateAccount.FromCreatedAccount(Account);
        }
        public jsonCreateAccount ToJsonCreateAccount()
        {
            return ToJsonCreateAccount(this);
        }
        public CreatedAccount FromJsonCreateAccount(jsonCreateAccount Account)
        {
            return Account.ToCreatedAccount();
        }
    }
}
