using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ToolHireSystem
{
    class Payment
    {
        private readonly int paymentId;
        private readonly int transactionId;
        private readonly DateTime transDate;
        private readonly decimal amount;

        public Payment()
        {
            paymentId = 0;
            transactionId = 0;
            transDate = DateTime.Now.Date;
            amount = 0;
        }
        public Payment(int paymentId, int transactionId, DateTime transDate, decimal amount)
        {
            this.paymentId = paymentId;
            this.transactionId = transactionId;
            this.transDate = transDate;
            this.amount = amount;
        }
        public static int GetNextPaymentId()
        {
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "SELECT MAX(payment_id) FROM Payments";
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
                Console.WriteLine($"[ERROR] Failed to get next payment ID: {ex.Message}");
                throw;
            }
        }
        public void RegPayment()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();
                string date = transDate.ToString("dd-MMM-yyyy");

                string strSQL = "INSERT INTO Payments VALUES(@paymentId, @transactionId, @date, @amount)";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@paymentId", paymentId);
                command.Parameters.AddWithValue("@transactionId", transactionId);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@amount", amount);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to register payment: {ex.Message}");
                throw;
            }
        }
        public static DataSet GetPaymentByLastName(DataSet DS, string lastname)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                string strSQL = "SELECT * FROM Payments WHERE cust_id=(SELECT cust_id FROM customer WHERE last_name LIKE @lastname) AND status = 'A'";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@lastname", $"%{lastname ?? string.Empty}%");

                using SqlDataAdapter da = new(command);
                da.Fill(DS, "item");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get payments by last name: {ex.Message}");
                throw;
            }
        }


    }
}
