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
            using SqlConnection databaseConnection = new(DBConnect.oradb);
            string strSQL = "SELECT * FROM USERS WHERE user_name = @Username AND pass_word = @Password AND level_auth = 1";
            using SqlCommand command = new(strSQL, databaseConnection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            command.Connection.Open();

            using SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
