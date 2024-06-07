﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Windows.Forms;

namespace controller
{
    public class RecoveryController : abstractController
    {
        private MySqlCommand cmd;
        private Database db;

        private string UID, email, phone;
        private string sqlStr;

        accountController accountController;

        public RecoveryController()
        {
        }

        public RecoveryController(accountController accountController)
        {
            this.accountController = accountController;
            UID = accountController.getUID();
        }

        //Find the user in the database
        public bool findUser(string UserID, string emailAdd, string phoneNo)
        {
            UID = UserID;
            email = emailAdd;
            phone = phoneNo;

            DataTable dt = new DataTable();
            try
            {
                if (UID.StartsWith("LMC"))
                {
                    sqlStr =
                        $"SELECT * FROM customer WHERE customerID = \'{UID}\' AND (phoneNumber = \'{phone}\' OR emailAddress = \'{email}\')";
                }
                else if (UID.StartsWith("LMS"))
                {
                    sqlStr =
                        $"SELECT * FROM staff WHERE staffID = \'{UID}\' AND (phoneNumber = \'{phone}\' OR emailAddress = \'{email}\')";
                }
                else
                    return false;

                adr = new MySqlDataAdapter(sqlStr, conn);
                adr.Fill(dt);
                adr.Dispose();

                if (dt.Rows.Count == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false; //Some error occurs retrn false to login.
            }
        }

        //Value for listbox.
        public List<string> getcity(string priovince)
        {
            DataTable dt = new DataTable();
            sqlStr = $"SELECT city FROM location WHERE priovince = \'{priovince}\'";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            List<string> city = new List<string>();

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                city.Add(dt.Rows[i]["city"].ToString());
            }

            return city;
        }

        public List<string> getpriovince()
        {
            DataTable dt = new DataTable();
            sqlStr = $"SELECT DISTINCT priovince FROM location";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            List<string> priovince = new List<string>();

            for (int i = 0; i <= (dt.Rows.Count - 1); i++)
            {
                priovince.Add(dt.Rows[i]["priovince"].ToString());
            }

            return priovince;
        }


        public void changPwd(string newPwd)
        {
            string hashedPwd = BCrypt.Net.BCrypt.EnhancedHashPassword(newPwd);


            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            //Generate random key and iv.
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            //Encrypt new password and convert into string.
            var encryptedPwd = Convert.ToBase64String(Encrypt(newPwd, key, iv));
            var strKey = Convert.ToBase64String(key);
            var strIV = Convert.ToBase64String(iv);

            //Update password in database
            conn.Open();
            sqlStr = UID.StartsWith("LMC")
                ? $"UPDATE customer_account SET password = \'{encryptedPwd}\', pwdKEY = \'{strKey}\', " +
                  $"pwdIV = \'{strIV}\', pwdChangeDate = \'{DateTime.Now:yyyy-MM-dd HH:mm:ss}\' " +
                  $"WHERE customerID = \'{UID}\'"
                : $"UPDATE staff_account SET password = \'{encryptedPwd}\', " +
                  $"pwdKEY = \'{strKey}\', pwdIV = \'{strIV}\', " +
                  $"pwdChangeDate = \'{DateTime.Now:yyyy-MM-dd HH:mm:ss}\' " +
                  $"WHERE staffID = \'{UID}\'";

            cmd = new MySqlCommand(sqlStr, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Return the new LMC ID to the create account form.
        public int getLMCID()
        {
            DataTable dt = new DataTable();

            conn.Open();
            sqlStr = "SELECT * FROM customer";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            conn.Close();

            return dt.Rows.Count + 1;
        }

        //For create a new customer accounr.
        public bool create(dynamic Userinfo)
        {
            string hashedPwd = BCrypt.Net.BCrypt.EnhancedHashPassword(Userinfo.pwd);


            byte[] key = new byte[16];
            byte[] iv = new byte[16];


            //Generate random key and iv.
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            //Encrypt new password and convert into string.
            string encryptedPwd = Convert.ToBase64String(Encrypt(Userinfo.pwd, key, iv));
            var strKey = Convert.ToBase64String(key);
            var strIV = Convert.ToBase64String(iv);

            string LMCID = "LMC" + getLMCID().ToString("D5");
            string accountID = "CA" + getLMCID().ToString("D5");

            try
            {
                //Insert a record into customer table
                conn.Open();
                sqlStr =
                    $"INSERT INTO customer VALUES(\'{LMCID}\', \'{Userinfo.fName}\', \'{Userinfo.lName}\', \'{Userinfo.gender}\', \'{Userinfo.email}\', \'{Userinfo.company}\', \'{Userinfo.phone}\'" +
                    $", \'{Userinfo.province}\', \'{Userinfo.city}\', \'{Userinfo.address1}\', \'{Userinfo.address2}\', \'{Userinfo.joinDate}\', \'{Userinfo.payment}\', {Userinfo.IMG} , {Userinfo.dateOfBirth}, NULL)";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();

                //Insert a record into customer_account table
                sqlStr =
                    $"INSERT INTO customer_account VALUES(\'{accountID}\', \'{LMCID}\', 'active', \'{encryptedPwd}\', \'{Userinfo.joinDate}\', \'{strKey}\', \'{strIV}\', \'{Userinfo.joinDate}\')";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();

                //Insert a record into customer_dfadd table set default address.
                sqlStr = $"INSERT INTO customer_dfadd VALUES(\'{LMCID}\', '1')";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                //Delete all created record from table.
                conn.Open();
                sqlStr = $"DELETE FROM customer WHERE customerID = \'{LMCID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();

                sqlStr = $"DELETE FROM customer_account WHERE customerID = \'{LMCID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();

                sqlStr = $"DELETE FROM customer_dfadd WHERE customerID = \'{LMCID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return false; //Something went wrong.
            }
        }

        //Encrypt the password when recovery the password or create a new customer account.
        private byte[] Encrypt(string simpletext, byte[] key, byte[] iv)
        {
            byte[] cipheredtext;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                           new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(simpletext);
                        }

                        cipheredtext = memoryStream.ToArray();
                    }
                }
            }

            return cipheredtext;
        }

        //Check whether the email or phone has registered an account.
        public bool checkEmailPhone(string data)
        {
            DataTable dt = new DataTable();
            sqlStr =
                $"SELECT emailAddress, phoneNumber FROM customer C, customer_account CA WHERE Status = 'active' AND c.customerID = CA.customerID AND (phoneNumber = \'{data}\' OR emailAddress = \'{data}\') " +
                $"UNION ALL SELECT emailAddress, phoneNumber FROM staff S, staff_account SA WHERE status = 'active' AND s.staffID = sa.staffID AND(phoneNumber = \'{data}\' OR emailAddress = \'{data}\');";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            if (dt.Rows.Count >= 1)
                return false;
            else
                return true;
        }
    }
}