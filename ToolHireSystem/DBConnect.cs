using System;

namespace ToolHireSystem
{
    /// <summary>
    /// Cloud-ready database connection configuration.
    /// Connection string is loaded from environment variables for cloud compatibility.
    /// </summary>
    class DBConnect
    {
        // Get connection string from environment variable for cloud deployments
        // Falls back to default for local development (should be configured in launchSettings.json)
        public static string oradb
        {
            get
            {
                // Primary: AWS Secrets Manager or Parameter Store (via environment variable)
                string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

                if (string.IsNullOrEmpty(connectionString))
                {
                    // Fallback for local development - use LocalDB
                    connectionString = Environment.GetEnvironmentVariable("LOCALDB_CONNECTION_STRING")
                        ?? "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Testing.mdf;Integrated Security=True;Connect Timeout=30;";
                }

                return connectionString;
            }
        }

        // AWS RDS connection string format example:
        // Data Source=myapp.c9akciq32.us-east-1.rds.amazonaws.com,1433;Initial Catalog=ToolHireDB;User ID=dbadmin;Password=<from-secrets-manager>;
    }
}
