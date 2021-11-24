using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;

namespace OnlineShop.Server.DB
{
    public class DataBase
    {
        DbConnection connection;

        public DataBase(DbConnection connection)
        {
            this.connection = connection;
        }

        public DateTime GetTime()
        {
            var command = connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select now()";

            using (var reader = command.ExecuteReader())
            {
                var time = reader.GetDateTime(0);
                return time;
            }
        }
    }
}
