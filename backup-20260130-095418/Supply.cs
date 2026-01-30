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

            SqlConnection databaseConnection = new(DBConnect.oradb);


            string strSQL = "SELECT * From Supply";


            SqlCommand command = new(strSQL, databaseConnection);


            SqlDataAdapter da = new(command);


            da.Fill(DS, "stk");


            databaseConnection.Close();


            return DS;

        }


        public static DataSet GetSuppType(DataSet DS, string type)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);


            string strSQL = "SELECT * From Supply where supply_type LIKE '%" + type + "%' AND status = 'A'";


            SqlCommand command = new(strSQL, databaseConnection);


            SqlDataAdapter da = new(command);


            da.Fill(DS, "stk");


            databaseConnection.Close();


            return DS;
        }

        public void RegSupply()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "INSERT INTO Supply Values(" + supply_id + ",'" + supply_type + "','" + description + "'," + price + ",'" + status + "')";


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();


        }

        public static int GetNextStockNo()
        {
            int nextStockNo;

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT MAX (supply_id) FROM Supply";
            SqlCommand command = new(strSQL, databaseConnection);


            SqlDataReader dr = command.ExecuteReader();


            dr.Read();




            if (dr.IsDBNull(0))
            {
                nextStockNo = 1;
            }
            else
            {
                nextStockNo = Convert.ToInt32(dr.GetValue(0)) + 1;
            }

            databaseConnection.Close();

            return nextStockNo;

        }
        public void UpdateSupply()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE Supply SET supply_type ='" + supply_type + "',description ='" + description + "',price =" + price + " WHERE supply_id =" + supply_id;


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }

        public void RemoveSupp()
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE Supply SET status ='U' WHERE supply_id =" + supply_id;


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }

        public static void UndoRemoveSupp(int id)
        {

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();


            string strSQL = "UPDATE Supply SET status ='A' WHERE supply_id =" + id;


            SqlCommand command = new(strSQL, databaseConnection);
            command.ExecuteNonQuery();


            databaseConnection.Close();
        }
    }
}
