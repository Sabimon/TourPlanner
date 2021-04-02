using Npgsql;
using System;
using System.Configuration; //Nuggi erforderlich
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;


namespace TourPlannerDL
{
    public class connectionStringsConfig
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }
    }

    public class CustomConfigLoader : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
            {
                throw new ArgumentNullException($"XMLNode passed in is null.");
            }

            var type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).FirstOrDefault(t => t.Name == section.Name);

            if (type == null)
            {
                throw new ArgumentException($"Type with name {section.Name} couldn't be found.");
            }

            XmlSerializer ser = new XmlSerializer(type, new XmlRootAttribute(section.Name));

            using (XmlReader reader = new XmlNodeReader(section))
            {
                return ser.Deserialize(reader);
            }
        }
    }

    public class DBConn
    {
        private NpgsqlConnection conn;
        //public string config = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\App.config";
        //public string connString = "Host=localhost;Username=postgres;Password=wordpass;Database=TourPlanner;Port=5432";

        public void Connection()
        {
            try
            {
                var config = (connectionStringsConfig)ConfigurationManager.GetSection(nameof(connectionStringsConfig));
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
