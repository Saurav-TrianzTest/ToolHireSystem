using Microsoft.Data.SqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ToolHireSystem
{
    class Validator
    {
        public static Boolean ValType(string type)
        {

            Regex typeChk = new("^[a-zA-Z ]*$");

            if (typeChk.IsMatch(type))
            {
                return true;
            }

            else
            {
                MessageBox.Show("Type must be letters only!");
                return false;

            }
        }

        public static Boolean ValDesc(string desc)
        {
            Regex descChk = new("^[a-zA-Z. ]*$");

            if (descChk.IsMatch(desc))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Description must be letters and '.' only!");
                return false;
            }
        }

        public static Boolean ValPrice(decimal price)
        {
            if (price <= 0)
            {
                MessageBox.Show("Price must be greater than zero!");
                return false;
            }
            else
            {

                return true;
            }
        }

        public static Boolean Valname(string name)
        {
            Regex typeChk = new("^[a-zA-Z ]*$");

            if (!typeChk.IsMatch(name) || name == "")
            {
                MessageBox.Show("Forename must be letters only!");
                return false;
            }

            else
            {
                return true;
            }
        }

        public static Boolean ValLastName(string name)
        {
            Regex typeChk = new("^[a-zA-Z ]*$");

            if (!typeChk.IsMatch(name) || name == "")
            {
                MessageBox.Show("Surname name must be letters only!");
                return false;
            }

            else
            {
                return true;
            }
        }

        public static Boolean ValEmail(string email)
        {
            Regex eMailChk = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (email == "")
            {
                MessageBox.Show("Email must not be empty!");
                return false;
            }
            if (eMailChk.IsMatch(email))
            {
                // Fixed: SQL injection vulnerability - using parameterized queries
                try
                {
                    string emailToLower = email.ToLower();
                    string inIn = "SELECT e_mail FROM Customer WHERE e_mail = @email";
                    using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                    using SqlCommand command = new(inIn, databaseConnection);
                    command.Parameters.AddWithValue("@email", emailToLower);

                    databaseConnection.Open();
                    using SqlDataReader dr = command.ExecuteReader();

                    if (!dr.Read())
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Email already exists in database!");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Email validation failed: {ex.Message}");
                    MessageBox.Show("Error validating email. Please try again.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Invalid E_Mail!");
                return false;
            }
        }


        public static Boolean ValUpdateEmail(string cust, string email)
        {
            Regex eMailChk = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (email == "")
            {
                MessageBox.Show("Email must not be empty!");
                return false;
            }
            if (eMailChk.IsMatch(email))
            {
                // Fixed: SQL injection vulnerability - using parameterized queries
                try
                {
                    string emailToLower = email.ToLower();
                    string inIn = "SELECT e_mail FROM Customer WHERE cust_id != @cust AND e_mail = @email";
                    using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                    using SqlCommand command = new(inIn, databaseConnection);
                    command.Parameters.AddWithValue("@cust", cust);
                    command.Parameters.AddWithValue("@email", emailToLower);

                    databaseConnection.Open();
                    using SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("EMail already exists in database!");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Email update validation failed: {ex.Message}");
                    MessageBox.Show("Error validating email. Please try again.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Invalid E_Mail!");
                return false;
            }
        }

        public static Boolean ValPhone(string phone)
        {
            Regex phoneChk = new("^[0-9]*$");
            if (phone == "")
            {
                MessageBox.Show("Phone number must not be empty!");
                return false;
            }
            if (phoneChk.IsMatch(phone))
            {
                // Fixed: SQL injection vulnerability - using parameterized queries
                try
                {
                    string inIn = "SELECT phone FROM Customer WHERE phone = @phone";
                    using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                    using SqlCommand command = new(inIn, databaseConnection);
                    command.Parameters.AddWithValue("@phone", phone);

                    databaseConnection.Open();
                    using SqlDataReader dr = command.ExecuteReader();

                    if (!dr.Read())
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Phone number already exists in database!");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Phone validation failed: {ex.Message}");
                    MessageBox.Show("Error validating phone. Please try again.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Phone number must be numbers only!");
                return false;
            }
        }

        public static Boolean ValUpdatePhone(string cust, string phone)
        {
            Regex phoneChk = new("^[0-9]*$");
            if (phone == "")
            {
                MessageBox.Show("Phone number must not be empty!");
                return false;
            }
            if (phoneChk.IsMatch(phone))
            {
                // Fixed: SQL injection vulnerability - using parameterized queries
                try
                {
                    string inIn = "SELECT phone FROM Customer WHERE cust_id != @cust AND phone = @phone";
                    using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                    using SqlCommand command = new(inIn, databaseConnection);
                    command.Parameters.AddWithValue("@cust", cust);
                    command.Parameters.AddWithValue("@phone", phone);

                    databaseConnection.Open();
                    using SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Phone already exists in database!");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Phone update validation failed: {ex.Message}");
                    MessageBox.Show("Error validating phone. Please try again.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Phone number must be numbers only!");
                return false;
            }
        }
    }
}
