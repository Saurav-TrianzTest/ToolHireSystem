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
            int nextTransId;

            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX(transaction_id) FROM invoices";
            using SqlCommand command = new(strSQL, databaseConnection);

            using SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextTransId = 1;
            }
            else
            {
                nextTransId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            return nextTransId;
        }
        public void RegInvoice()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "INSERT INTO Invoices (transaction_id, cust_id, transaction_date, amount, status) VALUES (@TransactionId, @CustId, @TransactionDate, @Balance, @Status)";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@TransactionId", transactionId);
            command.Parameters.AddWithValue("@CustId", custId);
            command.Parameters.AddWithValue("@TransactionDate", transactionDate);
            command.Parameters.AddWithValue("@Balance", balance);
            command.Parameters.AddWithValue("@Status", status);

            command.ExecuteNonQuery();
        }
        public static DataSet GetInvoiceByCustId(DataSet DS, string id)
        {
            int custId = Convert.ToInt32(id);

            using SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT transaction_id, cust_id, transaction_date, amount FROM Invoices WHERE cust_id = @CustId AND status = 'outstanding'";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@CustId", custId);

            using SqlDataAdapter da = new(command);

            da.Fill(DS, "invoice");

            return DS;
        }
        public void PayInvoice(int cust_id, decimal balance)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE Invoices SET status = @Status WHERE transaction_id = @TransactionId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@Status", status);
            command.Parameters.AddWithValue("@TransactionId", transactionId);

            command.ExecuteNonQuery();

            string strSQL1 = "UPDATE Customer SET balance = balance - @Balance WHERE cust_id = @CustId";

            using SqlCommand cmd1 = new(strSQL1, databaseConnection);
            cmd1.Parameters.AddWithValue("@Balance", balance);
            cmd1.Parameters.AddWithValue("@CustId", cust_id);

            cmd1.ExecuteNonQuery();
        }


    }
}
