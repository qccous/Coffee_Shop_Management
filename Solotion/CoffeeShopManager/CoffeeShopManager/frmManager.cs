﻿using System;
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
    public partial class frmManager : Form
    {
        public frmManager()
        {
            InitializeComponent();
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
    }
}
