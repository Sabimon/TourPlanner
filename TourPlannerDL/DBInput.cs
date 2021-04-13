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
    }
}
