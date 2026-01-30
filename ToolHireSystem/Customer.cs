using Microsoft.Data.SqlClient;
using System;
using System.Data;
namespace ToolHireSystem
{
    class Customer
    {
        private readonly int custId;
        private readonly string firstName;
        private readonly string lastName;
        private readonly string eMail;
        private readonly string phone;
        private readonly decimal balance;
        private readonly string status;


        public Customer()
        {
            custId = 0;
            firstName = "";
            lastName = "";
            eMail = "";
            phone = "";
            balance = 0;
            status = "";
        }
        public Customer(int custId, string firstName, string lastName, string eMail, string phone, decimal balance, string status)
        {
            this.custId = custId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.eMail = eMail;
            this.phone = phone;
            this.balance = balance;
            this.status = "A";

        }
        public static decimal GetBalance(int id)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            // Fixed: Proper resource disposal with using statements
            try
            {
                using Microsoft.Data.SqlClient.SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "SELECT balance FROM Customer WHERE cust_id = @id";
                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader dr = command.ExecuteReader();

                if (dr.Read())
                {
                    return dr.GetDecimal(0);
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get balance for customer {id}: {ex.Message}");
                throw;
            }
        }

        public static int GetNextCustId()
        {
            // Fixed: Proper resource disposal with using statements
            try
            {
                using Microsoft.Data.SqlClient.SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "SELECT MAX(cust_id) FROM Customer";
                using SqlCommand command = new(strSQL, databaseConnection);

                using SqlDataReader dr = command.ExecuteReader();
                dr.Read();

                if (dr.IsDBNull(0))
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(dr.GetValue(0)) + 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get next customer ID: {ex.Message}");
                throw;
            }
        }

        public void RegCustomer()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "INSERT INTO Customer VALUES(@custId, @firstName, @lastName, @eMail, @phone, @balance, @status)";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@custId", custId);
                command.Parameters.AddWithValue("@firstName", firstName ?? string.Empty);
                command.Parameters.AddWithValue("@lastName", lastName ?? string.Empty);
                command.Parameters.AddWithValue("@eMail", eMail ?? string.Empty);
                command.Parameters.AddWithValue("@phone", phone ?? string.Empty);
                command.Parameters.AddWithValue("@balance", balance);
                command.Parameters.AddWithValue("@status", status ?? "A");

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to register customer: {ex.Message}");
                throw;
            }
        }

        public static DataSet GetCustomerByLastName(DataSet DS, string lastname)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());

                string strSQL = "SELECT cust_id,first_name,last_name,e_mail,phone,balance FROM Customer WHERE last_name LIKE @lastname AND account_status = 'A'";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@lastname", $"%{lastname ?? string.Empty}%");

                using SqlDataAdapter da = new(command);
                da.Fill(DS, "cst");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get customers by last name: {ex.Message}");
                throw;
            }
        }

        public void UpdateCustomer()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE Customer SET first_name = @firstName, last_name = @lastName, e_mail = @eMail, phone = @phone WHERE cust_id = @custId";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@firstName", firstName ?? string.Empty);
                command.Parameters.AddWithValue("@lastName", lastName ?? string.Empty);
                command.Parameters.AddWithValue("@eMail", eMail ?? string.Empty);
                command.Parameters.AddWithValue("@phone", phone ?? string.Empty);
                command.Parameters.AddWithValue("@custId", custId);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to update customer: {ex.Message}");
                throw;
            }
        }
        public static void CloseCustomer(int id)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE Customer SET account_status = 'C' WHERE cust_id = @id";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to close customer account: {ex.Message}");
                throw;
            }
        }

        public void UpdateCustomerBalance(decimal updatedBalance)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE Customer SET balance = balance + @updatedBalance WHERE cust_id = @custId";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@updatedBalance", updatedBalance);
                command.Parameters.AddWithValue("@custId", custId);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to update customer balance: {ex.Message}");
                throw;
            }
        }
    }
}
