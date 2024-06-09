﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Windows.Forms;

namespace controller
{
    public class AccountController : abstractController
    {
        public bool IsLogin;
        private string accountID, firstName, lastName, UserID, AccountType;
        private Boolean isLM;
        UIController UIController;
        private Database db;

        public AccountController(Database db = null)
        {
            IsLogin = false;
            isLM = false;
            this.db = db ?? new Database();
        }

        public bool Login(string UID, string Pass, UIController UI)
        {
            try
            {
                var dt = ExecuteSqlQuery(GetAccountDataQuery(UID));

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("Account not found.");
                    return false;
                }

                if (IsPasswordValid(Pass, dt.Rows[0]["password"].ToString()) &&
                    IsAccountActive(dt.Rows[0]["status"].ToString()))
                {
                    SetLoginStatus(UID, UI);
                }

                return IsLogin;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private string GetAccountDataQuery(string UID)
        {
            return UID.StartsWith("LMC") || UID.StartsWith("LMS")
                ? $"SELECT password, status FROM customer_account WHERE customerID = '{UID}' " +
                  $"UNION ALL SELECT password, status FROM staff_account WHERE staffID = '{UID}'"
                : throw new Exception("Not a LM account.");
        }

        // check if the password is valid
        private bool IsPasswordValid(string inputPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
        }

        private bool IsAccountActive(string status)
        {
            return status.Equals("active");
        }

        private void SetLoginStatus(string UID, UIController UI)
        {
            var dt = ExecuteSqlQuery(GetIsLmDataQuery(UID));
            if (dt.Rows[0][0].ToString() == "Y")
            {
                isLM = true;
            }

            IsLogin = true;
            UserID = UID;
            AccountType = GetAccountType(UID);
            UIController = UI;
            UserInfo();
            UIController.SetPermission(UserID);
        }

        private string GetIsLmDataQuery(string UID)
        {
            return $"SELECT isLM from customer_account WHERE customerID = '{UID}'";
        }


        private void UserInfo()
        {
            var dt = ExecuteSqlQuery(GetUserInfoDataQuery(UserID));

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
            accountID = dt.Rows[0]["accountID"].ToString();
        }

        private string GetUserInfoDataQuery(string UID)
        {
            return UID.StartsWith("LMC") || UID.StartsWith("LMS")
                ? $"SELECT customerAccountID AS accountID, firstName, lastName FROM customer C, customer_account CA WHERE CA.customerID = '{UID}' AND C.customerID = '{UID}'"
                : $"SELECT staffAccountID AS accountID, firstName, lastName FROM staff S, staff_account SA WHERE SA.staffID = '{UID}' AND S.staffID = '{UID}'";
        }

        //update the login record into the database.
        public void SetLog(string Date)
        {
            var query = UserID.StartsWith("LMC")
                ? $"INSERT INTO customer_login_history VALUES('{accountID}', '{Date}')"
                : $"INSERT INTO staff_login_history VALUES('{accountID}', '{Date}')";

            db.ExecuteNonQueryCommand(query, null);
        }

        //Return the last login date time.
        public string GetLog()
        {
            var dt = ExecuteSqlQuery(
                $"SELECT loginDate FROM customer_login_history WHERE customerAccountID = '{accountID}' " +
                $"UNION ALL SELECT loginDate FROM staff_login_history WHERE staffAccountID = '{accountID}' ORDER BY loginDATE DESC");

            return dt.Rows[0]["loginDate"].ToString();
        }

        //Return the full login record.
        public DataTable GetFullLog()
        {
            return ExecuteSqlQuery(
                $"SELECT loginDate FROM customer_login_history WHERE customerAccountID = '{accountID}' " +
                $"UNION ALL SELECT loginDate FROM staff_login_history WHERE staffAccountID = '{accountID}' ORDER BY loginDate DESC");
        }

        //Return the last password change date.
        public DateTime GetPwdChange()
        {
            var dt = ExecuteSqlQuery(
                $"SELECT pwdChangeDate FROM customer_account WHERE customerAccountID = '{accountID}' " +
                $"UNION ALL SELECT pwdChangeDate FROM staff_account WHERE staffAccountID = '{accountID}'");

            return (DateTime)dt.Rows[0]["pwdChangeDate"];
        }


        //For the password change function in the profile.
        public bool MatchPwd(string gettedPwd)
        {
            var dt = ExecuteSqlQuery(GetAccountDataQuery(UserID));
            return IsPasswordValid(gettedPwd, dt.Rows[0]["password"].ToString());
        }

        //For customer account disable function.
        public bool DelAccount()
        {
            try
            {
                db.ExecuteNonQueryCommand(
                    $"UPDATE customer_account SET Status = 'disable' WHERE customerAccountID = '{accountID}'", null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetStaffDetail(string id)
        {
            var dt = ExecuteSqlQuery($"SELECT staffID FROM staff_account WHERE staffAccountID = '{id}'");
            var staffID = dt.Rows[0][0].ToString();
            return ExecuteSqlQuery($"SELECT * FROM staff WHERE staffID = '{staffID}'");
        }

        //use in viewOrderController   //id = customerID
        public DataTable GetCustomerDetail(string id)
        {
            return ExecuteSqlQuery($"SELECT * FROM customer WHERE customerID = '{id}'");
        }


        //User's information for UI
        public string GetName()
        {
            return lastName + " " + firstName;
        }

        public string GetUid()
        {
            return UserID;
        }

        public string GetAccountType()
        {
            return AccountType;
        }

        public string GetAccountType(string UID)
        {
            return UID.StartsWith("LMC") || UID.StartsWith("LMS")
                ? "Customer"
                : "Staff";
        }

        public bool GetIsLm()
        {
            return isLM;
        }

        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            return db.ExecuteDataTable(sqlQuery);
        }
    }
}