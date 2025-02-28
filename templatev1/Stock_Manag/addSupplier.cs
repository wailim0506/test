﻿using System;
using System.Dynamic;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.controller.Utilities;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Order_Management;
using LMCIS.Profile;
using LMCIS.Properties;
using LMCIS.System_page;
using LMCIS.User_Manag;
using Microsoft.Extensions.Logging;

namespace LMCIS.Stock_Manag
{
    public partial class addSupplier : Form
    {
        private string uName, UID;
        dynamic newSupplierInfo;
        AccountController accountController;
        stockController stockController;
        UIController UIController;


        public addSupplier(AccountController accountController, UIController UIController,
            stockController stockController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.stockController = stockController;
        }

        private void addSupplier_Load(object sender, EventArgs e)
        {
            Initialization();
            Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is loaded the form.");
        }

        private void Initialization()
        {
            timer1.Enabled = true;

            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            setIndicator(UIController.getIndicator("Stock Management"));
            cmbCountry.Items.AddRange(stockController.GetCountry().ToArray());
            lblSuppIDMsg.Text = "Select a country to generate the part number.";
            chkStatus.Checked = true;

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


            //For icon color
            if (LMCIS.Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Resources.LBWhite;
                picHome.Image = Resources.homeWhite;
            }
        }

        private void setIndicator(int btnNo)
        {
            switch (btnNo)
            {
                case 1:
                    palSelect1.Visible = true;
                    break;
                case 2:
                    palSelect2.Visible = true;
                    break;
                case 3:
                    palSelect3.Visible = true;
                    break;
                case 4:
                    palSelect4.Visible = true;
                    break;
                case 5:
                    palSelect5.Visible = true;
                    break;
            }
        }

        //Determine next page.
        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        //Determine next page.
        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
                    }
                    else if (accountController.CheckIsDeliverman())
                    {
                        next = new deliverman(accountController, UIController);
                    }
                    else
                    {
                        next = new staffOrderList(accountController, UIController);
                    }

                    break;
                case "Spare Part":
                    next = new sparePartList(accountController, UIController);
                    break;
                case "Cart":
                    next = new cart(accountController, UIController);
                    break;
                case "Favourite":
                    next = new favourite(accountController, UIController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(accountController, UIController);
                    break;

                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
                    next = new SAccManage(accountController, UIController);
                    break;
                case "Invoice Management":
                    next = new staffInvoiceList(accountController, UIController);
                    break;
            }

            Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is going to the {Function} page.");
            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is going to the profile page.");
            proFileController proFileController = new proFileController(accountController);

            proFileController.setType(accountController.GetAccountType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.Size = Size;
            proFile.ShowDialog();
            Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is logging out.");
            Form login = new Login();
            Hide();
            //Swap the current form to another.
            login.StartPosition = FormStartPosition.Manual;
            login.Location = Location;
            login.Size = Size;
            login.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is going to the home page.");
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.Size = Size;
            home.ShowDialog();
            Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is going to the about page.");
            Form about = new About(accountController, UIController);
            Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = Location;
            about.Size = Size;
            about.ShowDialog();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is going to the view supplier page.");
            Form viewSupplier = new viewSupplier(accountController, UIController, stockController);
            Hide();
            //Swap the current form to another.
            viewSupplier.StartPosition = FormStartPosition.Manual;
            viewSupplier.Location = Location;
            viewSupplier.Size = Size;
            viewSupplier.ShowDialog();
            Close();
        }

        private void cmbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            lblSuppIDMsg.Text = "";
            lblSupplierID.Text
                = stockController.GenSupplierNumber(cmbCountry.SelectedItem.ToString());
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (checkInfo()) //Pass to controller and create account
            {
                setValue(); //If passed set the value in to dynameic.
                if (stockController.CreateNewSupplier(newSupplierInfo))
                {
                    MessageBox.Show(
                        $"Create new supplier success! New supplierID is {lblSupplierID.Text}"
                        , "System message", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    getPage("Stock Management");
                    Log.LogMessage(LogLevel.Information, "[View] Stock Management", $"User: {UID} is going to the stock management page.");
                }
                else
                {
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    Log.LogMessage(LogLevel.Critical, "[View] Stock Management", $"User: {UID} is going to the stock management page, but failed.");
                    getPage("Stock Management");
                }
            }
        }

        //Check the inputted data.
        private bool checkInfo()
        {
            //Clean previous error message.
            lblAddMsg.Text = lblPhoneMsg.Text = lblNameMsg.Text = lblCountryMsg.Text = "";

            //Check country.
            if (cmbCountry.SelectedItem == null)
            {
                lblCountryMsg.Text = "Please select a country.";
                cmbCountry.Select();
                return false;
            }

            //Check supplier name.
            if (tbName.Text.Length < 2 || tbName.Text.Length > 50)
            {
                lblNameMsg.Text = "Name too short or too long, minimum 2 maximum 50.";
                tbName.Select();
                return false;
            }

            //Check supplier address.
            if (tbAddress.Text.Length < 2 || tbAddress.Text.Length > 70)
            {
                lblAddMsg.Text = "Address too short or too long, minimum 2 maximum 70.";
                tbAddress.Select();
                return false;
            }

            //Check supplier phone.
            if (tbPhone.Text.Length < 5 || tbPhone.Text.Length > 20)
            {
                lblNameMsg.Text = "Phone number too short or too long, minimum 5 maximum 20.";
                lblNameMsg.Select();
                return false;
            }

            return true;
        }

        private void setValue()
        {
            newSupplierInfo = new ExpandoObject();
            newSupplierInfo.SID = lblSupplierID.Text;
            newSupplierInfo.Country = cmbCountry.SelectedItem.ToString();
            newSupplierInfo.Name = tbName.Text;
            newSupplierInfo.Phone = tbPhone.Text;
            newSupplierInfo.Address = tbAddress.Text;
            newSupplierInfo.Status = chkStatus.Checked ? "Enable" : "Disable";
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}