﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Windows.Forms;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class UIController : abstractController
    {
        //For DataBase
        private string sqlStr;

        private bool BWMode;
        private bool showbtn1, showbtn2, showbtn3, showbtn4, showbtn5;      //whether the button is visible.
        private static string funbtn1, funbtn2, funbtn3, funbtn4, funbtn5;        //Text in the button.
        private static string AccountType;

        controller.accountController accountController;

        public UIController()
        {
            funbtn1 = funbtn2 = funbtn3 = funbtn4 = funbtn5 = sqlStr = "";
            showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = BWMode = false;
            AccountType = "";
        }

        public UIController(accountController account)
        {
            accountController = account;
            funbtn1 = funbtn2 = funbtn3 = funbtn4 = funbtn5 = sqlStr = "";
            showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = BWMode = false;
            AccountType = "";
        }



        public void setPermission(string UserID)
        {
            if (AccountType.Equals("Customer"))     //Customer
                determineFun("C");
            else       //Staff
            {
                DataTable dt = new DataTable();
                sqlStr = $"SELECT permissionID FROM staff_account_permission SP, staff_account S WHERE SP.staffAccountID = S.staffAccountID AND S.staffID = \'{UserID}\'";
                adr = new MySqlDataAdapter(sqlStr, conn);
                adr.Fill(dt);
                adr.Dispose();
                determineFun(dt.Rows[0]["permissionID"].ToString());
            }

        }
        public void setType(string AccType)
        {
            AccountType = AccType;
        }

        public void setMode(bool value)
        {
            BWMode = value;
        }

        public dynamic getMode()
        {
            if (!BWMode)
            {
                dynamic expando = new ExpandoObject();
                expando.textColor = "#FFFFFF";
                expando.bgColor = "#404040";
                expando.navBarColor = "#008000";
                expando.navColor = "#A0A0A0";
                expando.timeColor = "#A9A9A9";
                expando.locTbColor = "#808080";
                expando.logoutColor = "#FF0000";
                expando.profileColor = "#BDB76B";
                expando.btnColor = "#808080";
                expando.BWmode = true;
                return expando;
            }
            else
            {
                dynamic expando = new ExpandoObject();
                expando.textColor = "#000000";
                expando.bgColor = "#F0F0F0";
                expando.navBarColor = "#3bd5b8";
                expando.navColor = "#B9D1EA";
                expando.timeColor = "#cccccc";
                expando.locTbColor = "#E3E3E3";
                expando.logoutColor = "#ffc0c0";
                expando.profileColor = "#ffffc0";
                expando.btnColor = "#FFFFFF";
                expando.BWmode = false;
                return expando;
            }
        }

        private void determineFun(string permission)
        {
            switch (permission)
            {
                case "C":     //Customer
                    showbtn1 = showbtn2 = showbtn3 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Invoice Management";
                    funbtn3 = "User Managemnet";
                    break;
                case "MP01":     //Sales manager
                    showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Invoice Management";
                    funbtn3 = "On-Sale Product Management";
                    funbtn4 = "Stock Management";
                    funbtn5 = "User Managemnet";
                    break;
                case "MP02":     //Order processing clerk
                    showbtn1 = showbtn2 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "User Managemne";
                    break;
                case "MP03":     //Storeman
                    showbtn1 = showbtn2 = showbtn3 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Stock Management";
                    funbtn3 = "User Managemnet";
                    break;
                case "MP04":     //Department manager
                    showbtn1 = true;
                    funbtn1 = "User Managemnet";
                    break;
                case "MP05":     //Delivery man
                    showbtn1 = true;
                    funbtn1 = "Order Management";
                    showbtn2 = true;
                    funbtn2 = "User Managemne";
                    break;
            }
        }

        public int getIndicator(string btnText)
        {
            int No;
            if (btnText.Equals(funbtn1))
                return 1;
            else if (btnText.Equals(funbtn2))
                return 2;
            else if (btnText.Equals(funbtn3))
                return 3;
            else if (btnText.Equals(funbtn4))
                return 4;
            else
                return 5;
        }

        //Change the information needs to show between customer and staff.
        public dynamic proFile()
        {
            dynamic expando = new ExpandoObject();
            if (AccountType.Equals("Customer"))
            {
                expando.group1 = false;
                expando.group2 = true;
            }
            else
            {
                expando.group1 = true;
                expando.group2 = false;
            }
            return expando;
        }


        public dynamic showFun()
        {
            dynamic expando = new ExpandoObject();
            expando.btn1show = showbtn1;
            expando.btn1value = funbtn1;
            expando.btn2show = showbtn2;
            expando.btn2value = funbtn2;
            expando.btn3show = showbtn3;
            expando.btn3value = funbtn3;
            expando.btn4show = showbtn4;
            expando.btn4value = funbtn4;
            expando.btn5show = showbtn5;
            expando.btn5value = funbtn5;
            return expando;
        }

    }
}
