using CoffeeShopManager.DAO;
using CoffeeShopManager.DTO;
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
    public partial class frmAccountInfo : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; changeAccountInfo(LoginAccount); }
        }
        public frmAccountInfo(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        #region Method
        void changeAccountInfo(Account acc)
        {
            txtUsername.Text = LoginAccount.UserName;
            txtDisplayName.Text = LoginAccount.DisplayName;
        }
        void updateAccountInfo()
        {
            string username = txtUsername.Text;
            string displayName = txtDisplayName.Text;
            string password = txtPassword.Text;
            string newPassword = txtNewPassword.Text;
            string reEnterPassword = txtReEnter.Text;
            if (String.IsNullOrEmpty(password.Trim()))
            {
                MessageBox.Show("Hãy nhập mật khẩu để xác nhận thao tác!");
                return;
            }
            if (String.IsNullOrEmpty(displayName.Trim()))
            {
                MessageBox.Show("Tên hiển thị không được để trống");
                return;
            }
            if (!newPassword.Equals(reEnterPassword))
            {
                MessageBox.Show("Mật khẩu mới không trùng khớp!");
            }
            else
            {
                if (MessageBox.Show(string.Format("Bạn có thực sự muốn cập nhật các thông tin trên?"), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (AccountDAO.Instance.UpdateAccountInfo(username, displayName, password, newPassword))
                    {
                        MessageBox.Show("Cập nhật thành công");
                    }
                    else
                    {
                        MessageBox.Show("Hãy điền đúng thông tin");
                    }
                    changeAccountInfo(LoginAccount);
                    txtPassword.Clear();
                    txtNewPassword.Clear();
                    txtReEnter.Clear();
                }

            }
        }
        #endregion

        #region Event
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateAccountInfo();

        }
        #endregion




    }
}
