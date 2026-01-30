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
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();
                string strSQL = "SELECT MAX(rental_id) FROM Rentals";
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
                Console.WriteLine($"[ERROR] Failed to get next rental ID: {ex.Message}");
                throw;
            }
        }

        public static DataSet GetAllRentals(DataSet DS)
        {
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());

                string strSQL = "SELECT * FROM Rentals";

                using SqlCommand command = new(strSQL, databaseConnection);
                using SqlDataAdapter da = new(command);

                da.Fill(DS, "rtl");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get all rentals: {ex.Message}");
                throw;
            }
        }

        public void RegRental()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "INSERT INTO Rentals VALUES(@rentalId, @custId)";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@rentalId", rentalId);
                command.Parameters.AddWithValue("@custId", custId);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to register rental: {ex.Message}");
                throw;
            }
        }
    }
}
