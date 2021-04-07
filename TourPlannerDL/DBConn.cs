﻿using Npgsql;
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
    }
}
