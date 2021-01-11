using System;
using System.Collections.Generic;
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
    public class ExpireControllerTests : IntegrationTest
    {
        private static readonly ExpireCreateDto NewExpire = new ExpireCreateDto()
        {
            ExpirationDate = DateTime.Now.Date,
            IdItem = 1,
            IdStorage = 1,
            UserId = "1"
        };

        private static readonly string Json = JsonConvert.SerializeObject(NewExpire);
        private static readonly StringContent Content = new StringContent(Json, Encoding.UTF8, "application/json");
        private static readonly string Url = "api/expire/";

        [Fact]
        public async Task GetAllExpires()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetExpireById_ExpireExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task GetExpireById_ExpireNotExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateExpire()
        {
            // Arrange

            // Act
            var response = await TestClient.PostAsync(Url, Content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateExpire_BadRequest()
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
        public async Task UpdateExpire(int id)
        {
            // Arrange
            var oldExpireHttpResponseMessage = await TestClient.GetAsync(Url + id);
            var oldExpire = await oldExpireHttpResponseMessage.Content.ReadAsStringAsync();
            var old = JsonConvert.DeserializeObject<ExpireReadDto>(oldExpire);
            var newExpireDate = DateTime.Now.Date.AddDays(id);
            ExpireUpdateDto expireUpdateDto = new ExpireUpdateDto() { IdItem = old.IdItem, UserId = old.IdUser, ExpirationDate = newExpireDate };
            var json = JsonConvert.SerializeObject(expireUpdateDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(6)]
        public async Task UpdateExpire_NotFound(int id)
        {
            // Arrange
            var oldExpireHttpResponseMessage = await TestClient.GetAsync(Url + "1");
            var oldExpire = await oldExpireHttpResponseMessage.Content.ReadAsStringAsync();
            var old = JsonConvert.DeserializeObject<ExpireReadDto>(oldExpire);
            var newExpireDate = DateTime.Now.Date.AddDays(id);
            ExpireUpdateDto expireUpdateDto = new ExpireUpdateDto() { IdItem = old.IdItem, UserId = old.IdUser, ExpirationDate = newExpireDate };
            var json = JsonConvert.SerializeObject(expireUpdateDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateExpire_BadRequest(int id)
        {
            // Arrange
            var json = JsonConvert.SerializeObject("UpdateExpire_BadRequest");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteExpire()
        {
            // Arrange
            await TestClient.PostAsync(Url, Content);
            // Act
            var response = await TestClient.DeleteAsync(Url + 5);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteExpire_NotFound()
        {
            // Arrange

            // Act
            var response = await TestClient.DeleteAsync(Url + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetExpireByUserId_UserExists()
        {
            // Arrange
            var userIdResponseMessage = await TestClient.GetAsync("api/users/");
            var userIdContent = await userIdResponseMessage.Content.ReadAsStringAsync();
            var userIdJson = JsonConvert.DeserializeObject<List<UserReadDto>>(userIdContent);
            var id = userIdJson[0].UserId;
            // Act
            var response = await TestClient.GetAsync(Url + "byUserId/" + id);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task GetExpireByUserId_UserNotExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + "byUserId/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}