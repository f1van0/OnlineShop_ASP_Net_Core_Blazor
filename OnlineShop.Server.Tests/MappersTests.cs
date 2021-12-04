using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineShop.Server.DB;
using OnlineShop.Server.DB.Mappers;
using OnlineShop.Server.DB.Mappers.OnlineShop.Server.DB.Mappers;
using OnlineShop.Shared;
using System.Data;

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
            Vector size = new() {X = 3, Y = 3};

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
            Vector exprected = new Vector() {X = 32, Y = 102};

            // Act
            VectorMapper mapper = new();
            Vector size = mapper.Parse("32x102");

            // Assert
            size.Should().BeSameAs(exprected);
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

            // Act
            ArrayMapper<int> mapper = new();
            var result = mapper.Parse(example);

            // Assert
            result.Should().HaveCount(3);
            result.Should().Equal(1, 2, 3);
        }
    }
}