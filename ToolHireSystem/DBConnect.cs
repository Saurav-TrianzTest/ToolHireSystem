using System;
using System.IO;

namespace ToolHireSystem
{
    class DBConnect
    {
        public static readonly string oradb = GetConnectionString();

        private static string GetConnectionString()
        {
            // Use a relative path to the database file in the application directory
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appPath, "Testing.mdf");
            
            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;Connect Timeout=30";
        }
    }
}
