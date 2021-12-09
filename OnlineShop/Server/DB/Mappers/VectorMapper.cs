using Dapper;
using OnlineShop.Shared;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace OnlineShop.Server.DB.Mappers
{
    public class VectorMapper : SqlMapper.TypeHandler<ImageSize>
    {
        private static Regex sizeRegex = new(@"(\d+)x(\d+)", RegexOptions.Compiled | RegexOptions.Singleline);

        public override void SetValue(IDbDataParameter parameter, ImageSize value) =>
            parameter.Value = $"{value.SizeX}x{value.SizeY}";

        public override ImageSize Parse(object value)
        {
            string line = (string)value;

            Group[] groups = sizeRegex.Match(line).Groups.Values.ToArray();
            if (groups.Length != 3)
                throw new SerializationException($"Could not serialize Size = {line}");

            int x = int.Parse(groups[1].Value);
            int y = int.Parse(groups[2].Value);
            return new ImageSize() {SizeX = x, SizeY = y};
        }
    }
}