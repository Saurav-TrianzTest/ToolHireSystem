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
            decimal balance = 0;
            Microsoft.Data.SqlClient.SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT balance FROM Customer where cust_id =" + id;
            SqlCommand command = new(strSQL, databaseConnection);


            SqlDataReader dr = command.ExecuteReader();


            if (dr.Read())
            {
                balance = dr.GetDecimal(0);
            }
            return balance;
        }

        public static int GetNextCustId()
        {
            int nextCustId;

            Microsoft.Data.SqlClient.SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX (cust_id) FROM Customer";
            SqlCommand command = new(strSQL, databaseConnection);

            SqlDataReader dr = command.ExecuteReader();

            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextCustId = 1;
            }
            else
            {
                nextCustId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            databaseConnection.Close();

            return nextCustId;
        }

        public void RegCustomer()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "INSERT INTO Customer Values(" + custId + ",'" + firstName + "','" + lastName + "','" + eMail + "','" + phone + "'," + balance + ",'" + status + "')";


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }

        public static DataSet GetCustomerByLastName(DataSet DS, string lastname)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);

            string strSQL = "SELECT cust_id,first_name,last_name,e_mail,phone,balance From Customer where last_name LIKE '%" + lastname + "%' AND account_status = 'A'";

            SqlCommand command = new(strSQL, databaseConnection);

            SqlDataAdapter da = new(command);

            da.Fill(DS, "cst");

            databaseConnection.Close();

            return DS;
        }

        public void UpdateCustomer()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE Customer SET first_name ='" + firstName + "',last_name ='" + lastName + "',e_mail ='" + eMail + "',phone='" + phone + "' WHERE cust_id =" + custId;


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }
        public static void CloseCustomer(int id)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE Customer SET account_status ='C' where cust_id=" + id;


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();

            databaseConnection.Close();
        }

        public void UpdateCustomerBalance(decimal updatedBalance)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE Customer SET balance =balance+" + updatedBalance + " WHERE cust_id =" + custId;

            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();

            databaseConnection.Close();
        }
    }
}
