using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Npgsql;
using TourPlannerModels;

namespace TourPlannerDL
{
    public class DBOutput
    {
        DBConn db;
        private static readonly ILog log = LogManager.GetLogger(typeof(DBOutput));
        public static string ImagePath = @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\";

        public DBOutput()
        {
            db = DBConn.Instance();
        }

        public IEnumerable<Tour> GetRoutes(MediaFolder folder)
        {
            List<Tour> resultList = new List<Tour>();
            using (var cmd = new NpgsqlCommand($"SELECT routename, \"routeID\" FROM routes;", db.conn))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resultList.Add(new Tour() { Name = reader.GetString(0),
                    ImagePath= $@"{ImagePath}{reader.GetString(0)}.jpg",
                    TourID=reader.GetInt32(1)
                    });
                }
                reader.Close();
            }
            if (resultList == null)
            {
                resultList.Add(new Tour() { Name = "No Tour found" });
            }
            log.Info("RouteList filled from DB");
            return resultList;
        }

        public int GetRouteID(string Name)
        {
            int Result = 0;
            using (var cmd = new NpgsqlCommand($"SELECT \"routeID\" FROM routes WHERE routename =  (@r);", db.conn))
            {
                cmd.Parameters.AddWithValue("r", Name);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Result= reader.GetInt32(0);
                }
                reader.Close();
            }
            log.Info("Get RouteID from DB");
            return Result;
        }

        public ObservableCollection<Logs> GetLogs(int ID)
        {
            ObservableCollection<Logs> resultList = new ObservableCollection<Logs>();
            using (var cmd = new NpgsqlCommand($"SELECT * FROM logs WHERE \"routeID\" = (@r);", db.conn))
            {
                cmd.Parameters.AddWithValue("r", ID);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resultList.Add(new Logs() {
                        TourID=ID,
                        LogID= reader.GetInt32(0),
                        Report= reader.GetString(2), 
                        Weather = reader.GetString(3),
                        Time = reader.GetString(4),
                        Date = reader.GetString(5),
                        Highway = reader.GetString(6),
                        Distance = reader.GetString(7),
                        Access = reader.GetString(8),
                        Rating = reader.GetString(9),
                        Animals = reader.GetString(10),
                        Cost = reader.GetString(11)
                    });
                }
                reader.Close();
            }
            log.Info("LogList filled from DB");
            return resultList;
        }

        public ObservableCollection<Description> GetDescription(int ID)
        {
            ObservableCollection<Description> resultList = new ObservableCollection<Description>();
            using (var cmd = new NpgsqlCommand($"SELECT * FROM description WHERE \"routeID\" = (@r);", db.conn))
            {
                cmd.Parameters.AddWithValue("r", ID);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resultList.Add(new Description()
                    {
                        TourID=ID,
                        Distance = reader.GetString(2),
                        Time = reader.GetString(3),
                        Highway = reader.GetString(4),
                        Access = reader.GetString(5)
                    });
                }
                reader.Close();
            }
            log.Info("Get Description from DB");
            return resultList;
        }
        public int CountRouteIDInDescription(int ID)
        {
            int Result = 0;
            using (var cmd = new NpgsqlCommand($"SELECT COUNT(\"routeID\") FROM description WHERE \"routeID\" = (@r);", db.conn))
            {
                cmd.Parameters.AddWithValue("r", ID);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Result = reader.GetInt32(0);
                }
                reader.Close();
            }
            log.Info("Count Route IDs from DB in Table Description");
            return Result;
        }
    }
}
