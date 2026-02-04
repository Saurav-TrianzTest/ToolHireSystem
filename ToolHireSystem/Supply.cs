using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ToolHireSystem
{
    class Supply
    {

        private int supply_id;
        private readonly string supply_type;
        private readonly string description;
        private readonly decimal price;
        private readonly string status;

        public Supply()
        {
            supply_id = 0;
            supply_type = "";
            description = "";
            price = 0;
            status = "";

        }

        public Supply(int supply_id, string supply_type, string description, decimal price, string status)
        {
            this.supply_id = supply_id;
            this.supply_type = supply_type;
            this.description = description;
            this.price = price;
            this.status = status;
        }

        public void SetSupplyId(int supply_id)
        {
            this.supply_id = supply_id;
        }

        public static DataSet GetAllSupply(DataSet DS)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);

            string strSQL = "SELECT * FROM Supply";

            using SqlCommand command = new(strSQL, databaseConnection);
            using SqlDataAdapter da = new(command);

            da.Fill(DS, "stk");

            return DS;
        }


        public static DataSet GetSuppType(DataSet DS, string type)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);

            string strSQL = "SELECT * FROM Supply WHERE supply_type LIKE @Type AND status = 'A'";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@Type", "%" + type + "%");

            using SqlDataAdapter da = new(command);

            da.Fill(DS, "stk");

            return DS;
        }

        public void RegSupply()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "INSERT INTO Supply (supply_id, supply_type, description, price, status) VALUES (@SupplyId, @SupplyType, @Description, @Price, @Status)";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@SupplyId", supply_id);
            command.Parameters.AddWithValue("@SupplyType", supply_type);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Status", status);

            command.ExecuteNonQuery();
        }

        public static int GetNextStockNo()
        {
            int nextStockNo;

            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX(supply_id) FROM Supply";
            using SqlCommand command = new(strSQL, databaseConnection);

            using SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            if (dr.IsDBNull(0))
            {
                nextStockNo = 1;
            }
            else
            {
                nextStockNo = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            return nextStockNo;
        }
        public void UpdateSupply()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE Supply SET supply_type = @SupplyType, description = @Description, price = @Price WHERE supply_id = @SupplyId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@SupplyType", supply_type);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@SupplyId", supply_id);

            command.ExecuteNonQuery();
        }

        public void RemoveSupp()
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE Supply SET status = 'U' WHERE supply_id = @SupplyId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@SupplyId", supply_id);

            command.ExecuteNonQuery();
        }

        public static void UndoRemoveSupp(int id)
        {
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "UPDATE Supply SET status = 'A' WHERE supply_id = @SupplyId";

            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@SupplyId", id);

            command.ExecuteNonQuery();
        }
    }
}
