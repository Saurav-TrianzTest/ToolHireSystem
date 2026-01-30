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
            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();
            string strSQL = "SELECT MAX (rental_id) FROM Rentals";
            SqlCommand command = new(strSQL, databaseConnection);

            SqlDataReader dr = command.ExecuteReader();

            dr.Read();


            if (dr.IsDBNull(0))
            {
                nextRentalId = 1;
            }
            else
            {
                nextRentalId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            databaseConnection.Close();

            return nextRentalId;

        }

        public static DataSet GetAllRentals(DataSet DS)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);


            string strSQL = "SELECT * From Rentals";


            SqlCommand command = new(strSQL, databaseConnection);


            SqlDataAdapter da = new(command);


            da.Fill(DS, "rtl");


            databaseConnection.Close();


            return DS;

        }

        public void RegRental()
        {
            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "INSERT INTO Rentals Values(" + rentalId + "," + custId + ")";

            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();

            databaseConnection.Close();
        }
    }
}
