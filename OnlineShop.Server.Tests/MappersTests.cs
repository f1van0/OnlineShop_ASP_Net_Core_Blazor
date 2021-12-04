using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineShop.Server.DB;
using OnlineShop.Server.DB.Mappers;
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

            // // Assert
            size.Should().BeSameAs(exprected);
        }

        [Test]
        public void StringsMapper_Serialize()
        {
            // Palette = new ColourPalette() {Colors = new[] {"asd"}, ID = 1, Name = "ASD"},
            // Image = new int[][] {new[] {1, 2, 3}, new[] {1, 2, 3}, new[] {1, 2, 3}},
        }

        [Test]
        public void StringsMapper_Deserialize()
        {
        }
    }
}