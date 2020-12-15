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
        public async Task GetCategoryById_CategoryExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("api/category/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task GetCategoryById_CategoryNotExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync("api/category/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateCategory()
        {
            // Arrange
            CategoryCreateDto newCategory = new CategoryCreateDto()
            {
                CategoryName = "New Test Category"
            };
            var json = JsonConvert.SerializeObject(newCategory);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync("api/category/", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateCategory_BadRequest()
        {
            // Arrange
            var json = JsonConvert.SerializeObject("newCategory");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync("api/category/", content);

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
            var response = await TestClient.PutAsync("api/category/" + id, content);

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
            var response = await TestClient.PutAsync("api/category/" + id, content);

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
            var response = await TestClient.PutAsync("api/category/" + id, content);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PartialUpdateCategory()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task DeleteCategory()
        {
            // Arrange
            CategoryCreateDto newCategory = new CategoryCreateDto()
            {
                CategoryName = "New Test Category"
            };
            var json = JsonConvert.SerializeObject(newCategory);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await TestClient.PostAsync("api/category/", content);

            // Act
            var response = await TestClient.DeleteAsync("api/category/" + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCategory_NotFound()
        {
            // Arrange

            // Act
            var response = await TestClient.DeleteAsync("api/category/" + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}