using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Models;
using FluentAssertions;
using Xunit;

namespace Backend.Test
{
    public class CategoryControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAllCategories()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("api/category/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetCategoryById(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("api/category/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}