using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }

        public bool Login(string username, string password)
        {
            string query = "EXEC User_login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {username, password });

            return result.Rows.Count > 0;
        }
        public Account GetAccountByUsername(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Account WHERE userName ='" + userName+"'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
    }
}
