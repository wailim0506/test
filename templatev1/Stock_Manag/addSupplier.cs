﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Stock_Manag
{
    public partial class addSupplier : Form
    {
        controller.supplierController controller;
        private string uName, UID;
        controller.accountController accountController;
        controller.UIController UIController;

        public addSupplier()
        {
            InitializeComponent();
            controller = new controller.supplierController();
        }

        public addSupplier(controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.supplierController();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (controller.addSupplier(lblSupplierNumber.Text.ToString(), tbName.Text.ToString(),
                    tbPhone.Text.ToString(), tbAddress.Text.ToString(), cmbCountry.Text.ToString()) &&
                tbName.Text.ToString() != "" && tbPhone.Text.ToString() != "" && tbAddress.Text.ToString() != "" &&
                cmbCountry.Text.ToString() != "")
            {
                MessageBox.Show("Supplier added.");
                Form viewSupplier = new viewSupplier();
                this.Hide();
                viewSupplier.StartPosition = FormStartPosition.Manual;
                viewSupplier.Location = this.Location;
                viewSupplier.ShowDialog();
                this.Close();
            }
            else
            {
                if (tbName.Text.ToString() == "" || tbPhone.Text.ToString() == "" || tbAddress.Text.ToString() == "" ||
                    cmbCountry.Text.ToString() == "")
                {
                    MessageBox.Show("Please don't leave blank");
                }
                else
                {
                    MessageBox.Show("Please try again.");
                }
            }
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int countCountry = controller.getSupplierNumFromSameCountry(cmbCountry.Text.ToString());
            countCountry++;
            string countryCode = controller.getCountryCode(cmbCountry.Text.ToString());
            string id = $"SID{countryCode}";
            id += $"{countCountry:D5}";
            lblSupplierNumber.Text = id;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show("All unsaved change will be lost!\nAre you sure you want to cancel editing?",
                    "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Form viewSupplier = new viewSupplier();
                this.Hide();
                viewSupplier.StartPosition = FormStartPosition.Manual;
                viewSupplier.Location = this.Location;
                viewSupplier.ShowDialog();
                this.Close();
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void addSupplier_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //lblUid.Text = $"Uid: {accountController.getUID()}";  //not linked yet
        }
    }
}