﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1
{
    public partial class AddressMgmt : Form
    {
        private string uName, UID;
        dynamic placeholder, update;
        controller.accountController accountController;
        controller.UIController UIController;
        controller.proFileController proFileController;

        public AddressMgmt()
        {
            InitializeComponent();
        }

        public AddressMgmt(controller.accountController accountController, controller.UIController UIController, controller.proFileController proFileController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.proFileController = proFileController;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
            cmbProvince.Items.AddRange(proFileController.getpriovince().ToArray());
        }

        private void Initialization()
        {

            timer1.Enabled = true;

            UID = accountController.getUID();
            uName = accountController.getName();
            lblUid.Text = "UID: " + UID;
            placeholder = proFileController.getAddinfo();

            //For determine which button needs to be shown.
            dynamic btnFun = UIController.showFun();
            btnFunction1.Visible = btnFun.btn1show;
            btnFunction1.Text = btnFun.btn1value;
            btnFunction2.Visible = btnFun.btn2show;
            btnFunction2.Text = btnFun.btn2value;
            btnFunction3.Visible = btnFun.btn3show;
            btnFunction3.Text = btnFun.btn3value;
            btnFunction4.Visible = btnFun.btn4show;
            btnFunction4.Text = btnFun.btn4value;
            btnFunction5.Visible = btnFun.btn5show;
            btnFunction5.Text = btnFun.btn5value;

            //Show user information
            dynamic add = proFileController.getAddinfo();
            tbCorpAdd.Text = add.corpAdd;
            tbWarehouseAdd1.Text = add.wAdd1;
            tbWarehouseAdd2.Text = add.wAdd2;
            cmbProvince.Text = add.province;
            cmbCity.Text = add.city;
            if (add.dfvalue == 1)
                rbtA1.Checked = true;
            else
                rbtA2.Checked = true;

            //For icon color
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
            }
        }

        private void cmbProvince_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCity.SelectedIndex = -1;       //clear the selected value when the province has change.
            cmbCity.Text = "";
            cmbCity.Items.Clear();            //clear the value when the selected province has change.
            cmbCity.Items.AddRange(proFileController.getcity(cmbProvince.Text).ToArray());             //change city list base on current selected province.
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if(checkInfo())
                if (proFileController.modifyAdd(update))
                {
                    MessageBox.Show("Modify successful!", "System message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnProFile_Click(this, e);   //Refresh the profile page.
                }
                else
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void tbCorpAdd_Enter(object sender, EventArgs e)
        {
            if (tbCorpAdd.Text == placeholder.corpAdd)
                tbCorpAdd.Text = "";
        }

        private void tbCorpAdd_Leave(object sender, EventArgs e)
        {
            if (tbCorpAdd.Text == "")
                tbCorpAdd.Text = placeholder.corpAdd;
        }

        private void tbWarehouseAdd1_Enter(object sender, EventArgs e)
        {
            if (tbWarehouseAdd1.Text == placeholder.wAdd1)
                tbWarehouseAdd1.Text = "";
        }

        private void tbWarehouseAdd1_Leave(object sender, EventArgs e)
        {
            if (tbWarehouseAdd1.Text == "")
                tbWarehouseAdd1.Text = placeholder.wAdd1;
        }

        private void tbWarehouseAdd2_Enter(object sender, EventArgs e)
        {
            if (tbWarehouseAdd2.Text == placeholder.wAdd2)
                tbWarehouseAdd2.Text = "";
        }

        private void tbWarehouseAdd2_Leave(object sender, EventArgs e)
        {
            if (tbWarehouseAdd2.Text == "")
                tbWarehouseAdd2.Text = placeholder.wAdd2;
        }

        private bool checkInfo() 
        {
            lblAddMsg.Text = "";
            update = new ExpandoObject();

            if (tbCorpAdd.Text != placeholder.corpAdd)               //Check company address
            {
                if (tbCorpAdd.Text.Length > 50 || tbCorpAdd.Text.Length > 50)
                {
                    lblAddMsg.Text = "Conpany address too long or too short, minimum 5, maximum 50.";
                    tbCorpAdd.Select();
                    return false;
                }
                else
                    update.corpAdd = tbCorpAdd.Text;
            }
            else
                update.corpAdd = placeholder.corpAdd;


            if (tbWarehouseAdd1.Text != placeholder.wAdd1 || rbtA1.Checked == false)               //Check company address
            {
                if (tbWarehouseAdd1.Text.Length > 50 || tbWarehouseAdd1.Text.Length > 50)
                {
                    lblAddMsg.Text = "Warehouse 1 address too long or too short, minimum 5, maximum 50.";
                    tbWarehouseAdd1.Select();
                    return false;
                }
                else
                    update.wAdd1 = tbWarehouseAdd1.Text;
            }
            else
                update.wAdd1 = placeholder.wAdd1;

            if (tbWarehouseAdd2.Text != placeholder.wAdd2 || rbtA2.Checked == false)               //Check company address
            {
                if (tbWarehouseAdd2.Text.Length > 50 || tbWarehouseAdd2.Text.Length > 50)
                {
                    lblAddMsg.Text = "Warehouse 2 address too long or too short, minimum 5, maximum 50.";
                    tbWarehouseAdd2.Select();
                    return false;
                }
                else
                    update.wAdd2 = tbWarehouseAdd2.Text;
            }
            else
                update.wAdd2 = placeholder.wAdd2;

            if (rbtA1.Checked)
                update.dfvalue = "1";
            else
                update.dfvalue = "2";


            if (string.IsNullOrEmpty(cmbCity.Text))               //Check city.
            {
                lblAddMsg.Text = "Please select a city.";
                return false;
            }
            else
                update.city = cmbCity.Text;
            update.province = cmbProvince.Text;

            return true;
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            proFileController = new controller.proFileController(accountController);

            proFileController.setType(accountController.getType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            this.Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = this.Location;
            proFile.Size = this.Size;
            proFile.ShowDialog();
            this.Close();
        }

        private void chkCancel_Click(object sender, EventArgs e)
        {
            proFileController = new controller.proFileController(accountController);

            proFileController.setType(accountController.getType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            this.Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = this.Location;
            proFile.Size = this.Size;
            proFile.ShowDialog();
            this.Close();
        }





    }
}
