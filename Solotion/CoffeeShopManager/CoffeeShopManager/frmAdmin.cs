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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager
{
    public partial class frmAdmin : Form
    {
        BindingSource drinkList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountlist = new BindingSource();
        public Account loginAccount;
        public frmAdmin()
        {
            InitializeComponent();
            loadAll();
        }
        #region Method
        void loadAll()
        {
            dgvDrink.DataSource = drinkList;
            dgvAccount.DataSource = accountlist;
            dgvCategory.DataSource = categoryList;
            dgvTable.DataSource = tableList;
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            loadListBillByDate(dtpFromDate.Value, dtpEndDate.Value);
            loadDateTimePickerBill();
            loadListDrink();
            LoadAccount();
            addDrinkBinding();
            loadListCategory();
            loadCategoryToComboBox(cbDrinkCategory);
            loadListTable();
            addFinalPriceCol();
        }
        void loadListBillOrderByTable()
        {
            dgvBill.DataSource = BillDAO.Instance.getListBillOrderByTable();
        }
        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        #region Drink
        List<Drink> SearchDrinkByName(string name)
        {
            List<Drink> listdrinks = DrinkDAO.Instance.SearchDrinkByName(name);

            return listdrinks;
        }
        void loadListDrink()
        {
            drinkList.DataSource = DrinkDAO.Instance.GetListDrink();
            dgvDrink.Columns[3].DefaultCellStyle.Format = "c";
        }
        void addDrinkBinding()
        {
            txtDrinkName.DataBindings.Add(new Binding("Text", dgvDrink.DataSource, "name", true, DataSourceUpdateMode.Never));
            txtDrinkID.DataBindings.Add(new Binding("Text", dgvDrink.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtPrice.DataBindings.Add(new Binding("Text", dgvDrink.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region Category
        void loadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
            txtCategoryID.DataBindings.Clear();
            txtCategoryName.DataBindings.Clear();
            txtCategoryID.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtCategoryName.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        void loadCategoryToComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
            cb.Refresh();
        }
        #endregion

        #region Account
        void LoadAccount()
        {
            accountlist.DataSource = AccountDAO.Instance.GetListAccount();
            txtAccountUsername.DataBindings.Clear();
            txtAccountDisplayName.DataBindings.Clear();
            nbAccountType.DataBindings.Clear();
            txtAccountUsername.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "userName", true, DataSourceUpdateMode.Never));
            txtAccountDisplayName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "displayName", true, DataSourceUpdateMode.Never));
            nbAccountType.DataBindings.Add(new Binding("Value", dgvAccount.DataSource, "type", true, DataSourceUpdateMode.Never));
        }
        void addAccount(string userName, string displayName, int type)
        {
            bool containsLetter = Regex.IsMatch(userName.Trim(), @"^[a-zA-Z1-9 ]+$");
            if (String.IsNullOrEmpty(userName.Trim()))
            {
                MessageBox.Show("Tên đăng nhập không được để trống");
                return;
            }
            if (String.IsNullOrEmpty(displayName.Trim()))
            {
                MessageBox.Show("Tên hiển thị không được để trống");
                return;
            }
            if (!containsLetter)
            {
                MessageBox.Show("Định dạng 'Tên tài khoản' không hợp lệ");
                return;
            }
            if (AccountDAO.Instance.checkAccountExist(txtAccountUsername.Text.Trim()) == 1)
            {
                MessageBox.Show("Tài khoản đã tồn tại");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn thêm tài khoản '{0}'\nTên hiển thị là '{1}'\nTài khoản sau khi được tạo sẽ không thể đổi 'Tên đăng nhập'", userName.Trim(), displayName), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (AccountDAO.Instance.InsertAccountAdmin(userName.Trim(), displayName, type))
                {
                    MessageBox.Show("Thêm tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại!");
                }
            }
            LoadAccount();
        }
        void updateAccount(string userName, string displayName, int type)
        {
            if (loginAccount.UserName.Equals(userName) && nbAccountType.Value == 0)
            {
                MessageBox.Show("Không thể thay đổi quyền !");
                return;
            }
            if (String.IsNullOrEmpty(userName.Trim()))
            {
                MessageBox.Show("Tên đăng nhập không được để trống");
                return;
            }
            if (String.IsNullOrEmpty(displayName.Trim()))
            {
                MessageBox.Show("Tên hiển thị không được để trống");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn sửa Tên hiển thị thành: '{0}'\n Loại tài khoản thành '{1}'", displayName, type), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (AccountDAO.Instance.UpdateAccountAdmin(userName.Trim(), displayName, type))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật khoản thất bại!");
                }
            }
            LoadAccount();
        }
        void deleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName.Trim()))
            {
                MessageBox.Show("Không thể xóa tài khoản đang được đăng nhập");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn sửa xóa tài khoản '{0}'", userName.Trim()), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (AccountDAO.Instance.DeleteAccountAdmin(userName.Trim()))
                {
                    MessageBox.Show("Xóa tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Xóa nhật khoản thất bại!");
                }
            }
            LoadAccount();
        }
        void resetPassword(string userName)
        {
            if (AccountDAO.Instance.resetPassword(userName))
            {
                MessageBox.Show("Đặt mật khẩu về mặc định thành công");
            }
            else
            {
                MessageBox.Show("Đặt mật khẩu về mặc định thất bại!");
            }
            LoadAccount();
        }
        #endregion

        #region Bill
        void loadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpEndDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void loadListBillByDate(DateTime dateCheckin, DateTime dateCheckout)
        {
            dgvBill.DataSource = BillDAO.Instance.GetListBillByDate(dateCheckin, dateCheckout);

        }
        double DgvSum(int cellIndex)
        {
            double sum = 0;
            for (int i = 0; i < dgvBill.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dgvBill.Rows[i].Cells[cellIndex].Value);
            }
            return sum;
        }
        void addFinalPriceCol()
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "finalPrice";
            column.Name = "Tổng cuối";
            dgvBill.Columns.Add(column);
        }

        #endregion

        #region Table
        void loadListTable()
        {
            tableList.DataSource = TableDAO.Instance.loadTableList();
            txtTableID.DataBindings.Clear();
            txtTableName.DataBindings.Clear();
            txtStatustable.DataBindings.Clear();
            txtTableID.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTableName.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtStatustable.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));


        }
        #endregion
        #endregion

        #region Events 
        #region frmAdmin
        private void frmAdmin_Shown(object sender, EventArgs e)
        {
            btnSearchBill.PerformClick();
        }
        #endregion

        #region Bill
        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpFromDate.Value, dtpEndDate.Value);
            double sum = DgvSum(5);
            dgvBill.Rows[0].Cells[0].Value = sum.ToString("c");
            dgvBill.Columns[5].DefaultCellStyle.Format = "c";
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            dgvBill.SelectAll();
            DataObject copydata = dgvBill.GetClipboardContent();
            if (copydata != null) Clipboard.SetDataObject(copydata);
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            application.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            object missdata = System.Reflection.Missing.Value;
            workbook = application.Workbooks.Add(missdata);
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 1];
            xlr.Select();
            worksheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }
        #endregion

        #region Drink
        private void btnViewDrink_Click(object sender, EventArgs e)
        {
            loadListDrink();
        }
        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtDrinkName.Text.Trim(), " {2,}", " ");
            string price1 = txtPrice.Text.Trim().Replace(" ", String.Empty);
            bool isNumber = double.TryParse(price1, out double result);
            bool containsLetter = Regex.IsMatch(convertToUnSign(name.Trim()).ToLower(), @"^[a-zA-Z0-9 ]+$");
            if (String.IsNullOrEmpty(name.Trim()))
            {
                MessageBox.Show("Tên không được để trống");
                return;
            }
            if (!containsLetter)
            {
                MessageBox.Show("Định dạng tên không hợp lệ");
                return;
            }
            if (DrinkDAO.Instance.checkDrinkExist(name.Trim()) == 1)
            {
                MessageBox.Show("Đồ uống đã tồn tại");
                return;
            }
            if (String.IsNullOrEmpty(price1))
            {
                MessageBox.Show("Giá không được để trống");
                return;
            }
            if (!isNumber)
            {
                MessageBox.Show("Giá phải là số");
                return;
            }
            else
            {
                if (MessageBox.Show(string.Format("Bạn có thực sự muốn thêm món '{0}' vào danh mục '{1}'", name.Trim(), cbDrinkCategory.GetItemText(this.cbDrinkCategory.SelectedItem)), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    int idCategory = (cbDrinkCategory.SelectedItem as Category).ID;
                    double price = Convert.ToDouble(txtPrice.Text);
                    if (DrinkDAO.Instance.InsertDrink(name.Trim(), idCategory, price))
                    {
                        MessageBox.Show("Thêm thành công");
                        loadListDrink();
                        if (insertDrink != null)
                        {
                            insertDrink(this, new EventArgs());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm");
                    }
                }
            }
        }
        private void btnEditDrink_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtDrinkName.Text.Trim(), " {2,}", " ");
            string price1 = txtPrice.Text.Trim().Replace(" ", String.Empty);
            bool isNumber = double.TryParse(price1, out double result);
            bool containsLetter = Regex.IsMatch(convertToUnSign(name).ToLower(), @"^[a-zA-Z0-9 ]+$");
            if (String.IsNullOrEmpty(name.Trim()))
            {
                MessageBox.Show("Tên không được để trống");
                return;
            }
            if (!containsLetter)
            {
                MessageBox.Show("Định dạng tên không hợp lệ");
                return;
            }
            if (DrinkDAO.Instance.checkDrinkExist(name.Trim()) == 1)
            {
                MessageBox.Show("Đồ uống đã tồn tại");
                return;
            }
            if (String.IsNullOrEmpty(price1))
            {
                MessageBox.Show("Giá không được để trống");
                return;
            }
            if (!isNumber)
            {
                MessageBox.Show("Giá phải là số");
                return;
            }
            else
            {
                if (MessageBox.Show(string.Format("Bạn có thực sự muốn sửa tên món thành '{0}' vào danh mục '{1}'", name.Trim(), cbDrinkCategory.GetItemText(this.cbDrinkCategory.SelectedItem)), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    int idCategory = (cbDrinkCategory.SelectedItem as Category).ID;
                    double price = Convert.ToDouble(txtPrice.Text);
                    int idDrink = Convert.ToInt32(txtDrinkID.Text);
                    if (DrinkDAO.Instance.UpdateDrink(idDrink, name.Trim(), idCategory, price))
                    {
                        MessageBox.Show("Sửa thành công");
                        loadListDrink();
                        if (insertDrink != null)
                        {
                            insertDrink(this, new EventArgs());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi Sửa");
                    }
                }
            }


            loadCategoryToComboBox(cbDrinkCategory);
        }
        private void btnDeleteDrink_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtDrinkName.Text.Trim(), " {2,}", " ");
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn xóa món '{0}' tại danh mục '{1}'", name, cbDrinkCategory.GetItemText(this.cbDrinkCategory.SelectedItem)), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int idDrink = Convert.ToInt32(txtDrinkID.Text);
                if (DrinkDAO.Instance.DeleteDrink(idDrink))
                {
                    MessageBox.Show("Xóa thành công");
                    loadListDrink();
                    if (deleteDrink != null)
                    {
                        deleteDrink(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa");
                }
            }
            loadCategoryToComboBox(cbDrinkCategory);
        }
        private void txtDrinkID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDrink.SelectedCells.Count > 0)
                {
                    int id = (int)dgvDrink.SelectedCells[0].OwningRow.Cells["IdCategory"].Value;
                    Category category = CategoryDAO.Instance.GetCategoryByID(id);
                    cbDrinkCategory.Refresh();
                    cbDrinkCategory.SelectedItem = category;
                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbDrinkCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbDrinkCategory.SelectedIndex = index;
                }
            }
            catch { }
        }
        private void btnSearchDrink_Click(object sender, EventArgs e)
        {
            txtDrinkID.Clear();
            txtDrinkName.Clear();
            cbDrinkCategory.SelectedItem = null;
            txtPrice.Clear();
            string name = Regex.Replace(txtSearchDrinkName.Text.Trim(), " {2,}", " ");
            bool containsLetter = Regex.IsMatch(convertToUnSign(name.Trim()).ToLower(), @"^[a-zA-Z0-9 ]+$");
            drinkList.DataSource = SearchDrinkByName(txtSearchDrinkName.Text);
            if (dgvDrink.Rows.Count == 0)
            {
                MessageBox.Show("Không có tên trùng với từ khóa");
                loadListDrink();
                return;
            }
            if (String.IsNullOrEmpty(name.Trim()))
            {
                MessageBox.Show("Tên không được để trống");
                loadListDrink();
                return;
            }
            if (!containsLetter)
            {
                MessageBox.Show("Định dạng tên không hợp lệ");
                loadListDrink();
                return;
            }
            

        }

        #endregion

        #region Category
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtCategoryName.Text.Trim(), " {2,}", " ");
            bool containsLetter = Regex.IsMatch(convertToUnSign(name.Trim()).ToLower(), @"^[a-zA-Z0-9 ]+$");
            if (!containsLetter)
            {
                MessageBox.Show("Định dạng tên không hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(name.Trim()))
            {
                MessageBox.Show("Tên không được để trống");
                return;
            }
            if (CategoryDAO.Instance.checkCategoryExist(name.Trim()) == 1)
            {
                MessageBox.Show("Danh mục đã tồn tại");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn thêm danh mục '{0}'", name.Trim()), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (CategoryDAO.Instance.InsertCategory(name.Trim()))
                {
                    MessageBox.Show("Thêm thành công");
                    loadListCategory();
                    if (insertCategory != null)
                    {
                        insertCategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm");
                }
                loadCategoryToComboBox(cbDrinkCategory);
            }

        }
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtCategoryName.Text.Trim(), " {2,}", " ");
            bool containsLetter = Regex.IsMatch(convertToUnSign(name.Trim()).ToLower(), @"^[a-zA-Z0-9 ]+$");
            if (!containsLetter)
            {
                MessageBox.Show("Định dạng tên không hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(name.Trim()))
            {
                MessageBox.Show("Tên không được để trống");
                return;
            }
            if (CategoryDAO.Instance.checkCategoryExist(name.Trim()) == 1)
            {
                MessageBox.Show("Danh mục đã tồn tại");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn sửa tên danh mục thành '{0}'", name.Trim()), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int idCategory = Convert.ToInt32(txtCategoryID.Text);
                if (CategoryDAO.Instance.UpdateCategory(name.Trim(), idCategory))
                {
                    MessageBox.Show("Sửa thành công");
                    loadListCategory();
                    if (updateCategory != null)
                    {
                        updateCategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi Sửa");
                }
            }
            loadCategoryToComboBox(cbDrinkCategory);
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtCategoryName.Text.Trim(), " {2,}", " ");
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn xóa danh mục '{0}'", name.Trim()), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int idCategory = Convert.ToInt32(txtCategoryID.Text);
                if (CategoryDAO.Instance.DeleteCategory(idCategory))
                {
                    MessageBox.Show("Xóa thành công");
                    loadListCategory();
                    if (deleteCategory != null)
                    {
                        deleteCategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa");
                }
            }
            loadCategoryToComboBox(cbDrinkCategory);
        }
        private void btnViewCategory_Click(object sender, EventArgs e)
        {
            loadListCategory();
        }
        #endregion

        #region Table
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtTableName.Text.Trim(), " {2,}", " ");
            bool containsLetter = Regex.IsMatch(convertToUnSign(name.Trim()).ToLower(), @"^[a-zA-Z0-9 ]+$");
            if (!containsLetter)
            {
                MessageBox.Show("Định dạng tên không hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(name.Trim()))
            {
                MessageBox.Show("Tên không được để trống");
                return;
            }
            if (TableDAO.Instance.checkTableExist(name.Trim()) == 1)
            {
                MessageBox.Show("Bàn đã tồn tại");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn thêm bàn '{0}'", name.Trim()), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (TableDAO.Instance.InsertTable(name.Trim()))
                {
                    MessageBox.Show("Thêm thành công");
                    loadListCategory();
                    if (insertTable != null)
                    {
                        insertTable(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm");
                }
            }

            loadListTable();
        }
        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtTableName.Text.Trim(), " {2,}", " ");
            bool containsLetter = Regex.IsMatch(convertToUnSign(name).ToLower(), @"^[a-zA-Z0-9 ]+$");

            if (!containsLetter)
            {
                MessageBox.Show("Định dạng tên không hợp lệ");
                return;
            }
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên không được để trống");
                return;
            }
            if (TableDAO.Instance.checkTableExist(name) == 1)
            {
                MessageBox.Show("Bàn đã tồn tại");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn sửa tên bàn thành '{0}'", name), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int idTable = Convert.ToInt32(txtTableID.Text);
                if (TableDAO.Instance.UpdateTable(idTable, name))
                {
                    MessageBox.Show("Sửa thành công");
                    loadListCategory();
                    if (updateTable != null)
                    {
                        updateTable(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi sửa");
                }
            }

            loadListTable();
        }
        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string name = Regex.Replace(txtTableName.Text.Trim(), " {2,}", " ");
            if (txtStatustable.Text.Equals("Có người"))
            {
                MessageBox.Show("Không đuổi khách !");
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có thực sự muốn xóa bàn '{0}'", name), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int idTable = Convert.ToInt32(txtTableID.Text);
                if (TableDAO.Instance.DeleteTable(idTable))
                {
                    MessageBox.Show("Xóa thành công");
                    loadListCategory();
                    if (deleteTable != null)
                    {
                        deleteTable(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa");
                }
            }

            loadListTable();
        }
        private void btnViewTable_Click(object sender, EventArgs e)
        {
            loadListTable();
        }
        #endregion

        #region Account
        private void btnViewAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }
        private void btnAddAcount_Click(object sender, EventArgs e)
        {
            string userName = txtAccountUsername.Text;
            string displayName = txtAccountDisplayName.Text;
            int type = Convert.ToInt32(nbAccountType.Value);
            addAccount(userName.Trim(), displayName, type);
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtAccountUsername.Text;
            string displayName = txtAccountDisplayName.Text;
            int type = Convert.ToInt32(nbAccountType.Value);
            updateAccount(userName.Trim(), displayName, type);
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtAccountUsername.Text;
            deleteAccount(userName.Trim());
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txtAccountUsername.Text;
            resetPassword(userName.Trim());
        }
        #endregion
        #endregion

        #region EventHandler
        #region Category
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }
        #endregion
        #region Drink
        private event EventHandler insertDrink;
        public event EventHandler InsertDrink
        {
            add { insertDrink += value; }
            remove { insertDrink -= value; }
        }

        private event EventHandler deleteDrink;
        public event EventHandler DeleteDrink
        {
            add { deleteDrink += value; }
            remove { deleteDrink -= value; }
        }

        private event EventHandler updateDrink;
        public event EventHandler UpdateDrink
        {
            add { updateDrink += value; }
            remove { updateDrink -= value; }
        }
        #endregion
        #region Table
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }













        #endregion

        #endregion

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            loadListBillOrderByTable();
            dgvBill.Columns.RemoveAt(0);
            dgvBill.Columns[2].DefaultCellStyle.Format = "c";
        }
    }
}
