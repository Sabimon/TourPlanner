using Npgsql;
using System;
using System.IO;

namespace TourPlannerDL
{
    public class DBConn
    {
        private NpgsqlConnection conn;
        public string path = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\db.config";
        public string connString = "Host=localhost;Username=postgres;Password=wordpass;Database=TourPlanner;Port=5432";
        public void Connection()
        {
            try
            {
                conn = new NpgsqlConnection(connString);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public DBConn()
        {
            Connection();
        }

        public void InsertNewRoute(string fileName)
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO routes (routename) VALUES (@r)", conn))
            {
                cmd.Parameters.AddWithValue("r", fileName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
