using CoffeeShopManager.DAO;
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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            loadListBillByDate(dtpFromDate.Value, dtpEndDate.Value);
            loadDateTimePickerBill();
        }
        #region Method
        void loadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpFromDate.Value = new DateTime(today.Year, today.Month,1);
            dtpEndDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void loadListBillByDate (DateTime dateCheckin, DateTime dateCheckout)
        {
            dgvBill.DataSource = BillDAO.Instance.GetListBillByDate(dateCheckin, dateCheckout);

        }
        #endregion

        #region Events 
        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpFromDate.Value, dtpEndDate.Value);
        }
        #endregion
   
    }
}
