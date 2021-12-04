using Dapper;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Size = OnlineShop.Shared.Size;

namespace OnlineShop.Server.DB.Mappers
{
    public class SizeMapper : SqlMapper.TypeHandler<Size>
    {
        private static Regex sizeRegex = new(@"(\d+)x(\d+)", RegexOptions.Compiled | RegexOptions.Singleline);

        public override void SetValue(IDbDataParameter parameter, Size value) =>
            parameter.Value = $"{value.X}x{value.Y}";

        public override Size Parse(object value)
        {
            string line = (string)value;

            Group[] groups = sizeRegex.Match(line).Groups.Values.ToArray();
            if (groups.Length != 2)
                throw new SerializationException($"Could not serialize Size = {line}");

            int x = int.Parse(groups[0].Value);
            int y = int.Parse(groups[1].Value);
            return new Size() {X = x, Y = y};
        }
    }
}