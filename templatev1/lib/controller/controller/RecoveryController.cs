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
        private string UID, email, phone;
        private string sqlStr;
        private MySqlCommand cmd;

        public RecoveryController()
        {
            sqlStr = "";
            UID = email = phone = "";
        }

        public bool findUser(string UserID, string emailAdd, string phoneNo)
        {
            UID = UserID;
            email = emailAdd;
            phone = phoneNo;


            DataTable dt = new DataTable();

            if (UID.StartsWith("LMC"))
            {
                sqlStr = $"SELECT * FROM customer WHERE customerID = \'{UID}\' AND (phoneNumber = \'{phone}\' OR emailAddress = \'{email}\')";
            }
            else if (UID.StartsWith("LMS"))
            {
                sqlStr = $"SELECT * FROM staff WHERE staffID = \'{UID}\' AND (phoneNumber = \'{phone}\' OR emailAddress = \'{email}\')";
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

        public void changPwd(string newPwd)
        {
            string encryptedPwd, strKey, strIV;
            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            //Generate random key and iv.
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            //Encrypt new password and convert into string.
            encryptedPwd = Convert.ToBase64String(Encrypt(newPwd, key, iv));
            strKey = Convert.ToBase64String(key);
            strIV = Convert.ToBase64String(iv);

            //Update password in database
            conn.Open();
            if (UID.StartsWith("LMC"))
                sqlStr = $"UPDATE customer_account SET password = \'{encryptedPwd}\', pwdKEY = \'{strKey}\', pwdIV = \'{strIV}\', pwdChangeDate = \'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\' WHERE customerID = \'{UID}\'";
            else
            {
                sqlStr = $"UPDATE staff_account SET password = \'{encryptedPwd}\', pwdKEY = \'{strKey}\', pwdIV = \'{strIV}\', pwdChangeDate = \'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\' WHERE staffID = \'{UID}\'";
            }
            cmd = new MySqlCommand(sqlStr, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

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

        static byte[] Encrypt(string simpletext, byte[] key, byte[] iv)
        {
            byte[] cipheredtext;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
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

    }
}