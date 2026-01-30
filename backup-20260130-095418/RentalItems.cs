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

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX (item_rental_id) FROM RentalItems";
            SqlCommand command = new(strSQL, databaseConnection);

            SqlDataReader dr = command.ExecuteReader();

            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextRentalItemId = 1;
            }
            else
            {
                nextRentalItemId = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            databaseConnection.Close();

            return nextRentalItemId;

        }
        public void RegRentalItems()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "INSERT INTO RentalItems Values(" + itemRentalId + "," + rentalId + "," + supplyId + "," + custId + ",CURRENT_TIMESTAMP,CURRENT_TIMESTAMP," + price + ",'" + status + "')";


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }
        public static DataSet GetRentalItemsByLastName(DataSet DS, string lastname)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT item_rental_id,rental_id,supply_id,cust_id,date_from,date_to,item_cost from rentalItems where cust_id=(select cust_id from customer where last_name like '%" + lastname + "%' )and status = 'A'";

            SqlCommand command = new(strSQL, databaseConnection);

            SqlDataAdapter da = new(command);


            da.Fill(DS, "item");


            databaseConnection.Close();


            return DS;
        }
        public static DataSet GetRentalItemByCustId(DataSet DS, string id)
        {
            Convert.ToInt32(id);

            SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT item_rental_id,rental_id,supply_id,date_from,date_to,item_cost from rentalItems where cust_id= " + id + "and status = 'A'";

            SqlCommand command = new(strSQL, databaseConnection);

            SqlDataAdapter da = new(command);

            da.Fill(DS, "item");

            databaseConnection.Close();

            return DS;
        }
        public static void ReturnRentalItem(int rentalId)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE rentalItems SET Status ='R' WHERE item_rental_Id =" + rentalId;


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }

    }
}
