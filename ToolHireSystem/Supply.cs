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
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());

                string strSQL = "SELECT * FROM Supply";

                using SqlCommand command = new(strSQL, databaseConnection);
                using SqlDataAdapter da = new(command);

                da.Fill(DS, "stk");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get all supplies: {ex.Message}");
                throw;
            }
        }


        public static DataSet GetSuppType(DataSet DS, string type)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());

                string strSQL = "SELECT * FROM Supply WHERE supply_type LIKE @type AND status = 'A'";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@type", $"%{type ?? string.Empty}%");

                using SqlDataAdapter da = new(command);
                da.Fill(DS, "stk");

                return DS;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get supplies by type: {ex.Message}");
                throw;
            }
        }

        public void RegSupply()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "INSERT INTO Supply VALUES(@supply_id, @supply_type, @description, @price, @status)";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@supply_id", supply_id);
                command.Parameters.AddWithValue("@supply_type", supply_type ?? string.Empty);
                command.Parameters.AddWithValue("@description", description ?? string.Empty);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@status", status ?? "A");

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to register supply: {ex.Message}");
                throw;
            }
        }

        public static int GetNextStockNo()
        {
            // Fixed: Proper resource disposal with using statements
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "SELECT MAX(supply_id) FROM Supply";
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
                Console.WriteLine($"[ERROR] Failed to get next stock number: {ex.Message}");
                throw;
            }
        }
        public void UpdateSupply()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE Supply SET supply_type = @supply_type, description = @description, price = @price WHERE supply_id = @supply_id";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@supply_type", supply_type ?? string.Empty);
                command.Parameters.AddWithValue("@description", description ?? string.Empty);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@supply_id", supply_id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to update supply: {ex.Message}");
                throw;
            }
        }

        public void RemoveSupp()
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE Supply SET status = 'U' WHERE supply_id = @supply_id";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@supply_id", supply_id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to remove supply: {ex.Message}");
                throw;
            }
        }

        public static void UndoRemoveSupp(int id)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                databaseConnection.Open();

                string strSQL = "UPDATE Supply SET status = 'A' WHERE supply_id = @id";

                using SqlCommand command = new(strSQL, databaseConnection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to undo remove supply: {ex.Message}");
                throw;
            }
        }
    }
}
