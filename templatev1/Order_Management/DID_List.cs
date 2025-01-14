﻿using System;
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
    public partial class DID_List : Form
    {
        AccountController accountController;
        UIController UIController;
        didListController controller;
        private string uName, UID;
        string orderID;
        bool isManager;

        public DID_List(string orderID)
        {
            InitializeComponent();
            controller = new didListController();
            this.orderID = orderID;
            isManager = false;
        }

        public DID_List(string orderID, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new didListController();
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            lblLoc.Text += $" {orderID}";
            lblHeading.Text += $" {orderID}";
            isManager = accountController.CheckIsManager();
        }


        private void DID_List_Load(object sender, EventArgs e)
        {
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));

            timer1.Enabled = true;
            cmbSorting.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            load_data(cmbCategory.Text, cmbSorting.Text);
        }

        public void load_data(string category, string sortBy)
        {
            pnlPartList.Controls.Clear();
            DataTable dt = controller.getData(category, sortBy, orderID);

            int yPosition = 8;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lblRowNum = new Label
                {
                    Text = $"{(i + 1)}.",
                    Location = new Point(3, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(41, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblCategory = new Label
                {
                    Text = $"{dt.Rows[i][3]}",
                    Location = new Point(42, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(86, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblPartNum = new Label
                {
                    Name = $"lblPartNum{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(143, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(128, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblPartName = new Label
                {
                    Text = $"{dt.Rows[i][1]}",
                    Location = new Point(285, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(242, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblQty = new Label
                {
                    Text = $"{dt.Rows[i][2]}",
                    Location = new Point(542, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(94, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View DID",
                    Location = new Point(647, yPosition - 2),
                    Font = new Font("Times New Roman", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(116, 25),
                    Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;

                yPosition += 50;

                pnlPartList.Controls.Add(lblRowNum);
                pnlPartList.Controls.Add(lblCategory);
                pnlPartList.Controls.Add(lblPartNum);
                pnlPartList.Controls.Add(lblPartName);
                pnlPartList.Controls.Add(lblQty);
                pnlPartList.Controls.Add(btnView);
            }
        }

        public void btnView_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int index = getIndex(buttonName);
                if (index != -1)
                {
                    int i = 0;

                    foreach (Control control in pnlPartList.Controls)
                    {
                        if (control.Name == $"lblPartNum{index}")
                        {
                            Form o =
                                new DID(orderID, control.Text, accountController, UIController);
                            Hide();
                            o.StartPosition = FormStartPosition.Manual;
                            o.Location = Location;
                            o.ShowDialog();
                            Close();
                            return;
                        }

                        ++i;
                    }
                }
            }
        }

        private int getIndex(string btnName)
        {
            int i = 0;
            while (true)
            {
                if (btnName == $"btnView{i}")
                {
                    return i;
                }

                i++;
            }
        }

        public void hideButton()
        {
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
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form o = new staffViewOrder(orderID, accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbCategory.Text, cmbSorting.Text);
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbCategory.Text, cmbSorting.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

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
            proFileController proFileController = new proFileController(accountController);

            proFileController.setType(accountController.GetAccountType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About(accountController, UIController);
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