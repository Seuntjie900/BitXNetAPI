using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitXNetAPI.Authentication
{
    public enum PermissionsList
    {
        R_Balance,R_Transactions,W_Send,R_Addresses,W_Addresses,R_Orders,W_Orders,R_Withdrawals,W_Withdrawals,R_Merchant,W_Merchant,R_ClientDebit,W_ClientDebit,R_Beneficiaries,W_Beneficiaries
    }

    public class Permissions
    {
        public List<PermissionsList> MyPermissions { get; private set; } = new List<PermissionsList>();
        public static Permissions FromInt(int PermissionsBit)
        {
            throw new NotImplementedException();
        }
    }
}
