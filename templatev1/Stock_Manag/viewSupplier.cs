﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;

namespace templatev1.Stock_Manag
{
    public partial class viewSupplier : Form
    {
        DataTable dt;
        supplierController controller;
        private string uName, UID;
        AccountController accountController;
        UIController UIController;

        public viewSupplier()
        {
            InitializeComponent();
            controller = new supplierController();
        }

        public viewSupplier(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            controller = new supplierController();
            this.accountController = accountController;
            this.UIController = UIController;
        }

        private void viewSupplier_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //lblUid.Text = $"Uid: {accountController.getUID()}";  //not linked yet
            int numOfSupplier = controller.CountSupplier();
            dt = controller.GetSupplierList();

            //create label 
            int yPosition = 15;
            for (int i = 1; i <= numOfSupplier; i++)
            {
                Label lblID = new Label
                {
                    Name = $"lblID{i}", Text = $"{dt.Rows[i - 1][0]}",
                    Location = new Point(15, yPosition), Font = new Font("Microsoft Sans Serif", 11)
                };
                Label lblName = new Label
                {
                    Name = $"lblName{i}", Text = $"{dt.Rows[i - 1][1]}",
                    Location = new Point(152, yPosition), Font = new Font("Microsoft Sans Serif", 11),
                    Size = new Size(180, 50)
                };
                Label lblPhone = new Label
                {
                    Name = $"lblPhone{i}", Text = $"{dt.Rows[i - 1][2]}",
                    Location = new Point(341, yPosition), Font = new Font("Microsoft Sans Serif", 11),
                    Size = new Size(150, 50)
                };
                Label lblAddress = new Label
                {
                    Name = $"lblAddress{i}", Text = $"{dt.Rows[i - 1][3]}",
                    Location = new Point(510, yPosition), Font = new Font("Microsoft Sans Serif", 11),
                    Size = new Size(180, 50)
                };
                Label lblCountry = new Label
                {
                    Name = $"lblCountry{i}", Text = $"{dt.Rows[i - 1][4]}",
                    Location = new Point(716, yPosition - 7),
                    Font = new Font("Microsoft Sans Serif", 11), Size = new Size(150, 35),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                RadioButton radioButton = new RadioButton
                {
                    Name = $"radioButton{i}", Text = "", Location = new Point(873, yPosition - 1),
                    BackColor = Color.Transparent, Size = new Size(14, 17)
                };

                grpSupplier.Controls.Add(lblID);
                grpSupplier.Controls.Add(lblName);
                grpSupplier.Controls.Add(lblPhone);
                grpSupplier.Controls.Add(lblAddress);
                grpSupplier.Controls.Add(lblCountry);
                grpSupplier.Controls.Add(radioButton);
                yPosition += 50;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Label label = new Label();
            int index = getIndex(); //get which radio is checked first
            if (index != -1)
            {
                foreach (Control control in grpSupplier.Controls)
                {
                    if (control.Name == $"lblID{index}")
                    {
                        label = (Label)control;
                        Form editSupplier = new editSupplier(control.Text, accountController, UIController);
                        Hide();
                        editSupplier.StartPosition = FormStartPosition.Manual;
                        editSupplier.Location = Location;
                        editSupplier.ShowDialog();
                        Close();
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select one supplier.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Label label = new Label();
            int index = getIndex(); //get which radio is checked first
            foreach (Control control in grpSupplier.Controls)
            {
                if (control.Name == $"lblID{index}")
                {
                    label = (Label)control;
                    Boolean del = controller.DeleteSupplier(label.Text);
                    if (del)
                    {
                        Form viewSupplier = new viewSupplier();
                        Hide();
                        viewSupplier.StartPosition = FormStartPosition.Manual;
                        viewSupplier.Location = Location;
                        viewSupplier.Size = Size;
                        viewSupplier.ShowDialog();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "It seem like there are still have spare part using this supplier.\nPLease check again.");
                    }
                }
            }
        }

        private int getIndex()
        {
            RadioButton radioButton;
            int i = 1;
            foreach (Control control in
                     grpSupplier
                         .Controls) //find which radio button is checked                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
            {
                if (control.Name == $"radioButton{i}")
                {
                    radioButton = (RadioButton)control;
                    if (radioButton.Checked)
                    {
                        return i;
                    }

                    i++;
                }
            }

            int x = -1;
            return x;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form addSupplier = new addSupplier(accountController, UIController);
            Hide();
            addSupplier.StartPosition = FormStartPosition.Manual;
            addSupplier.Location = Location;
            addSupplier.ShowDialog();
            Close();
        }
    }
}