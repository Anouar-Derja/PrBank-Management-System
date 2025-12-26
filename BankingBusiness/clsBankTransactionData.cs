using Banking_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankingBusiness
{
    public class clsBankTransactionData
    {
        public int TransactionID { get; set; }
        public int TransactionTypeID { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public int UserID { get; set; }

        public clsBankTransactionData()
        {
            Description = "";
            FromAccount = "";
            ToAccount = "";
        }
        public clsBankTransactionData(int TransactionID, int TransactionTypeID, decimal Amount,
            decimal BalanceAfter, DateTime TransactionDate, string Description, string FromAccount, string ToAccount, int UserID  )
        {
            this.TransactionID = TransactionID;
            this.TransactionTypeID = TransactionTypeID;
            this.Amount = Amount;
            this.BalanceAfter = BalanceAfter;
            this.TransactionDate = TransactionDate;
            this.Description = Description;
            this.FromAccount = FromAccount;
            this.ToAccount = ToAccount;
        }

        // Empty constructor


        // CORRECTED METHOD - This should call the data access layer, not itself
        public static DataTable GetAllBankTransaction()
        {
            // Call the data access layer method, not itself (this was causing recursion)
            return Banking_DataAccess.clsBankTransaction.GetAllBankTransaction();
        }
    }
}
