using System;

namespace ToolHireSystem
{
    class DBConnect
    {
        // Cloud-ready connection string using environment variables
        // For AWS deployment, use AWS Secrets Manager or RDS connection string
        public static string GetConnectionString()
        {
            // Priority: Environment variable > AWS Secrets Manager > Fallback
            string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback for local development only
                string server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
                string database = Environment.GetEnvironmentVariable("DB_NAME") ?? "ToolHireSystem";
                string userId = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
                string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

                // Use AWS RDS format for cloud deployment
                connectionString = $"Server={server};Database={database};User Id={userId};Password={password};Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;";
            }

            return connectionString;
        }

        // Backward compatibility - marked as obsolete
        [Obsolete("Use GetConnectionString() instead for cloud compatibility")]
        public static string oradb => GetConnectionString();
    }
}
