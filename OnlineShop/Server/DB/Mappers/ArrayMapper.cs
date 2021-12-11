using Dapper;
using System;
using System.Data;
using System.IO;
using System.Text.Json;

namespace OnlineShop.Server.DB.Mappers
{
    namespace OnlineShop.Server.DB.Mappers
    {
        public class ArrayMapper<T> : SqlMapper.TypeHandler<T[]>
        {
            public override void SetValue(IDbDataParameter parameter, T[] value) =>
                parameter.Value = JsonSerializer.Serialize(value);

            public override T[] Parse(object value)
            {
                Span<byte> span = new Span<byte>((byte[])value);
                return JsonSerializer.Deserialize<T[]>(span);
            }
        }
    }
}