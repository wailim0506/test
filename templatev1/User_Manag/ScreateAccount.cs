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
using templatev1.Online_Ordering_Platform;

namespace templatev1
{
    public partial class ScreateAccount : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        UserController UserController;

        public ScreateAccount(AccountController accountController, UIController UIController, UserController userController)
        {
            InitializeComponent();
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            this.accountController = accountController;
            this.UIController = UIController;
            this.UserController = userController;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Initialization()
        {
            setIndicator(UIController.getIndicator("User Management"));
            timer1.Enabled = true;
            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            lblFormUID.Text = "LMS" + UserController.getLMSID().ToString("D5");
            lblCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            tbPass.PasswordChar = tbConfirmPass.PasswordChar = '*';
            cmbDept.Items.AddRange(UserController.GetDept().ToArray());
            cmbJobTitle.Items.AddRange(UserController.GetJobTitle().ToArray());


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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form UserMgmt = new SAccManage(accountController, UIController);
            Hide();
            UserMgmt.StartPosition = FormStartPosition.Manual;
            UserMgmt.Location = Location;
            UserMgmt.Size = Size;
            UserMgmt.ShowDialog();
            Close();
        }

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
                //my version
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
                    }
                    else
                    {
                        next = new Order_Management.staffOrderList(accountController, UIController);
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
                //my version
                case "Invoice Management":


                    break;
                case "On-Sale Product Management":


                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
                    next = new SAccManage(accountController, UIController);
                    break;
            }

            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
          //  if (checkInfo()) //Pass to controller and create account
            {
              //  setValue(); //If passed set the value in to dynameic.
               // if (recoveryController.create(value))
              //  {
                   // MessageBox.Show(
                    // "Create account success! Your UID is LMC" + (recoveryController.getLMCID() - 1).ToString("D5") +
                   // ".\nThe system will redirect to the login page.", "System message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
              //  else
             //   {
            //        MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK,
            //            MessageBoxIcon.Warning);
              //  }
            }
        }

        private bool checkInfo()
        {
            //Clean previous error message.
            lblfNameMsg.Text = lblLNameMsg.Text = lblSexMsg.Text = lblJobMsg.Text = lblDateMsg.Text =
                lblPhoneMsg.Text = lblEmailMsg.Text = lblDeptMsg.Text = lblPwdMsg.Text = "";

            //Check firstName
            if (string.IsNullOrEmpty(tbFirstName.Text))
            {
                lblfNameMsg.Text = "Please enter firstName.";
                tbFirstName.Select();
                return false;
            }
            else if (tbFirstName.Text.Length < 2 || tbFirstName.Text.Length > 20)
            {
                lblfNameMsg.Text = "FirstName too short or too long, minimum 2 maximum 20.";
                tbFirstName.Select();
                return false;
            }

            //Check lastName
            if (string.IsNullOrEmpty(tbLastName.Text))
            {
                lblLNameMsg.Text = "Please enter lastName.";
                tbLastName.Select();
                return false;
            }
            else if (tbLastName.Text.Length < 2 || tbLastName.Text.Length > 20)
            {
                lblLNameMsg.Text = "LastName too short or too long, minimum 2 maximum 20.";
                tbLastName.Select();
                return false;
            }

            //Check gender
            if (cmbGender.SelectedItem == null)
            {
                lblSexMsg.Text = "Please select a gender.";
                cmbGender.Select();
                return false;
            }

            //Check jobtitle
            if (cmbJobTitle.SelectedItem == null)
            {
                lblJobMsg.Text = "Please select a gender.";
                cmbJobTitle.Select();
                return false;
            }

            //Check deptment
            if (cmbDept.SelectedItem == null)
            {
                lblDeptMsg.Text = "Please select a gender.";
                cmbDept.Select();
                return false;
            }

            //Check date of birth, MUST today > selected value > age 17.
            if (dtpDateOfBirth.Value.Date == DateTime.Now.Date)
            {
                lblDateMsg.Text = "Please select the date";
                return false;
            }
            else if ((dtpDateOfBirth.Value.Date > DateTime.Now.Date ||
                                                   dtpDateOfBirth.Value.Date > new DateTime(2007, 1, 1)))
            {
                lblDateMsg.Text = "Please select a valid date.";
                return false;
            }

            //Check phone number
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                lblPhoneMsg.Text = "Please enter the phone number.";
                tbPhone.Select();
                return false;
            }
            else if (tbPhone.Text.Length != 11)
            {
                lblPhoneMsg.Text = "Please enter the correct format.";
                tbPhone.Select();
                return false;
            }
            else if (!UserController.CheckEmailPhone(tbPhone.Text))
            {
                lblPhoneMsg.Text = "The phone number has already registered an account.";
                tbPhone.Select();
                return false;
            }

            //Check email address
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                lblEmailMsg.Text = "Please enter the email address.";
                tbEmail.Select();
                return false;
            }
            else if (!IsValidEmail(tbEmail.Text) || tbEmail.Text.Length > 30)
            {
                lblEmailMsg.Text = "Please enter the correct format.";
                tbEmail.Select();
                return false;
            }
            else if (!UserController.CheckEmailPhone(tbEmail.Text))
            {
                lblEmailMsg.Text = "The email address has already registered an account.";
                tbEmail.Select();
                return false;
            }

            //Check whether the password and confirm the password match.
            if (string.IsNullOrEmpty(tbPass.Text))
            {
                lblPwdMsg.Text = "Please enter the password.";
                tbPass.Select();
                return false;
            }
            else if (tbPass.Text.Length < 10 || tbPass.Text.Length > 50)
            {
                lblPwdMsg.Text = "Password too short or too long, minimum 10 maximum 50.";
                tbPass.Select();
                return false;
            }
            else if (!tbPass.Text.Equals(tbConfirmPass.Text))
            {
                lblPwdMsg.Text = "Confirm password does NOT match.";
                tbConfirmPass.Select();
                return false;
            }

            return true;
        }

        //Check the email address.
        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}