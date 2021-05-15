using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TourPlannerModels;

namespace TourPlannerDL
{
    public class DBOutput
    {
        DBConn db;

        public DBOutput()
        {
            db = DBConn.Instance();
        }

        public IEnumerable<MediaItem> GetRoutes(MediaFolder folder)
        {
            List<MediaItem> resultList = new List<MediaItem>();
            using (var cmd = new NpgsqlCommand($"SELECT routename FROM routes;", db.conn))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resultList.Add(new MediaItem() { Name = reader.GetString(0) });
                }
                reader.Close();
            }
            if (resultList == null)
            {
                resultList.Add(new MediaItem() { Name = "No Tour found" });
            }
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
            return Result;
        }

        public ObservableCollection<Logs> GetLogs(int ID)
        {
            ObservableCollection<Logs> resultList = new ObservableCollection<Logs>();
            //resultList = null;
            using (var cmd = new NpgsqlCommand($"SELECT * FROM logs WHERE \"routeID\" = (@r);", db.conn))
            {
                cmd.Parameters.AddWithValue("r", ID);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resultList.Add(new Logs() {
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
                        Distance = reader.GetString(2),
                        Time = reader.GetString(3),
                        Highway = reader.GetString(4),
                        Access = reader.GetString(5)
                    });
                }
                reader.Close();
            }
            return resultList;
        }
    }
}
