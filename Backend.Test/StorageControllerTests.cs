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
    public class StorageControllerTests : IntegrationTest
    {
        private static readonly StorageCreateDto NewStorage = new StorageCreateDto()
        {
            StorageName = "New Test Storage",
            UserId = "1"
        };

        private static readonly string Json = JsonConvert.SerializeObject(NewStorage);
        private static readonly StringContent Content = new StringContent(Json, Encoding.UTF8, "application/json");
        private static readonly string Url = "api/storage/";

        [Fact]
        public async Task GetAllStorages()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetStorageById_StorageExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task GetStorageById_StorageNotExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateStorage()
        {
            // Arrange

            // Act
            var response = await TestClient.PostAsync(Url, Content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateStorage_BadRequest()
        {
            // Arrange
            var json = JsonConvert.SerializeObject("Create Storage Bad Request");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(Url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateStorage(int id)
        {
            // Arrange
            var oldStorageHttpResponseMessage = await TestClient.GetAsync(Url + id);
            var oldStorage = await oldStorageHttpResponseMessage.Content.ReadAsStringAsync();
            var old = JsonConvert.DeserializeObject<ExpireReadDto>(oldStorage);
            var storageName = "Updated Test Storage " + id;
            StorageUpdateDto storageUpdate = new StorageUpdateDto() { UserId = old.IdUser, StorageName = storageName };
            var json = JsonConvert.SerializeObject(storageUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(6)]
        public async Task UpdateStorage_NotFound(int id)
        {
            // Arrange
            var oldStorageHttpResponseMessage = await TestClient.GetAsync(Url + "1");
            var oldStorage = await oldStorageHttpResponseMessage.Content.ReadAsStringAsync();
            var old = JsonConvert.DeserializeObject<ExpireReadDto>(oldStorage);
            var storageName = "Updated Test Storage " + id;
            StorageUpdateDto storageUpdate = new StorageUpdateDto() { UserId = old.IdUser, StorageName = storageName };
            var json = JsonConvert.SerializeObject(storageUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateStorage_BadRequest(int id)
        {
            // Arrange
            var json = JsonConvert.SerializeObject("Update Storage BadRequest");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteStorage()
        {
            // Arrange
            await TestClient.PostAsync(Url, Content);

            // Act
            var response = await TestClient.DeleteAsync(Url + 5);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteStorage_NotFound()
        {
            // Arrange

            // Act
            var response = await TestClient.DeleteAsync(Url + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}