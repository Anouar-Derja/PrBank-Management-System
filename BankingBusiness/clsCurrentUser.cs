using BankingBusiness;
using System;

namespace BankingBusiness
{
    public static class clsCurrentUser
    {
        public static clsUser User { get; set; }

        public static bool HasPermission(string permission)
        {
            if (User == null) return false;

            switch (permission)
            {
                case "CanAddClient": return User.CanAddClient;
                case "CanManageUser": return User.CanManageUser;
                case "CanShowClient": return User.CanShowClient;
                case "CanUpdateClient": return User.CanUpdateClient;
                case "CanTransaction": return User.CanTransaction;
                case "CanFindDeleteClient": return User.CanFindDeleteClient;
                default: return false;
            }
        }

        public static void Logout()
        {
            User = null;
        }
    }
}