using CoffeeShopManager.DAO;
using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager
{
    public partial class frmManager : Form
    {
        public frmManager()
        {
            InitializeComponent();
            loadTable();
            loadCategory();
        }

        #region Method
        void loadTable()
        {
            List<Table> listTable = TableDAO.Instance.loadTableList();
            foreach (Table item in listTable)
            {
                Button btn = new Button() { Width = 100, Height = 100 };
                btn.Text = item.Name + Environment.NewLine + "(" + item.Status + ")";
                btn.Click += btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Brown;
                        break;
                    default:
                        btn.BackColor = Color.Green;
                        break;
                }
                flbTable.Controls.Add(btn);
            }
        }

        void loadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "name";
        }

        void loadDrinkbyCategoryId(int id)
        {
            List<Drink> listDrink = DrinkDAO.Instance.GetDrinkByCategoryID(id);
            cbDrink.DataSource = listDrink;
            cbDrink.DisplayMember = "name";
        }
        void ShowBill(int id)
        {
            lstvBill.Items.Clear();
            List<Menu> listMenu = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (Menu item in listMenu)
            {
                ListViewItem lstvItem = new ListViewItem(item.DrinkName.ToString());
                lstvItem.SubItems.Add(item.Count.ToString());
                lstvItem.SubItems.Add(item.Price.ToString());
                lstvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lstvBill.Items.Add(lstvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentCulture = culture;
            txtTotalPrice.Text = totalPrice.ToString("c", culture);
        }
        #endregion

        #region Event
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lstvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);

        }

        private void itLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void itPersonalInfo_Click(object sender, EventArgs e)
        {
            frmAccountInfo frmAccountInfo = new frmAccountInfo();

            frmAccountInfo.ShowDialog();

        }

        private void itAdmin_Click(object sender, EventArgs e)
        {
            frmAdmin frmAdmin = new frmAdmin();
            frmAdmin.ShowDialog();
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            Category selected = cb.SelectedItem as Category;
            if (cb.SelectedItem == null)
            {
                return;
            }
            id = selected.ID;
            loadDrinkbyCategoryId(id);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Table table = lstvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUncheckBillIdByTableId(table.ID);
            int idDrink = (cbDrink.SelectedItem as Drink).ID;
            int count = (int)nbDrinkCount.Value;
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), idDrink, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idDrink, count);
            }
            ShowBill(table.ID);
        }

        #endregion


    }
}
