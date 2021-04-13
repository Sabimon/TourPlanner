using log4net.Config;
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
        public NpgsqlConnection conn;
        private static DBConn _instance = null;
        private string connString = null;

        public void Connection()
        {
            try
            {
                //XmlConfigurator.Configure(new FileInfo(@"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\app.config"));
                var config = (ConnectionStringsConfig)ConfigurationManager.GetSection(nameof(ConnectionStringsConfig));
                if (config != null)
                {
                    connString = $"Host={config.Host};Username={config.Username};Password={config.Password};Database={config.Database};Port={config.Port}";
                }
                else//tests dont work with ConfigurationManager yet
                {
                    connString = $"Host=localhost;Username=postgres;Password=wordpass;Database=TourPlanner;Port=5432";
                }
                
                conn = new NpgsqlConnection(connString);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private DBConn()
        {
            Connection();
        }

        public static DBConn Instance()
        {
            if (_instance == null)
            {
                _instance = new DBConn();
            }
            return _instance;
        }
    }
}
