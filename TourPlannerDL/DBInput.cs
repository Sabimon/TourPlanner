using Npgsql;

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

        public void InsertTourLogs(string distance, string totalTime, string highway, string access)
        {
            using (var cmd = new NpgsqlCommand($"INSERT INTO logs (\"routeID\", report, weather, total_time, date, distance, highway, access, rating, animals, cost) VALUES (@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k)", db.conn))
            {
                cmd.Parameters.AddWithValue("a", 1);
                cmd.Parameters.AddWithValue("b", "dauert lange");
                cmd.Parameters.AddWithValue("c", "rainy");
                cmd.Parameters.AddWithValue("d", totalTime);
                cmd.Parameters.AddWithValue("e", "24.01.");
                cmd.Parameters.AddWithValue("g", distance);
                cmd.Parameters.AddWithValue("f", highway);
                cmd.Parameters.AddWithValue("h", access);
                cmd.Parameters.AddWithValue("i", "1"); 
                cmd.Parameters.AddWithValue("j", "true");
                cmd.Parameters.AddWithValue("k", "10.5");
                cmd.ExecuteNonQuery();
            }
        }
    }
}
