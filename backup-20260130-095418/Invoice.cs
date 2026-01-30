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

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX (transaction_id) FROM invoices";
            SqlCommand command = new(strSQL, databaseConnection);


            SqlDataReader dr = command.ExecuteReader();


            dr.Read();




            if (dr.IsDBNull(0))
            {
                nextTransId = 1;
            }
            else
            {
                nextTransId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }


            databaseConnection.Close();

            return nextTransId;
        }
        public void RegInvoice()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string date = transactionDate.ToString("dd-MMM-yyyy");
            // DateTime sqlDate = (DateTime) date;

            string strSQL = "INSERT INTO Invoices Values(" + transactionId + "," + custId + ",'" + date + "'," + balance + ",'" + status + "')";


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }
        public static DataSet GetInvoiceByCustId(DataSet DS, string id)
        {
            Convert.ToInt32(id);

            SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT transaction_id,cust_id,transaction_date,amount from Invoices where cust_id=" + id + "and status = 'outstanding'";


            SqlCommand command = new(strSQL, databaseConnection);




            SqlDataAdapter da = new(command);


            da.Fill(DS, "invoice");


            databaseConnection.Close();


            return DS;
        }
        public void PayInvoice(int cust_id, decimal balance)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE Invoices SET status ='" + status + "' WHERE transaction_id =" + transactionId;


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();





            //  decimal test=100;
            //MessageBox.Show(" "+balance);

            string strSQL1 = "UPDATE Customer SET balance =balance-" + balance + " WHERE cust_id =" + cust_id;


            SqlCommand cmd1 = new(strSQL1, databaseConnection);
            cmd1.ExecuteNonQuery();
            databaseConnection.Close();
        }


    }
}
