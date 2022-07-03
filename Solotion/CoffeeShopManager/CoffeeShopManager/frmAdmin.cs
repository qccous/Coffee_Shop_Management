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
        }
        void LoadAccountList()
        {
            string query = "Select * from dbo.Account where type = 0";
            DataProvider provider = new DataProvider();
            dgvAccount.DataSource = provider.ExecuteQuery(query);
        }
    }
}
