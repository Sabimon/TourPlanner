using log4net;
using Npgsql;
using System.Collections.ObjectModel;
using TourPlannerModels;

namespace TourPlannerDL
{
    public class DBInput
    {
        DBConn db;
        private static readonly ILog log = LogManager.GetLogger(typeof(DBInput));

        public DBInput()
        {
            db = DBConn.Instance();
        }

        public void InsertNewRoute(string FromDest, string ToDest)
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO routes (routename) VALUES (@r)", db.conn))
            {
                cmd.Parameters.AddWithValue("r", $"{FromDest}-{ToDest}");
                cmd.ExecuteNonQuery();
                log.Info("Route inserted to DB");
            }
        }
        public void DeleteRoute(string route)
        {
            using (var cmd = new NpgsqlCommand($"DELETE FROM routes WHERE routename = (@r)", db.conn))
            {
                cmd.Parameters.AddWithValue("r", route);
                cmd.ExecuteNonQuery();
                log.Info("Route deleted from DB");
            }
        }
        public void InsertTourLogs(ObservableCollection<Logs> AddLogs, int ID)
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO logs (\"routeID\", report, weather, " +
                $"total_time, date, distance, highway, access, rating, animals, cost) " +
                $"VALUES (@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k)", db.conn))
            {
                cmd.Parameters.AddWithValue("a", ID);
                cmd.Parameters.AddWithValue("b", AddLogs[0].Report);
                cmd.Parameters.AddWithValue("c", AddLogs[0].Weather);
                cmd.Parameters.AddWithValue("d", AddLogs[0].Time);
                cmd.Parameters.AddWithValue("e", AddLogs[0].Date);
                cmd.Parameters.AddWithValue("f", AddLogs[0].Distance);
                cmd.Parameters.AddWithValue("g", AddLogs[0].Highway);
                cmd.Parameters.AddWithValue("h", AddLogs[0].Access);
                cmd.Parameters.AddWithValue("i", AddLogs[0].Rating); 
                cmd.Parameters.AddWithValue("j", AddLogs[0].Animals);
                cmd.Parameters.AddWithValue("k", AddLogs[0].Cost);
                cmd.ExecuteNonQuery();
            }
            log.Info("Log inserted to DB");
        }
        public void InsertTourDescription(string distance, string totalTime, string highway, string access, int ID)
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO description (\"routeID\", distance, " +
                $"total_time, highway, access, descriptions) VALUES (@a, @b, @c, @d, @e)", db.conn))
            {
                cmd.Parameters.AddWithValue("a", ID);
                cmd.Parameters.AddWithValue("b", distance);
                cmd.Parameters.AddWithValue("c", totalTime);
                cmd.Parameters.AddWithValue("d", highway);
                cmd.Parameters.AddWithValue("e", access);
                cmd.ExecuteNonQuery();
            }
            log.Info("Description inserted to DB");
        }
        public void ChangeLog(ObservableCollection<Logs> ChangeLogs, int ID)
        {
            using (var cmd = new NpgsqlCommand($"UPDATE logs SET " +
                $"report=(@a), weather=(@b), total_time=(@c), date=(@d), distance=(@e), highway=(@f), " +
                $"access=(@g), rating=(@h), animals=(@i), cost=(@j) WHERE \"logID\"=(@k)", db.conn))
            {
                cmd.Parameters.AddWithValue("a", ChangeLogs[0].Report);
                cmd.Parameters.AddWithValue("b", ChangeLogs[0].Weather);
                cmd.Parameters.AddWithValue("c", ChangeLogs[0].Time);
                cmd.Parameters.AddWithValue("d", ChangeLogs[0].Date);
                cmd.Parameters.AddWithValue("e", ChangeLogs[0].Distance);
                cmd.Parameters.AddWithValue("f", ChangeLogs[0].Highway);
                cmd.Parameters.AddWithValue("g", ChangeLogs[0].Access);
                cmd.Parameters.AddWithValue("h", ChangeLogs[0].Rating);
                cmd.Parameters.AddWithValue("i", ChangeLogs[0].Animals);
                cmd.Parameters.AddWithValue("j", ChangeLogs[0].Cost);
                cmd.Parameters.AddWithValue("k", ID);
                cmd.ExecuteNonQuery();
            }
            log.Info("Log changed in DB");
        }
        public void DeleteLog(int ID)
        {
            using (var cmd = new NpgsqlCommand($"DELETE FROM logs WHERE \"logID\"=(@a)", db.conn))
            {
                cmd.Parameters.AddWithValue("a", ID);
                cmd.ExecuteNonQuery();
            }
            log.Info("Log deleted from DB");
        }
    }
}
