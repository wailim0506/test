﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class deliverman : Form
    {
        AccountController accountController;
        UIController UIController;
        delivermanOrderListController controller;
        private string uName, UID;

        public deliverman()
        {
            InitializeComponent();
            lblUid.Text = $"Uid: {UID}";
            controller = new delivermanOrderListController();
        }

        public deliverman(AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new delivermanOrderListController();
            UID = this.accountController.GetUid();


            lblUid.Text = $"Uid: {UID}";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
            cmbUpcomingSorting.SelectedIndex = 0;
            cmbFinishedSorting.SelectedIndex = 0;
            load_data(cmbUpcomingSorting.Text.ToString(), cmbUpcomingSorting.Text.ToString());
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

        public void load_data(string sortBy1, string sortBy2)
        {
            //upcoming job
            pnlUpJob.Controls.Clear();
            DataTable dt = controller.getAllOrder(UID, $"{sortBy1}");

            int yPosition = 9;

            //create label
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lblRowNum = new Label
                {
                    Text = $"{(i + 1)}{"."} ",
                    Location = new Point(10, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(30, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderID = new Label
                {
                    Name = $"lblOrderID{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(46, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(129, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };


                string orderDate = dt.Rows[i][2].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                orderDate = d[0];

                Label lblDate = new Label
                {
                    Name = $"lblDate{i}",
                    Text = $"{orderDate}",
                    Location = new Point(181, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(150, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblAddress = new Label
                {
                    Name = $"lblAddress{i}",
                    Text = $"{dt.Rows[i][5]}",
                    Location = new Point(337, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(489, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View Order",
                    Location = new Point(832, yPosition - 4),
                    Font = new Font("Times New Roman", 13),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;
                pnlUpJob.Controls.Add(lblRowNum);
                pnlUpJob.Controls.Add(lblOrderID);
                pnlUpJob.Controls.Add(lblDate);
                pnlUpJob.Controls.Add(lblAddress);
                pnlUpJob.Controls.Add(btnView);


                yPosition += 50;
            }

            yPosition = 9;

            //finished job
            pnlFinishedJob.Controls.Clear();
            dt = controller.GetAllFinishedOrder(UID, $"{sortBy2}");

            //create label
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lblRowNum = new Label
                {
                    Text = $"{(i + 1)}{"."} ",
                    Location = new Point(10, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(30, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderID = new Label
                {
                    Name = $"lblOrderID{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(46, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(129, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };


                string orderDate = dt.Rows[i][2].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                orderDate = d[0];

                Label lblDate = new Label
                {
                    Name = $"lblDate{i}",
                    Text = $"{orderDate}",
                    Location = new Point(181, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(150, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblAddress = new Label
                {
                    Name = $"lblAddress{i}",
                    Text = $"{dt.Rows[i][5]}",
                    Location = new Point(337, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(489, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View Order",
                    Location = new Point(832, yPosition - 4),
                    Font = new Font("Times New Roman", 13),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click_Finished;
                pnlFinishedJob.Controls.Add(lblRowNum);
                pnlFinishedJob.Controls.Add(lblOrderID);
                pnlFinishedJob.Controls.Add(lblDate);
                pnlFinishedJob.Controls.Add(lblAddress);
                pnlFinishedJob.Controls.Add(btnView);


                yPosition += 50;
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

                    foreach (Control control in pnlUpJob.Controls)
                    {
                        if (control.Name == $"lblOrderID{index}")
                        {
                            Form delivermanViewOrder =
                                 new delivermanViewOrder(control.Text, accountController, UIController);
                            Hide();
                            delivermanViewOrder.StartPosition = FormStartPosition.Manual;
                            delivermanViewOrder.Location = Location;
                            delivermanViewOrder.ShowDialog();
                            Close();
                            return;
                        }

                        ++i;
                    }
                }
            }
        }

        public void btnView_Click_Finished(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int index = getIndex(buttonName);
                if (index != -1)
                {
                    int i = 0;

                    foreach (Control control in pnlFinishedJob.Controls)
                    {
                        if (control.Name == $"lblOrderID{index}")
                        {
                            Form delivermanViewOrder =
                                new delivermanViewOrder(control.Text, accountController, UIController);
                            Hide();
                            delivermanViewOrder.StartPosition = FormStartPosition.Manual;
                            delivermanViewOrder.Location = Location;
                            delivermanViewOrder.ShowDialog();
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

        private void cmbUpcomingSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbUpcomingSorting.Text.ToString(), cmbFinishedSorting.Text.ToString());
        }

        private void cmbFinishedSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbUpcomingSorting.Text.ToString(), cmbFinishedSorting.Text.ToString());
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o = new deliverman(accountController,UIController);
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

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            Form proFile = new SAccManage(accountController, UIController);
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