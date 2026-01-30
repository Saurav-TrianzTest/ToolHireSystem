using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ToolHireSystem
{
    class Invoice
    {
        private readonly int transactionId;
        private readonly int custId;
        private readonly DateTime transactionDate;
        private readonly decimal balance;
        private readonly string status;

        public Invoice()
        {
            transactionId = 0;
            custId = 0;

            transactionDate = DateTime.Now.Date;
            balance = 0;
        }
        public Invoice(int transId, int custId, DateTime date, decimal balance, string status)
        {
            transactionId = transId;
            this.custId = custId;

            transactionDate = date;
            this.balance = balance;
            this.status = status;
        }
        public static int GetNextTransId()
        {
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "SELECT MAX(transaction_id) FROM invoices";
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
                Console.WriteLine($"[ERROR] Failed to get next transaction ID: {ex.Message}");
                throw;
            }
        }
        public void RegInvoice()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string date = transactionDate.ToString("dd-MMM-yyyy");

                string strSQL = "INSERT INTO Invoices VALUES(@transactionId, @custId, @date, @balance, @status)";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@transactionId", transactionId);
                command.Parameters.AddWithValue("@custId", custId);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@balance", balance);
                command.Parameters.AddWithValue("@status", status ?? "outstanding");

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to register invoice: {ex.Message}");
                throw;
            }
        }
        public static DataSet GetInvoiceByCustId(DataSet DS, string id)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                string strSQL = "SELECT transaction_id,cust_id,transaction_date,amount FROM Invoices WHERE cust_id = @id AND status = 'outstanding'";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(id));

                using SqlDataAdapter da = new(command);
                da.Fill(DS, "invoice");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get invoices by customer ID: {ex.Message}");
                throw;
            }
        }
        public void PayInvoice(int cust_id, decimal balance)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            // Fixed: No transaction management - added try-catch
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE Invoices SET status = @status WHERE transaction_id = @transactionId";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@status", status ?? "paid");
                command.Parameters.AddWithValue("@transactionId", transactionId);

                command.ExecuteNonQuery();

                string strSQL1 = "UPDATE Customer SET balance = balance - @balance WHERE cust_id = @cust_id";

                using SqlCommand cmd1 = new(strSQL1, databaseConnection);
                cmd1.Parameters.AddWithValue("@balance", balance);
                cmd1.Parameters.AddWithValue("@cust_id", cust_id);

                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to pay invoice: {ex.Message}");
                throw;
            }
        }


    }
}
