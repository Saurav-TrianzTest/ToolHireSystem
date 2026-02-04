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
            int nextRentalItemId;

            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX(item_rental_id) FROM RentalItems";
            using SqlCommand command = new(strSQL, databaseConnection);

            using SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextRentalItemId = 1;
            }
            else
            {
                nextRentalItemId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            return nextRentalItemId;
        }
        public void RegRentalItems()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "INSERT INTO RentalItems (item_rental_id, rental_id, supply_id, cust_id, date_from, date_to, item_cost, status) VALUES (@ItemRentalId, @RentalId, @SupplyId, @CustId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, @Price, @Status)";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@ItemRentalId", itemRentalId);
            command.Parameters.AddWithValue("@RentalId", rentalId);
            command.Parameters.AddWithValue("@SupplyId", supplyId);
            command.Parameters.AddWithValue("@CustId", custId);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Status", status);

            command.ExecuteNonQuery();
        }
        public static DataSet GetRentalItemsByLastName(DataSet DS, string lastname)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT item_rental_id, rental_id, supply_id, cust_id, date_from, date_to, item_cost FROM rentalItems WHERE cust_id = (SELECT cust_id FROM customer WHERE last_name LIKE @LastName) AND status = 'A'";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@LastName", "%" + lastname + "%");

            using SqlDataAdapter da = new(command);

            da.Fill(DS, "item");

            return DS;
        }
        public static DataSet GetRentalItemByCustId(DataSet DS, string id)
        {
            int custId = Convert.ToInt32(id);

            using SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT item_rental_id, rental_id, supply_id, date_from, date_to, item_cost FROM rentalItems WHERE cust_id = @CustId AND status = 'A'";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@CustId", custId);

            using SqlDataAdapter da = new(command);

            da.Fill(DS, "item");

            return DS;
        }
        public static void ReturnRentalItem(int rentalId)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE rentalItems SET Status = 'R' WHERE item_rental_Id = @RentalId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@RentalId", rentalId);

            command.ExecuteNonQuery();
        }

    }
}
