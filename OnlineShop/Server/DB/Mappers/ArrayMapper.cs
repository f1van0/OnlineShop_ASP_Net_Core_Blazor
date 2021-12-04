using Dapper;
using System.Data;
using System.Text.Json;

namespace OnlineShop.Server.DB.Mappers
{
    namespace OnlineShop.Server.DB.Mappers
    {
        public class ArrayMapper<T> : SqlMapper.TypeHandler<T[]>
        {
            public override void SetValue(IDbDataParameter parameter, T[] value) =>
                parameter.Value = JsonSerializer.Serialize(value);

            public override T[] Parse(object value) =>
                JsonSerializer.Deserialize<T[]>((string)value);
        }
    }
}