using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineShop.Server.DB;
using OnlineShop.Server.DB.Mappers;
using OnlineShop.Server.DB.Mappers.OnlineShop.Server.DB.Mappers;
using OnlineShop.Shared;
using System.Data;
using System.Linq;
using System.Text;

namespace OnlineShop.Server.Tests
{
    public class MappersTests
    {
        [Test]
        public void VectorMapper_Serialize()
        {
            // Arrange
            Mock<IDbDataParameter> parameter = new Mock<IDbDataParameter>()
                .SetupProperty(x => x.Value);
            ImageSize size = new() {SizeX = 3, SizeY = 3};

            // Act
            VectorMapper mapper = new();
            IDbDataParameter mockedParameter = parameter.Object;
            mapper.SetValue(mockedParameter, size);

            // // Assert
            mockedParameter.Value.Should().Be("3x3");
        }

        [Test]
        public void VectorMapper_Deserialize()
        {
            // Arrange
            ImageSize expected = new ImageSize() {SizeX = 32, SizeY = 102};

            // Act
            VectorMapper mapper = new();
            ImageSize size = mapper.Parse("32x102");

            // Assert
            size.Should().Be(expected);
        }

        [Test]
        public void StringsMapper_Serialize()
        {
            // Arrange
            Mock<IDbDataParameter> parameter = new Mock<IDbDataParameter>()
                .SetupProperty(x => x.Value);
            string[] example = new[] {"Apple", "Banana"};

            // Act
            StringsMapper mapper = new();
            var dataParameter = parameter.Object;
            mapper.SetValue(dataParameter, example);

            // Assert
            dataParameter.Value.Should().Be("Apple, Banana");
        }

        [Test]
        public void StringsMapper_Deserialize()
        {
            // Arrange
            string example = "Apple, Banana";

            // Act
            StringsMapper mapper = new();
            var result = mapper.Parse(example);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Equal("Apple", "Banana");
        }

        [Test]
        public void ArraysMapper_Serialize()
        {
            // Arrange
            Mock<IDbDataParameter> parameter = new Mock<IDbDataParameter>()
                .SetupProperty(x => x.Value);
            int[] example = new[] {1, 2, 3};

            // Act
            ArrayMapper<int> mapper = new();
            var dataParameter = parameter.Object;
            mapper.SetValue(dataParameter, example);

            // Assert
            dataParameter.Value.Should().Be("[1,2,3]");
        }

        [Test]
        public void ArrayMapper_Deserialize()
        {
            // Arrange
            string example = "[1,2,3]";
            byte[] exampleBytes = Encoding.UTF8.GetBytes(example);
            
            // Act
            ArrayMapper<int> mapper = new();
            var result = mapper.Parse(exampleBytes);

            // Assert
            result.Should().HaveCount(3);
            result.Should().Equal(1, 2, 3);
        }

        [Test]
        public void ArrayMapper_Deserialize_Big()
        {
            // Arrange
            string example = "[[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,1,2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,2,2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,1,2,2,2,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,1,1,0,1,2,1,1,1,1,2,2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,1,1,1,1,1,1,2,2,2,2,2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,1,1,1,1,1,1,1,1,2,2,2,2,2,2,1,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,1,1,1,1,1,1,1,1,2,2,2,2,2,2,1,0,0,0,0,0,1,1,1,0,0,0,0],[0,0,0,0,0,1,1,1,2,1,1,1,1,2,2,2,1,2,1,1,0,0,0,1,1,1,1,1,0,0,0,0],[0,0,0,0,0,1,1,1,2,2,1,2,2,3,3,1,2,5,2,1,1,0,0,1,1,1,1,1,0,0,0,0],[0,0,0,0,0,1,1,2,2,2,1,2,2,3,2,2,1,3,2,1,1,1,1,1,0,1,1,0,0,0,0,0],[0,0,1,0,0,1,1,2,3,2,2,2,2,4,3,1,2,3,2,2,1,1,1,1,1,1,1,1,1,1,0,0],[1,0,1,1,0,1,1,2,2,2,2,2,2,3,4,2,3,4,4,3,2,2,1,1,1,1,1,1,1,1,1,1],[1,0,1,1,0,1,1,2,2,2,3,2,2,1,2,3,4,4,4,4,3,3,2,1,1,1,1,1,1,1,1,1],[2,1,1,1,1,0,1,2,2,2,2,2,2,2,1,3,5,4,4,4,4,4,2,2,1,1,1,1,1,1,1,0],[2,1,1,1,1,0,1,2,2,2,2,3,2,2,2,2,3,3,4,4,5,3,3,3,2,1,1,1,1,1,1,1],[2,2,2,1,1,1,1,1,2,2,3,3,2,2,2,2,2,5,4,3,3,4,2,1,3,2,1,1,1,1,1,1],[2,2,3,2,1,1,1,2,2,2,2,3,3,2,2,2,2,3,4,5,5,4,1,2,3,3,1,1,1,2,2,2],[1,2,3,3,1,0,1,1,1,1,1,3,2,2,3,3,3,3,5,5,2,2,2,3,4,2,2,2,2,2,2,2],[0,2,1,2,1,0,0,2,2,2,2,2,3,3,5,4,2,3,2,1,2,3,4,5,4,3,3,3,3,2,1,2],[0,1,1,1,1,0,0,1,2,3,2,2,3,3,4,5,3,2,2,3,4,5,5,5,5,4,4,4,3,2,2,3],[0,1,1,0,1,0,0,1,2,2,2,2,3,4,3,4,5,3,2,2,4,5,5,5,5,5,4,3,3,2,2,3],[0,0,0,0,0,0,1,1,2,2,2,2,3,3,3,4,5,5,4,4,4,5,5,4,4,3,2,2,3,3,3,3],[0,0,0,0,0,0,0,1,2,2,2,2,3,3,3,3,4,4,4,4,4,5,4,3,2,2,2,3,3,3,4,2],[0,0,0,0,0,0,0,0,1,1,2,2,2,2,3,3,3,3,3,3,3,3,3,3,2,2,2,3,3,3,3,2],[0,0,0,0,0,0,0,0,0,0,1,2,3,3,3,3,3,3,2,2,2,3,2,2,2,1,1,2,2,2,2,3],[1,0,0,0,0,0,0,0,0,0,1,2,2,3,2,3,3,3,2,2,2,2,1,1,1,1,1,1,2,1,2,2]]";
            byte[] exampleBytes = Encoding.UTF8.GetBytes(example);
            // Act
            ArrayMapper<int[]> mapper = new();
            var result = mapper.Parse(exampleBytes);

            // Assert
            result.Should().HaveCount(32);
            result.FirstOrDefault().Should().HaveCount(32);
        }
    }
}