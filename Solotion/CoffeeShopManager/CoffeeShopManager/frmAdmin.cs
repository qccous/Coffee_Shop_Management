using CoffeeShopManager.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            LoadAccountList();
            LoadDrinkList();
            LoadCategoryList();
            LoadTableList();
        }
        void LoadTableList()
        {
            string query = "select * from TableCoffee";

            dgvTable.DataSource = DataProvider.Instance.ExecuteQuery(query);

        }
        void LoadCategoryList()
        {
            string query = "select * from Category";

            dgvCategory.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        void LoadDrinkList()
        {
            string query = "select * from Drinks";

            dgvDrink.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        void LoadAccountList()
        {
            string query = "Select * from dbo.Account where type = 0";
            
            dgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
