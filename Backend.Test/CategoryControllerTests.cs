using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Backend.Dtos;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Backend.Test
{
    public class CategoryControllerTests : IntegrationTest
    {
        private static readonly CategoryCreateDto NewCategory = new CategoryCreateDto()
        {
            CategoryName = "New Test Category"
        };

        private static readonly string Json = JsonConvert.SerializeObject(NewCategory);
        private readonly StringContent _content = new StringContent(Json, Encoding.UTF8, "application/json");
        private static readonly string Url = "api/category/";
        [Fact]
        public async Task GetAllCategories()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetCategoryById_CategoryExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task GetCategoryById_CategoryNotExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateCategory()
        {
            // Arrange

            // Act
            var response = await TestClient.PostAsync(Url, _content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateCategory_BadRequest()
        {
            // Arrange
            var json = JsonConvert.SerializeObject("Create Category Bad Request");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(Url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateCategory(int id)
        {
            // Arrange
            var categoryName = "Updated Test Category " + id;
            CategoryUpdateDto categoryUpdate = new CategoryUpdateDto() { CategoryName = categoryName };
            var json = JsonConvert.SerializeObject(categoryUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(6)]
        public async Task UpdateCategory_NotFound(int id)
        {
            // Arrange
            var categoryName = "Updated Test Category " + id;
            CategoryUpdateDto categoryUpdate = new CategoryUpdateDto() { CategoryName = categoryName };
            var json = JsonConvert.SerializeObject(categoryUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateCategory_BadRequest(int id)
        {
            // Arrange
            var json = JsonConvert.SerializeObject("UpdateCategory_BadRequest");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteCategory()
        {
            // Arrange
            await TestClient.PostAsync(Url, _content);

            // Act
            var response = await TestClient.DeleteAsync(Url + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCategory_NotFound()
        {
            // Arrange

            // Act
            var response = await TestClient.DeleteAsync(Url + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}