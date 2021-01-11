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
    public class UserControllerTests : IntegrationTest
    {

        private static readonly UserInfo NewUser = new UserInfo()
        {
            EmailAddress = "newTestUser@test.test",
            Password = "String!!!2334",
        };

        private static readonly string Json = JsonConvert.SerializeObject(NewUser);
        private static readonly StringContent Content = new StringContent(Json, Encoding.UTF8, "application/json");
        private static readonly string Url = "api/users/";

        [Fact]
        public async Task GetAllUsers()
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetUserById_UserExists()
        {
            // Arrange
            var getUserHttpResponseMessage = await TestClient.GetAsync(Url);
            var allUsers = await getUserHttpResponseMessage.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserReadDto>>(allUsers);
            var userId = users[0].UserId;
            // Act
            var response = await TestClient.GetAsync(Url + userId);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task GetUserById_UserNotExists(int id)
        {
            // Arrange

            // Act
            var response = await TestClient.GetAsync(Url + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetUser_ByEmail_UserExist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task GetUser_ByEmail_UserNotExist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task CreateUser()
        {
            // Arrange

            // Act
            var response = await TestClient.PostAsync(Url + "Create/", Content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateUser_BadRequest()
        {
            // Arrange
            var json = JsonConvert.SerializeObject("Create User Bad Request");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(Url + "Create/", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateUser()
        {
            // Arrange
            var getUserHttpResponseMessage = await TestClient.GetAsync(Url);
            var allUsers = await getUserHttpResponseMessage.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserReadDto>>(allUsers);
            var oldUser = users[0];
            UserCreateDto userUpdate = new UserCreateDto() { UserEmail = oldUser.UserEmail + "updated", UserName = oldUser.UserName, UserPassword = "String!!!2334", UserNormalizedName = oldUser.UserName.ToUpper(), UserNormalizedEmail = (oldUser.UserEmail + "updated").ToUpper(), UserLockoutEnabled = true };
            var json = JsonConvert.SerializeObject(userUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + oldUser.UserId, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(6)]
        public async Task UpdateUser_NotFound(int id)
        {
            // Arrange
            var userName = "Updated Test User Name" + id;
            var userEmail = "updated_test@email." + id;
            var userPassword = "Updated Test User Password" + id;
            UserUpdateDto userUpdate = new UserUpdateDto() { UserName = userName, UserEmail = userEmail, UserPassword = userPassword };
            var json = JsonConvert.SerializeObject(userUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + id, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateUser_BadRequest()
        {
            // Arrange
            var getUserHttpResponseMessage = await TestClient.GetAsync(Url);
            var allUsers = await getUserHttpResponseMessage.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserReadDto>>(allUsers);
            var oldUser = users[0];
            var json = "Bad request";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PutAsync(Url + oldUser.UserId, content);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteUser()
        {
            // Arrange
            var getUserHttpResponseMessage = await TestClient.GetAsync(Url);
            var allUsers = await getUserHttpResponseMessage.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserReadDto>>(allUsers);
            var userId = users[0].UserId;

            // Act
            var response = await TestClient.DeleteAsync(Url + userId);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteUser_NotFound()
        {
            // Arrange

            // Act
            var response = await TestClient.DeleteAsync(Url + 6);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}