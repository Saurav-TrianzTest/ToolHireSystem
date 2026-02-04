using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ToolHireSystem
{
    class Rental
    {
        private int rentalId;
        private int custId;

        public Rental()
        {
            rentalId = 0;
            custId = 0;
        }

        public Rental(int rental, int customer)
        {
            SetRentalId(rental);
            SetCustId(customer);
        }

        public void SetRentalId(int rental)
        {
            rentalId = rental;
        }

        public void SetCustId(int cust)
        {
            custId = cust;
        }

        public static int GetNextRentalId()
        {
            int nextRentalId;
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();
            string strSQL = "SELECT MAX(rental_id) FROM Rentals";
            using SqlCommand command = new(strSQL, databaseConnection);

            using SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextRentalId = 1;
            }
            else
            {
                nextRentalId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            return nextRentalId;
        }

        public static DataSet GetAllRentals(DataSet DS)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);

            string strSQL = "SELECT * FROM Rentals";

            using SqlCommand command = new(strSQL, databaseConnection);
            using SqlDataAdapter da = new(command);

            da.Fill(DS, "rtl");

            return DS;
        }

        public void RegRental()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "INSERT INTO Rentals (rental_id, cust_id) VALUES (@RentalId, @CustId)";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@RentalId", rentalId);
            command.Parameters.AddWithValue("@CustId", custId);

            command.ExecuteNonQuery();
        }
    }
}
