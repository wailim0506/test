﻿using System;
using System.Drawing;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.controller.Utilities;

namespace LMCIS.System_page
{
    public partial class Login : Form
    {
        // loading form
        LoadingForm loadingForm;


        //For install date in about page.
        public static string sysInsDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        private bool IsLogin;

        AccountController accountController;
        UIController UIController;
        RecoveryController recoveryController;

        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            accountController = new AccountController();
            UIController = new UIController(accountController);

            IsLogin = false;
            timer1.Enabled = true;
            tbPassword.PasswordChar = '*';
            rememberMe();
            grpDevTools.Visible = Configuration.IsDevMode();
        }

        //Login the system.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Clean the error message in the label.
            lblUsernameMsg.Text = "";
            lblPasswordMsg.Text = "";

            if (string.IsNullOrEmpty(tbUsername.Text)) //username and password have not been entered.
            {
                lblUsernameMsg.Text = "Please enter your UserID.";
                tbUsername.Select();
            }
            else if (string.IsNullOrEmpty(tbPassword.Text))
            {
                lblPasswordMsg.Text = "Please enter your password.";
                tbPassword.Select();
            }
            else if (string.IsNullOrEmpty(tbPassword.Text)) //password have not been entered.
            {
                lblPasswordMsg.Text = "Please enter your password.";
                tbPassword.Select();
            }
            else if (accountController.Login(tbUsername.Text, tbPassword.Text, UIController)) //Checking the password
            {
                IsLogin = true;
                rememberMe();
                accountController.SetLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //Back to login page
                Form Home = new Home(accountController, UIController);
                Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = Location;
                Home.Size = Size;
                Home.ShowDialog();
                Close();
            }
            else //Password dose not match.
            {
                tbPassword.Clear();
                lblPasswordMsg.Text = "Invalid username or password.";
                tbUsername.Select();
            }
        }

        //For remember me function.
        private void rememberMe()
        {
            if (IsLogin)
            {
                if (chkRememberMe.Checked == true) //Store to local
                {
                    LMCIS.Properties.Settings.Default.Usesrname = tbUsername.Text;
                    LMCIS.Properties.Settings.Default.Password = tbPassword.Text;
                    LMCIS.Properties.Settings.Default.Save();
                }
                else
                    LMCIS.Properties.Settings.Default.Reset(); //Clean local data
            }
            else //Read from local
            {
                if (!string.IsNullOrEmpty(LMCIS.Properties.Settings.Default.Usesrname))
                {
                    chkRememberMe.CheckState = CheckState.Checked;
                    tbUsername.Text = LMCIS.Properties.Settings.Default.Usesrname;
                    tbPassword.Text = LMCIS.Properties.Settings.Default.Password;
                    btnLogin.Select();
                }
            }
        }

        //Forget password.
        private void btnForgetPassword_Click(object sender, EventArgs e)
        {
            recoveryController = new RecoveryController();

            Form PasswordRecovery = new PasswordRe(recoveryController);
            Hide();
            //Swap the current form to another.
            PasswordRecovery.StartPosition = FormStartPosition.Manual;
            PasswordRecovery.Location = Location;
            PasswordRecovery.Size = Size;
            PasswordRecovery.ShowDialog();
            Close();
        }

        //Create a customer account.
        private void btnCreateCustomerAcc_Click(object sender, EventArgs e)
        {
            recoveryController = new RecoveryController();

            Form CreateCustoemrAcc = new CreateCustomerAcc(recoveryController);
            Hide();
            //Swap the current form to another.
            CreateCustoemrAcc.StartPosition = FormStartPosition.Manual;
            CreateCustoemrAcc.Location = Location;
            CreateCustoemrAcc.Size = Size;
            CreateCustoemrAcc.ShowDialog();
            Close();
        }


        //Development Tools
        private void btnTest1_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMC00001";
            tbPassword.Text = "password123";
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00001";
            tbPassword.Text = "password123";
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00006";
            tbPassword.Text = "password123";
        }

        private void btnTest4_Click_1(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00003";
            tbPassword.Text = "password123";
        }

        private void btnTest5_Click_1(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00004";
            tbPassword.Text = "password123";
        }

        private void btnTest6_Click_1(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00009";
            tbPassword.Text = "password123";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMC00002";
            tbPassword.Text = "password123";
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }

        //Set installed date for the about page.
        public static string getInsDate()
        {
            return sysInsDate;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void btnText7_Click(object sender, EventArgs e)
        {
            //Redirect to test tools
            Form testDatabaseAndController = new DevTool();
            //Swap the current form to another.
            testDatabaseAndController.StartPosition = FormStartPosition.Manual;
            testDatabaseAndController.Location = new Point(100, 100);
            testDatabaseAndController.Size = Size;
            testDatabaseAndController.ShowDialog();
        }

        private void OnOnExit()
        {
            // Handle the completion of the loading process here
            // For example, you can remove the LoadingForm from the Form
            this.Controls.Remove(loadingForm);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00012";
            tbPassword.Text = "password123";
        }
    }
}