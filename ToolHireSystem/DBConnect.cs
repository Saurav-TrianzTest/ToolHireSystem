using System;

namespace ToolHireSystem
{
    class DBConnect
    {
        public static readonly string oradb = GetConnectionString();

        private static string GetConnectionString()
        {
            // Try to get full connection string from environment variable first
            string connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (!string.IsNullOrEmpty(connString))
            {
                return connString;
            }

            // Fall back to building connection string from individual environment variables
            string server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
            string database = Environment.GetEnvironmentVariable("DB_NAME") ?? "ToolHireSystem";
            string user = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "DefaultPassword123!";

            return $"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;";
        }
    }
}
