﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Order_Management
{
    public partial class customerViewOrder : Form
    {
        controller.accountController accountController;
        controller.UIController UIController;
        controller.viewOrderController controller;
        private string uName, UID;
        string orderID;
        string shipDate;
        public customerViewOrder()
        {
            InitializeComponent();
            controller = new controller.viewOrderController();
        }

        public customerViewOrder(string orderID, controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            this.controller = new controller.viewOrderController();
            this.shipDate = "";
            //UID = this.accountController.getUID();
            

            UID = "LMC00001"; //hard code for testing
            //UID = "LMC00003"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        

        private void customerViewOrder_Load(object sender, EventArgs e)
        {
            
            timer1.Enabled = true; 
            lblLoc.Text += $" {orderID.ToString()}";
            DataTable dt =  controller.getOrder(orderID);

            //order basic info
            Label lblID = new Label() { Name = $"lblID", Text = orderID, Location = new System.Drawing.Point(381,123), Font = new Font("Microsoft Sans Serif", 12),AutoSize = true };
            Label lblSerialNum = new Label() { Name = $"lblSerialNum", Text = $"{dt.Rows[0][3]}", Location = new System.Drawing.Point(381, 170), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true };
            Label lblDate = new Label() { Name = $"lblDate", Text = $"{dt.Rows[0][4]}", Location = new System.Drawing.Point(381, 217), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true };
            Label lblStaffName = new Label() { Name = $"lblStaffName", Text = $"{controller.getStaffName(dt.Rows[0][2].ToString())}", Location = new System.Drawing.Point(381, 264), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true };
            Label lblStaffID = new Label() { Name = $"lblStaffID", Text = $"{controller.getStafftID(dt.Rows[0][2].ToString())}", Location = new System.Drawing.Point(381, 311), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true, TextAlign = ContentAlignment.MiddleCenter };
            Label lblStaffContact = new Label() { Name = $"lblStaffContact", Text = $"{controller.getStaffContact(dt.Rows[0][2].ToString())}", Location = new System.Drawing.Point(381, 358), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true};
            Label lblStatus = new Label() { Name = $"lblStatus", Text = $"{dt.Rows[0][6]}", Location = new System.Drawing.Point(381, 405), Font = new Font("Microsoft Sans Serif", 12),AutoSize = true };

            this.Controls.Add(lblID);
            this.Controls.Add(lblSerialNum);
            this.Controls.Add(lblDate);
            this.Controls.Add(lblStaffName);
            this.Controls.Add(lblStaffID);
            this.Controls.Add(lblStaffContact);
            this.Controls.Add(lblStatus);

            //delivery info
            dt = new DataTable();
            dt = controller.getShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[] d = shippingDate.Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            shipDate = shippingDate;
            string[] delivermanDetail = controller.getDelivermanDetail(orderID);
            lblDelivermanID.Text = dt.Rows[0][1].ToString();
            lblDelivermanName.Text = $"{delivermanDetail[0]} {delivermanDetail[1]}";
            lblDelivermanContact.Text = delivermanDetail[2];            
            lblShippingDate.Text = $"{shippingDate}";         
            lblExpressNum.Text = dt.Rows[0][4].ToString();
            lblShippingAddress.Text = controller.getShippingAddress(UID);

            //ordered spare part
            dt = new DataTable();
            dt = controller.getOrderedSparePart(orderID);
            int row = dt.Rows.Count;

            
            int rowPosition = 8;
            int orderTotalPrice = 0;
            for (int i = 1; i <= row; i++)
            {
                Label lblRowNum = new Label() { Name = $"lblRowNum{i}", Text = $"{i.ToString()}.", Location = new System.Drawing.Point(3, rowPosition), Font = new Font("Microsoft Sans Serif", 12), TextAlign = ContentAlignment.MiddleCenter, Size = new System.Drawing.Size(30, 20) };
                Label lblItemNum = new Label() { Name = $"lblItemNum{i}", Text = $"{controller.getItemNum(dt.Rows[i - 1][0].ToString())}", Location = new System.Drawing.Point(38, rowPosition), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(83, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblPartNum = new Label() { Name = $"lblPartNum{i}", Text = $"{dt.Rows[i - 1][0].ToString()}", Location = new System.Drawing.Point(127, rowPosition), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(97, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblPartName = new Label() { Name = $"lblPartName{i}", Text = $"{controller.getPartName(dt.Rows[i - 1][0].ToString())}", Location = new System.Drawing.Point(230, rowPosition), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(300, 20),TextAlign = ContentAlignment.MiddleCenter };
                Label lblQuantity = new Label() { Name = $"lblQuantity{i}", Text = $"{dt.Rows[i - 1][2].ToString()}", Location = new System.Drawing.Point(536, rowPosition), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(106, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblUnitPrice = new Label() { Name = $"lblUnitPrice{i}", Text = $"¥{dt.Rows[i - 1][3].ToString()}", Location = new System.Drawing.Point(648, rowPosition), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(144, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblRowTotalPrice = new Label() { Name = $"lblRowTotalPrice{i}", Text = $"¥{(int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString())).ToString()}", Location = new System.Drawing.Point(798, rowPosition), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(114, 20), TextAlign = ContentAlignment.MiddleCenter };
                
                
                rowPosition += 50;
                orderTotalPrice += (int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString()));
                lblOrderTotalPrice.Text = $"¥ { orderTotalPrice.ToString()}";

                pnlSP.Controls.Add(lblRowNum);
                pnlSP.Controls.Add(lblItemNum);
                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);              
                pnlSP.Controls.Add(lblQuantity);
                pnlSP.Controls.Add(lblUnitPrice);
                pnlSP.Controls.Add(lblRowTotalPrice);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss"); 
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form customerEditOrder = new customerEditOrder(orderID,accountController, UIController);
            this.Hide();
            customerEditOrder.StartPosition = FormStartPosition.Manual;
            customerEditOrder.Location = this.Location;
            customerEditOrder.ShowDialog();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete order {orderID} ?\nYour action cannot be revoked after confirming it.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes && controller.deleteOrder(orderID))
            {
                MessageBox.Show("Delete successful.", " Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form customerOrderList = new Online_Ordering_Platform.customerOrderList(accountController, UIController);
                this.Hide();
                customerOrderList.StartPosition = FormStartPosition.Manual;
                customerOrderList.Location = this.Location;
                customerOrderList.ShowDialog();
                this.Close();
            }
            else if (dialogResult == DialogResult.Yes && !controller.deleteOrder(orderID))
            {
                MessageBox.Show("Something went wrong.\nPlease contact our staff for help", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form customerOrderList = new Online_Ordering_Platform.customerOrderList(accountController, UIController);
            this.Hide();
            customerOrderList.StartPosition = FormStartPosition.Manual;
            customerOrderList.Location = this.Location;
            customerOrderList.ShowDialog();
            this.Close();
        }

        private void /*Boolean*/ twoDayBefore()
        {

        }
    }
}