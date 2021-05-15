using log4net;
using Npgsql;
using System;
using System.Configuration; //Nuggi erforderlich


namespace TourPlannerDL
{
    public class DBConn
    {
        public NpgsqlConnection conn;
        private static DBConn _instance = null;
        private string connString = null;
        private static readonly ILog log = LogManager.GetLogger(typeof(DBConn));

        public void Connection()
        {
            try
            {
                //XmlConfigurator.Configure(new FileInfo(@"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\app.config"));
                //var config = (ConnectionStringsConfig)ConfigurationManager.GetSection(nameof(ConnectionStringsConfig));
                connString = ConfigurationManager.ConnectionStrings["ConnectionStringConfig"].ConnectionString;
                conn = new NpgsqlConnection(connString);
                conn.Open();
                log.Info("DB Connection success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                log.Info("DB Connection failed");
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
