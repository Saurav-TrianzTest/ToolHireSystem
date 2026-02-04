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
            using Microsoft.Data.SqlClient.SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT balance FROM Customer WHERE cust_id = @CustId";
            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@CustId", id);

            using SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                balance = dr.GetDecimal(0);
            }
            return balance;
        }

        public static int GetNextCustId()
        {
            int nextCustId;

            using Microsoft.Data.SqlClient.SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX(cust_id) FROM Customer";
            using SqlCommand command = new(strSQL, databaseConnection);

            using SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextCustId = 1;
            }
            else
            {
                nextCustId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            return nextCustId;
        }

        public void RegCustomer()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "INSERT INTO Customer (cust_id, first_name, last_name, e_mail, phone, balance, account_status) VALUES (@CustId, @FirstName, @LastName, @Email, @Phone, @Balance, @Status)";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@CustId", custId);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", eMail);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Balance", balance);
            command.Parameters.AddWithValue("@Status", status);

            command.ExecuteNonQuery();
        }

        public static DataSet GetCustomerByLastName(DataSet DS, string lastname)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);

            string strSQL = "SELECT cust_id, first_name, last_name, e_mail, phone, balance FROM Customer WHERE last_name LIKE @LastName AND account_status = 'A'";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@LastName", "%" + lastname + "%");

            using SqlDataAdapter da = new(command);

            da.Fill(DS, "cst");

            return DS;
        }

        public void UpdateCustomer()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE Customer SET first_name = @FirstName, last_name = @LastName, e_mail = @Email, phone = @Phone WHERE cust_id = @CustId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", eMail);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@CustId", custId);

            command.ExecuteNonQuery();
        }
        public static void CloseCustomer(int id)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE Customer SET account_status = 'C' WHERE cust_id = @CustId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@CustId", id);

            command.ExecuteNonQuery();
        }

        public void UpdateCustomerBalance(decimal updatedBalance)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE Customer SET balance = balance + @UpdatedBalance WHERE cust_id = @CustId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@UpdatedBalance", updatedBalance);
            command.Parameters.AddWithValue("@CustId", custId);

            command.ExecuteNonQuery();
        }
    }
}
