using Npgsql;
using System.Collections.ObjectModel;
using TourPlannerModels;

namespace TourPlannerDL
{
    public class DBInput
    {
        DBConn db;

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
            }
        }

        public void DeleteRoute(string route)
        {
            using (var cmd = new NpgsqlCommand($"DELETE FROM routes WHERE routename = (@r)", db.conn))
            {
                cmd.Parameters.AddWithValue("r", route);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertTourLogs(ObservableCollection<Logs> AddLogs)
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO logs (report, weather, total_time, date, distance, highway, access, rating, animals, cost) VALUES (@b, @c, @d, @e, @f, @g, @h, @i, @j, @k)", db.conn))
            {
                cmd.Parameters.AddWithValue("b", AddLogs[0].Report);
                cmd.Parameters.AddWithValue("c", AddLogs[0].Weather);
                cmd.Parameters.AddWithValue("d", AddLogs[0].Time);
                cmd.Parameters.AddWithValue("e", AddLogs[0].Date);
                cmd.Parameters.AddWithValue("g", AddLogs[0].Distance);
                cmd.Parameters.AddWithValue("f", AddLogs[0].Highway);
                cmd.Parameters.AddWithValue("h", AddLogs[0].Access);
                cmd.Parameters.AddWithValue("i", AddLogs[0].Rating); 
                cmd.Parameters.AddWithValue("j", AddLogs[0].Animals);
                cmd.Parameters.AddWithValue("k", AddLogs[0].Cost);
                cmd.ExecuteNonQuery();
            }
        }
        public void InsertTourDescription(string distance, string totalTime, string highway, string access)
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO description (distance, total_time, highway, access) VALUES (@b, @c, @d, @e)", db.conn))
            {
                cmd.Parameters.AddWithValue("b", distance);
                cmd.Parameters.AddWithValue("c", totalTime);
                cmd.Parameters.AddWithValue("d", highway);
                cmd.Parameters.AddWithValue("e", access);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
