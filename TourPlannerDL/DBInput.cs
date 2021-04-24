using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            using (var cmd = new NpgsqlCommand($"INSERT INTO logs (\"routeID\", report, rating, animals, cost, weather, total_time, date, distance, highway, access) VALUES (@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k)", db.conn))
            {
                cmd.Parameters.AddWithValue("a", 1);
                cmd.Parameters.AddWithValue("b", "dauert lange");
                cmd.Parameters.AddWithValue("c", 1);
                cmd.Parameters.AddWithValue("d", 1);
                cmd.Parameters.AddWithValue("e", 10.5);
                cmd.Parameters.AddWithValue("g", "rainy");
                cmd.Parameters.AddWithValue("f", totalTime);
                cmd.Parameters.AddWithValue("h", "24.01.");
                cmd.Parameters.AddWithValue("i", distance); 
                cmd.Parameters.AddWithValue("j", highway);
                cmd.Parameters.AddWithValue("k", access);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
