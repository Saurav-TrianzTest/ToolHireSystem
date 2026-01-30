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

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX (payment_id) FROM Payments";
            SqlCommand command = new(strSQL, databaseConnection);


            SqlDataReader dr = command.ExecuteReader();


            dr.Read();




            if (dr.IsDBNull(0))
            {
                nextPaymentId = 1;
            }
            else
            {
                nextPaymentId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            databaseConnection.Close();

            return nextPaymentId;

        }
        public void RegPayment()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();
            string date = transDate.ToString("dd-MMM-yyyy");

            string strSQL = "INSERT INTO Payments Values(" + paymentId + "," + transactionId + ",'" + date + "'," + amount + ")";


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }
        public static DataSet GetPaymentByLastName(DataSet DS, string lastname)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT * from Payments where cust_id=(select cust_id from customer where last_name like '%" + lastname + "%' )and status = 'A'";


            SqlCommand command = new(strSQL, databaseConnection);




            SqlDataAdapter da = new(command);


            da.Fill(DS, "item");


            databaseConnection.Close();


            return DS;
        }


    }
}
