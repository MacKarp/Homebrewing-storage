using System;
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
    public class ItemControllerTests : IntegrationTest
    {
        private static readonly ItemCreateDto NewItem = new ItemCreateDto()
        {
            ItemName = "Test item",
            CategoryId = 1
        };

        private static readonly string Json = JsonConvert.SerializeObject(NewItem);
        private static readonly StringContent Content = new StringContent(Json, Encoding.UTF8, "application/json");
        private static readonly string Url = "api/item/";

        [Fact]
        public async Task GetAllItems()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetItemById_ItemExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task GetItemById_ItemNotExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateItem()
        {
            // Arrange

            // Act
            var response = await TestClient.PostAsync(Url, Content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateItem_BadRequest()
        {
            // Arrange
            var json = JsonConvert.SerializeObject("Create Item Bad Request");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(Url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateItem(int id)
        {
            // Arrange
            var itemName = "Updated Test Item " + id;
            ItemUpdateDto itemUpdate = new ItemUpdateDto() { ItemName = itemName };
            var json = JsonConvert.SerializeObject(itemUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(6)]
        public async Task UpdateItem_NotFound(int id)
        {
            // Arrange
            var itemName = "Updated Test Item " + id;
            ItemUpdateDto itemUpdate = new ItemUpdateDto() { ItemName = itemName };
            var json = JsonConvert.SerializeObject(itemUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateItem_BadRequest(int id)
        {
            // Arrange
            var json = JsonConvert.SerializeObject("Update Item BadRequest");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteItem()
        {
            // Arrange
            await TestClient.PostAsync(Url, Content);

            // Act
            var response = await TestClient.DeleteAsync(Url + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteItem_NotFound()
        {
            // Arrange

            // Act
            var response = await TestClient.DeleteAsync(Url + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}