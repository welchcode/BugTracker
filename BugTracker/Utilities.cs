using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BugTracker
{
    public static class Utilities
    {
        public static SqlCommand Command;
        public static SqlConnection Connection;
        public static SqlDataReader Reader;

        public static void SetConnectionString()
        {
            string ConnectionString = "Data Source=DESKTOP-MRQJK1G;" +
                 "Initial Catalog=BugTrackerDatabase;" +
                 "Integrated Security=true;";

            Connection = new SqlConnection(ConnectionString);
                 
        }

    }
}
