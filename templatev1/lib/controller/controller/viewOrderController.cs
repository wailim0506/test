﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class viewOrderController : abstractController 
    {
        string sqlCmd;
        public viewOrderController()
        {
            sqlCmd = "";
        }

        public DataTable getOrder(string id) //orderID
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM order_ WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public string getStafftID(string id) //staff account id
        {
            accountController ac = new accountController();
            DataTable dt = ac.getStaffDetail(id);
            return dt.Rows[0][0].ToString();
        }

        public string getStaffName(string id) //staff account id
        {
            accountController ac = new accountController();
            DataTable dt = ac.getStaffDetail(id);
            return $"{dt.Rows[0][2].ToString()} {dt.Rows[0][3].ToString()}";
        }

        public string getStaffContact(string id)
        {
            accountController ac = new accountController();
            DataTable dt = ac.getStaffDetail(id);
            return dt.Rows[0][6].ToString();
        }

        public DataTable getOrderedSparePart(string id) //orderID
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM order_line WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public string getItemNum(string id) //part number
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
            
        }

        public string getPartName(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT name FROM spare_part WHERE partNumber = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getShippingAddress(string id) //customerID
        {
            accountController ac = new accountController();
            DataTable dt = ac.getCustomerDetail(id);
            return $"{dt.Rows[0][10].ToString()}, {dt.Rows[0][7].ToString()}, {dt.Rows[0][8].ToString()}"; 
        }

        public DataTable getShippingDetail(string id) //orderID
        { //orderID
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM shipping_detail WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public string[] getDelivermanDetail(string id) //orderID
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT delivermanID FROM shipping_detail WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string delivermanID = dt.Rows[0][0].ToString();

            //get deliverman name and contact from staff table
            dt = new DataTable();
            sqlCmd = $"SELECT firstName, lastName, phoneNumber FROM staff WHERE delivermanID = \'{delivermanID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string[] delivermanDetail = new string[3];
            for (int i = 0; i < delivermanDetail.Length; ++i)
            {
                delivermanDetail[i] = dt.Rows[0][i].ToString();
            }
            return delivermanDetail;
        }

       

        public Boolean deleteOrder(string id) //order id
        {
            string sqlCmd = "DELETE FROM invoice WHERE orderID = @id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            sqlCmd = "DELETE FROM feedback WHERE orderID = @id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            sqlCmd = "UPDATE shipping_detail SET remark = 'Cancelled' WHERE orderID = @id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            sqlCmd = "DELETE FROM instruction WHERE orderID = @id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            //keep record
            //sqlCmd = "DELETE FROM order_line WHERE orderID = @id";
            //try
            //{
            //    using (MySqlConnection connection = new MySqlConnection(connString))
            //    {
            //        connection.Open();

            //        using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
            //        {
            //            command.Parameters.AddWithValue("@id", id);

            //            command.ExecuteNonQuery();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            //finally
            //{
            //    conn.Close();
            //}

            sqlCmd = "UPDATE order_ SET status = @status WHERE orderID = @id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@status", "Cancelled");
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }


            return true;
        }


        public Dictionary<string, int> getPartNumWithQty(string id) //order id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT partNumber, quantity FROM order_line WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);

            Dictionary<string, int> partNumQty = new Dictionary<string, int>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                partNumQty.Add($"{dt.Rows[i][0]}", int.Parse(dt.Rows[i][1].ToString()));
            }
            return partNumQty;
        }

        public void addQtyback(string partNum, int qtyInOrder)
        {
            cartController c = new cartController();
            c.addQtyBack(partNum, qtyInOrder, 0);
        }

        public Boolean reOrder(string id, string partNum, int qty) //customer id, part number, quantity
        {
            spareListController c = new spareListController();
            return c.addCart(id, partNum, qty);
        }
    }
}
