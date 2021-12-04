using Dapper;
using System;
using System.Data;

namespace OnlineShop.Server.DB.Mappers
{
    public class StringsMapper : SqlMapper.TypeHandler<string[]>
    {
        public override void SetValue(IDbDataParameter parameter, string[] value)
        {
            parameter.Value = String.Join(", ", value);
        }

        public override string[] Parse(object value)
        {
            var line = value as string;
            return line?.Split(", ") ?? new[] {""};
        }
    }
}