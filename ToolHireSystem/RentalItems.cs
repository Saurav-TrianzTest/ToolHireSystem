using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ToolHireSystem
{
    class RentalItems
    {
        private int itemRentalId;
        private int rentalId;
        private int supplyId;
        private readonly int custId;
        private int dateFrom;
        private int dateTo;
        private readonly decimal price;
        private readonly string status;

        public RentalItems()
        {
            itemRentalId = 0;
            rentalId = 0;
            supplyId = 0;
            custId = 0;
            dateFrom = 0;
            dateTo = 0;
            price = 0;
            status = "A";

        }

        public RentalItems(int itemRentalId, int rentalId, int supplyId, int custid, int dateTo, decimal price, string status)
        {
            SetItemRentalId(itemRentalId);
            SetRentalId(rentalId);
            SetSupplId(supplyId);
            custId = custid;

            SetDateTo(dateTo);
            this.price = price;
            this.status = status;
        }
        public void SetDateFrom(int date)
        {
            dateFrom = date;
        }
        public void SetItemRentalId(int itemId)
        {
            itemRentalId = itemId;
        }
        public void SetRentalId(int rentId)
        {
            rentalId = rentId;
        }
        public void SetSupplId(int suppId)
        {
            supplyId = suppId;
        }
        public void SetDateTo(int numDays)
        {
            dateTo = numDays;
        }
        public int GetDateFrom()
        {
            return dateFrom;
        }
        public int GetDateTo()
        {
            return dateTo;
        }

        public static int GetNextRentalItemsId()
        {
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "SELECT MAX(item_rental_id) FROM RentalItems";
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
                Console.WriteLine($"[ERROR] Failed to get next rental item ID: {ex.Message}");
                throw;
            }
        }
        public void RegRentalItems()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            // Fixed: CURRENT_TIMESTAMP replaced with DateTime.UtcNow for cloud portability
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "INSERT INTO RentalItems VALUES(@itemRentalId, @rentalId, @supplyId, @custId, @dateFrom, @dateTo, @price, @status)";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@itemRentalId", itemRentalId);
                command.Parameters.AddWithValue("@rentalId", rentalId);
                command.Parameters.AddWithValue("@supplyId", supplyId);
                command.Parameters.AddWithValue("@custId", custId);
                command.Parameters.AddWithValue("@dateFrom", DateTime.UtcNow);
                command.Parameters.AddWithValue("@dateTo", DateTime.UtcNow);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@status", status ?? "A");

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to register rental item: {ex.Message}");
                throw;
            }
        }
        public static DataSet GetRentalItemsByLastName(DataSet DS, string lastname)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                string strSQL = "SELECT item_rental_id,rental_id,supply_id,cust_id,date_from,date_to,item_cost FROM rentalItems WHERE cust_id=(SELECT cust_id FROM customer WHERE last_name LIKE @lastname) AND status = 'A'";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@lastname", $"%{lastname ?? string.Empty}%");

                using SqlDataAdapter da = new(command);
                da.Fill(DS, "item");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get rental items by last name: {ex.Message}");
                throw;
            }
        }
        public static DataSet GetRentalItemByCustId(DataSet DS, string id)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                string strSQL = "SELECT item_rental_id,rental_id,supply_id,date_from,date_to,item_cost FROM rentalItems WHERE cust_id = @id AND status = 'A'";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(id));

                using SqlDataAdapter da = new(command);
                da.Fill(DS, "item");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get rental items by customer ID: {ex.Message}");
                throw;
            }
        }
        public static void ReturnRentalItem(int rentalId)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE rentalItems SET Status = 'R' WHERE item_rental_Id = @rentalId";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@rentalId", rentalId);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to return rental item: {ex.Message}");
                throw;
            }
        }

    }
}
