using Banking_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBusiness
{
    public class clsUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool CanAddClient { get; set; }
        public bool CanManageUser { get; set; }
        public bool CanShowClient { get; set; }
        public bool CanUpdateClient { get; set; }
        public bool CanTransaction { get; set; }
        public bool CanFindDeleteClient { get; set; }

        public clsUser()
        {
            Username = "";
            Password = "";
            CreatedAt = DateTime.Now;
            IsActive = true;
        }

        public clsUser(int UserID, string Username, string Password, DateTime CreatedAt, bool IsActive,
                      bool CanAddClient = false, bool CanManageUser = false, bool CanShowClient = false,
                      bool CanUpdateClient = false, bool CanTransaction = false, bool CanFindDeleteClient = false)
        {
            this.UserID = UserID;
            this.Username = Username;
            this.Password = Password;
            this.CreatedAt = CreatedAt;
            this.IsActive = IsActive;
            this.CanAddClient = CanAddClient;
            this.CanManageUser = CanManageUser;
            this.CanShowClient = CanShowClient;
            this.CanUpdateClient = CanUpdateClient;
            this.CanTransaction = CanTransaction;
            this.CanFindDeleteClient = CanFindDeleteClient;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static clsUser Authenticate(string username, string password)
        {
            return clsUserData.AuthenticateUser(username, password);
        }

     
    }
}
