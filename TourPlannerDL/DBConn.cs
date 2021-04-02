using Npgsql;
using System;
using System.Configuration; //Nuggi erforderlich
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;


namespace TourPlannerDL
{
    public class DBConn
    {
        private NpgsqlConnection conn;

        public void Connection()
        {
            try
            {
                var config = (ConnectionStringsConfig)ConfigurationManager.GetSection(nameof(ConnectionStringsConfig));
                string connString = $"Host={config.Host};Username={config.Username};Password={config.Password};Database={config.Database};Port={config.Port}";
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
