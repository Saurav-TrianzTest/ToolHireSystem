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
            int nextPaymentId;

            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX(payment_id) FROM Payments";
            using SqlCommand command = new(strSQL, databaseConnection);

            using SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextPaymentId = 1;
            }
            else
            {
                nextPaymentId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            return nextPaymentId;
        }
        public void RegPayment()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "INSERT INTO Payments (payment_id, transaction_id, trans_date, amount) VALUES (@PaymentId, @TransactionId, @TransDate, @Amount)";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@PaymentId", paymentId);
            command.Parameters.AddWithValue("@TransactionId", transactionId);
            command.Parameters.AddWithValue("@TransDate", transDate);
            command.Parameters.AddWithValue("@Amount", amount);

            command.ExecuteNonQuery();
        }
        public static DataSet GetPaymentByLastName(DataSet DS, string lastname)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT * FROM Payments WHERE cust_id = (SELECT cust_id FROM customer WHERE last_name LIKE @LastName) AND status = 'A'";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@LastName", "%" + lastname + "%");

            using SqlDataAdapter da = new(command);

            da.Fill(DS, "item");

            return DS;
        }


    }
}
