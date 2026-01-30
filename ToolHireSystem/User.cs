using Microsoft.Data.SqlClient;
using System;


namespace ToolHireSystem
{

    class User
    {
        private string userName;
        private string passWord;
        private readonly int auth;

        public User()
        {
            userName = "";
            passWord = "";
            auth = 0;
        }
        public User(string userName, string passWord)
        {
            SetUsername(userName);
            SetPassword(passWord);
            auth = 0;
        }
        public void SetUsername(string username)
        {
            userName = username;
        }
        public void SetPassword(string password)
        {
            passWord = password;
        }
        public string GetUsername()
        {
            return userName;
        }
        public string GetPassword()
        {
            return passWord;
        }
        public int GetAuth()
        {
            return auth;
        }
        public static Boolean GetUserByUserName(string username, string password)
        {
            // Fixed: SQL injection vulnerability - using parameterized queries
            // Fixed: Proper connection disposal with using statement
            // Fixed: Proper error handling for cloud deployment
            try
            {
                using SqlConnection databaseConnection = new(DBConnect.GetConnectionString());
                string strSQL = "SELECT * FROM USERS WHERE user_name = @username AND pass_word = @password AND level_auth = 1";
                using SqlCommand command = new(strSQL, databaseConnection);

                // Parameterized queries prevent SQL injection
                command.Parameters.AddWithValue("@username", username ?? string.Empty);
                command.Parameters.AddWithValue("@password", password ?? string.Empty);

                command.Connection.Open();

                using SqlDataReader dr = command.ExecuteReader();
                return dr.Read();
            }
            catch (Exception ex)
            {
                // Log to console for cloud monitoring (CloudWatch Logs)
                Console.WriteLine($"[ERROR] User authentication failed: {ex.Message}");
                throw; // Re-throw for proper error handling at higher levels
            }
        }

    }
}
