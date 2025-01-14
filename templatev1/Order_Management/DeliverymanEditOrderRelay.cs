﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Profile;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;

namespace LMCIS.Order_Management
{
    public partial class DeliverymanEditOrderRelay : Form
    {
        private readonly AccountController _accountController;
        private readonly UIController _uiController;
        private readonly viewOrderController _viewController;
        private string uName, UID;
        readonly string _orderId;
        public DeliveryController DeliveryController;

        public DeliverymanEditOrderRelay(string orderID)
        {
            InitializeComponent();
            _viewController = new viewOrderController();
            _orderId = orderID;
            DeliveryController = new DeliveryController();
        }

        public DeliverymanEditOrderRelay(string orderId, AccountController accountController, UIController uiController)
        {
            InitializeComponent();
            _orderId = orderId;
            _accountController = accountController;
            _uiController = uiController;
            _viewController = new viewOrderController();
            DeliveryController = new DeliveryController();
            UID = _accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
        }

        private void delivermanViewOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblLoc.Text += $" {_orderId}";
            load_data();
            cbxCity.Enabled = false;
            cbxRelayName.Enabled = false;
            List<string> ProvinceList = DeliveryController.GetProvinceList();
            cbxProvince.Items.AddRange(ProvinceList.ToArray());
            cbxProvince.SelectedIndex = 0;
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(_uiController.getIndicator("Order Management"));
        }

        private void load_data()
        {
            DataTable dt = _viewController.GetOrder(_orderId);
            string[] f = dt.Rows[0][4].ToString().Split(' ');

            //order info
            lblOrderID.Text = _orderId;

            if ($"{dt.Rows[0][6]}" == "Shipped") //if status is shipped, hide the job finish button
            {
                btnReturn.Location = new Point(1037, 820);
            }

            //delivery info
            dt = new DataTable();
            dt = _viewController.GetShippingDetail(_orderId);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            string[] delivermanDetail = _viewController.GetDelivermanDetail(_orderId);
            if (_viewController.GetStatus(_orderId) == "Cancelled")
            {
                lblDelivermanID.Text = "N/A";
            }
            else
            {
                lblDelivermanID.Text = dt.Rows[0][1].ToString();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form deliverman = new deliverman(_accountController, _uiController);
            Hide();
            deliverman.StartPosition = FormStartPosition.Manual;
            deliverman.Location = Location;
            deliverman.ShowDialog();
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void btnRelayEdit_Click(object sender, EventArgs e)
        {
            DeliveryController.EditRelay(_orderId, DeliveryController.GetRelayId(cbxRelayName.SelectedItem.ToString()));
        }

        private void cbxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComboBox(cbxProvince, cbxCity, DeliveryController.GetCitiesByProvince);
        }

        private void cbxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComboBox(cbxCity, cbxRelayName, DeliveryController.GetRelayNamesByCity);
        }

        private void UpdateComboBox(ComboBox source, ComboBox target, Func<string, List<string>> fetchItems)
        {
            target.Items.Clear();
            target.Enabled = true;
            var items = fetchItems(source.SelectedItem.ToString());
            target.Items.AddRange(items.ToArray());
            if (target.Items.Count > 0) target.SelectedIndex = 0;
        }

        private void cbxRelayName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string location = DeliveryController.GetRelayLocation(cbxRelayName.SelectedItem.ToString());
            RelayPointImage.Load(DeliveryController.GenerateMapUrl(location, RelayPointImage.Size,
                cbxRelayName.SelectedItem.ToString()));
        }

        public void hideButton()
        {
            dynamic btnFun = _uiController.showFun();
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
        }

        private void getPage(string Function)
        {
            Form next = new Home(_accountController, _uiController);
            switch (Function)
            {
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(_accountController, _uiController);
                    }
                    else if (_accountController.CheckIsDeliverman())
                    {
                        next = new deliverman(_accountController, _uiController);
                    }
                    else
                    {
                        next = new staffOrderList(_accountController, _uiController);
                    }

                    break;
                case "Spare Part":
                    next = new sparePartList(_accountController, _uiController);
                    break;
                case "Cart":
                    next = new cart(_accountController, _uiController);
                    break;
                case "Favourite":
                    next = new favourite(_accountController, _uiController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(_accountController, _uiController);
                    break;

                case "On-Sale Product Management":
                    next = new OnSaleMain(_accountController, _uiController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(_accountController, _uiController);
                    break;
                case "User Management":
                    next = new SAccManage(_accountController, _uiController);
                    break;
                case "Invoice Management":
                    next = new staffInvoiceList(_accountController, _uiController);
                    break;
            }

            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            proFileController proFileController = new proFileController(_accountController);

            proFileController.setType(_accountController.GetAccountType());

            Form proFile = new proFileMain(_accountController, _uiController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(_accountController, _uiController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About(_accountController, _uiController);
            Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = Location;
            about.Size = Size;
            about.ShowDialog();
            Close();
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
    }
}