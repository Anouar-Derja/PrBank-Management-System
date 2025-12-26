using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankingBusiness
{
    public class clsTransactionType
    {
        public int TransactionTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public const int Deposit = 1;
        public const int Withdrawal = 2;
        public const int TransferOut = 3;
        public const int TransferIn = 4;

        public clsTransactionType()
        {
            TypeName = "";
            Description = "";
        }
        public clsTransactionType(int transactionTypeID, string typeName, string description)
        {
            TransactionTypeID = transactionTypeID;
            TypeName = typeName;
            Description = description;
        }

    }
}