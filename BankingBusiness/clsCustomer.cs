using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankingBusiness
{
    public class clsCustomer
    {
        public int CustomerID { get; set; }
        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AccountBalance { get; set; }
        public string AccountStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserID { get; set; }

        public clsCustomer()
        {
            AccountNumber = "";
            PinCode = "";
            FirstName = "";
            LastName = "";
            Phone = "";
            AccountStatus = "";
        }
        public clsCustomer(int CustomerID, string AccountNumber, string PinCode,
      string FirstName, string LastName, string Phone, DateTime DateOfBirth,
      decimal AccountBalance, string AccountStatus, DateTime CreatedAt, int UserID)
        {
            this.CustomerID = CustomerID;
            this.AccountNumber = AccountNumber;
            this.PinCode = PinCode;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.DateOfBirth = DateOfBirth;
            this.AccountBalance = AccountBalance;
            this.AccountStatus = AccountStatus;
            this.CreatedAt = CreatedAt;
            this.UserID = UserID;
        }

        // Empty constructor
        

        // CORRECTED METHOD - This should call the data access layer, not itself
        public static DataTable GetAllCustomers()
        {
            // Call the data access layer method, not itself (this was causing recursion)
            return clsCustomerData.GetAllCustomers();
        }
       
      
    }
}
