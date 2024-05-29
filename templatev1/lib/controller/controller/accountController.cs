﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Windows.Forms;

namespace controller
{
    public class accountController : abstractController
    {
        //For DataBase
        private string sqlStr;

        public bool IsLogin;
        private string accountID, firstName, lastName, UserID, AccountType;

        private MySqlCommand cmd;

        controller.UIController UIController;

        public accountController()
        {
            IsLogin = false;
            sqlStr = "";
            accountID = firstName = lastName = UserID = AccountType = "";
        }

        public bool login(string UID, string Pass, controller.UIController UI)
        {
            //try
            //{
                DataTable dt = new DataTable();

                if (UID.StartsWith("LMC"))         //A customer account
                {
                    sqlStr = "SELECT password, status FROM customer_account WHERE customerID = '" + UID + "'";
                }
                else if (UID.StartsWith("LMS"))         //A staff account
                {
                    sqlStr = "SELECT password, status FROM staff_account WHERE staffID = '" + UID + "'";
                }
                else
                    return IsLogin;      //Not a LM account

                adr = new MySqlDataAdapter(sqlStr, conn);
                adr.Fill(dt);
                adr.Dispose();

                if (dt.Rows.Count < 1)          //Account NOT found
                    return IsLogin;
                else if (Pass.Equals(dt.Rows[0]["password"]) && dt.Rows[0]["status"].Equals("active"))          //Account found
                {
                    IsLogin = true;
                    UserID = UID;

                    UIController = UI;

                    UserInfo();
                    UIController.setPermission(UserID);
                }
                return IsLogin;
            //}
            //catch (Exception e)
            //{
            //    return IsLogin;     //Some error occurs retrn false to login
            //}
        }

        private void UserInfo()
        {
            DataTable dt = new DataTable();

            if (UserID.StartsWith("LMC"))         //A customer account
            {
                sqlStr = "SELECT customerAccountID AS accountID, firstName, lastName FROM customer C, customer_account CA WHERE CA.customerID = '" + UserID + "' AND C.customerID = '" + UserID + "'";
                AccountType = "Customer";
                UIController.setType(AccountType);

            }
            else     //A staff account
            {
                sqlStr = "SELECT staffAccountID AS accountID, firstName, lastName FROM staff S, staff_account SA WHERE SA.staffID = '" + UserID + "' AND S.staffID = '" + UserID + "'";
                AccountType = "Staff";
                UIController.setType(AccountType);
            }

            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            conn.Close();

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
            accountID = dt.Rows[0]["accountID"].ToString();
        }

        public string getName()
        {
            return lastName + " " + firstName;
        }

        public string getUID()
        {
            return UserID;
        }

        public string getType()
        {
            return AccountType;
        }


        public void setLog(string Date)
        {
            conn.Open();

            if(UserID.StartsWith("LMC"))
                sqlStr = "INSERT INTO customer_login_history VALUES('" + accountID + "', ' " + Date + "')";
            else
                sqlStr = "INSERT INTO staff_login_history VALUES('" + accountID + "', ' " + Date + "')";

            cmd = new MySqlCommand(sqlStr, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public string getLog()
        {
            DataTable dt = new DataTable();

            if (UserID.StartsWith("LMC"))         //A customer account
            {
                sqlStr = "SELECT loginDate FROM customer_login_history WHERE customerAccountID = '" + accountID + "' ORDER BY loginDATE DESC";
            }
            else
            {
                sqlStr = "SELECT loginDate FROM staff_login_history WHERE staffAccountID = '" + accountID + "' ORDER BY loginDATE DESC";
            }

            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            conn.Close();

            return dt.Rows[0]["loginDate"].ToString();
        }

        public DataTable getStaffDetail(string id) //use in OrderListController     //id = staff account id
        {
            DataTable dt = new DataTable();
            string sqlCmd = $"SELECT staffID FROM staff_account WHERE staffAccountID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string staffID = dt.Rows[0][0].ToString();
            //get detail
            sqlCmd = $"SELECT * FROM staff WHERE staffID = \'{staffID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            dt = new DataTable();
            adr.Fill(dt);
            return dt;
        }

    }
}
