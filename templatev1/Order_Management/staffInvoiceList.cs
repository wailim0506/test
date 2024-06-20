﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class staffInvoiceList : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        staffInvoiceListController controller;
        Boolean isManager;
        public staffInvoiceList()
        {
            InitializeComponent();
            controller = new staffInvoiceListController();
            lblUid.Text = $"Uid: {UID}";
        }

        public staffInvoiceList(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new staffInvoiceListController();
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void staffInvoiceList_Load(object sender, EventArgs e)
        {
            if (!isManager)
            {
                hideButton();
            }
            cmbStatus.SelectedIndex = 0;
            cmbSorting.SelectedIndex = 0;
            load_data(cmbSorting.Text, cmbStatus.Text);

        }

        public void load_data(string sortBy, string status)
        {
            pnl_Invoice.Controls.Clear();
            DataTable dt = new DataTable();

            switch (sortBy)
            {
                case "Invoice Number (Ascending)":
                    dt = controller.getData(UID, "IN", status, isManager);
                    break;
                case "Invoice Number (Descending)":
                    dt = controller.getData(UID, "INDESC", status, isManager);
                    break;
                case "Order Date (Nearest)":
                    dt = controller.getData(UID, "ODate", status, isManager);
                    break;
                case "Order Date (Furtherest)":
                    dt = controller.getData(UID, "ODateDESC", status, isManager);
                    break;
                case "Delivery Date (Nearest)":
                    dt = controller.getData(UID, "DDate", status, isManager);
                    break;
                case "Delivery Date (Furtherest)":
                    dt = controller.getData(UID, "DDateDESC", status, isManager);
                    break;
                case "Order ID (Ascending)":
                    dt = controller.getData(UID, "OID", status, isManager);
                    break;
                case "Order ID (Descending)":
                    dt = controller.getData(UID, "OIDDESC", status, isManager);
                    break;
                default:
                    dt = controller.getData(UID, "", status, isManager);
                    break;
            }


            int yPosition = 9;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orderDate = dt.Rows[i][3].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                orderDate = d[0];
                string deliveryDate = dt.Rows[i][4].ToString();
                string[]
                    e = deliveryDate.Split(' ');
                deliveryDate = e[0];
                string Invoicestatus = dt.Rows[i][2].ToString();
                if (Invoicestatus == "")
                {
                    Invoicestatus = "Not Confirm";
                }
                else 
                {
                    Invoicestatus = "Confirmed";
                }

                Label lblRowNum = new Label
                {
                    Text = $"{(i + 1)}.",
                    Location = new Point(3, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(50, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblInvoiceNumebr = new Label
                {
                    Text = $"{dt.Rows[i][1]}",
                    Location = new Point(54, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(141, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderID = new Label
                {
                    Name = $"lblOrderID{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(201, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(163, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderDate = new Label
                {
                    Text = $"{orderDate}",
                    Location = new Point(370, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(153, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblDeliveryDate = new Label
                {
                    Text = $"{deliveryDate}",
                    Location = new Point(529, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(152, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblStatus = new Label
                {
                    Text = Invoicestatus,
                    Location = new Point(687, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(151, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View DID",
                    Location = new Point(845, yPosition - 5),
                    Font = new Font("Microsoft Sans Serif", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };

               //btnView.Click += btnView_Click;

                pnl_Invoice.Controls.Add(lblRowNum);
                pnl_Invoice.Controls.Add(lblInvoiceNumebr);
                pnl_Invoice.Controls.Add(lblOrderID);
                pnl_Invoice.Controls.Add(lblOrderDate);
                pnl_Invoice.Controls.Add(lblDeliveryDate);
                pnl_Invoice.Controls.Add(lblStatus);
                pnl_Invoice.Controls.Add(btnView);

                yPosition += 50;

            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbSorting.Text, cmbStatus.Text);
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbSorting.Text, cmbStatus.Text);

        }

        public void hideButton()
        {
            palSelect3.Visible = false;
            btnFunction3.Visible = false;
            palSelect4.Visible = false;
            btnFunction4.Visible = false;
            btnFunction5.Location = new Point(0, 233);
            btnFunction5.Controls.Add(palSelect5);
        }

        
    }
}
