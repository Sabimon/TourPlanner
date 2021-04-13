using System;
using System.Collections.Generic;
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
    }
}
