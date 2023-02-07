using Newtonsoft.Json;
using SocialNetwork.API.Models;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Repositories.Implementations;
using SocialNetwork.Test.Fixtures;
using System.Net;

namespace SocialNetwork.Test.Controllers
{
    public class UserControllerTests : IClassFixture<SocialNetworkFixture>
    {

        private readonly UserRepository _userRepository;
        private readonly HttpClient _client;

        public UserControllerTests(SocialNetworkFixture fixture)
        {
            _client = fixture.CreateClient();
            _userRepository = new UserRepository(fixture.InMemoryDbContext);
        }


        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetUser_Should_Return_UserModel()
        {
            // Arrange
            User user = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser@test.com",
                password:"test1234");

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChanges();


            // Act
            HttpResponseMessage result = await _client.GetAsync($"api/users/{user.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            UserModel? model = JsonConvert.DeserializeObject<UserModel>(response);
            Assert.NotNull(model);
            Assert.Equal($"{user.FirstName} {user.LastName}", model!.FullName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetUser_Should_Return_NotFound_When_Invalid_Id()
        {
            // Arrange
            int nonExistentUserId = 999;

            // Act
            HttpResponseMessage result = await _client.GetAsync($"api/users/{nonExistentUserId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetFollowers_Should_Return_UserModel_List()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "Test",
                lastName: "User Follower",
                email: "testuserfollower@test.com",
                password: "test12345")
            {
                Following = new List<User>
                {
                    user1
                }
            };

            await _userRepository.AddAsync(user1);
            await _userRepository.AddAsync(user2);
            await _userRepository.SaveChanges();

            // Act
            HttpResponseMessage result = await _client.GetAsync($"api/users/followers/{user1.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            IEnumerable<UserModel>? model = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(response);
            Assert.NotNull(model);
            string followerName = $"{user2.FirstName} {user2.LastName}";
            Assert.Contains(model, f => f.FullName == followerName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetFollowers_Should_Return_BadRequest_When_Invalid_Id()
        {
            // Arrange
            int nonExistentUserId = 999;

            // Act
            HttpResponseMessage result = await _client.GetAsync($"api/users/followers/{nonExistentUserId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            Assert.Equal($"User with id {nonExistentUserId} not found", response);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetFollows_Should_Return_UserModel_List()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "Test",
                lastName: "User Follower",
                email: "testuserfollower@test.com",
                password: "test12345")
            {
                Followers = new List<User>
                {
                    user1
                }
            };

            await _userRepository.AddAsync(user1);
            await _userRepository.AddAsync(user2);
            await _userRepository.SaveChanges();

            // Act
            HttpResponseMessage result = await _client.GetAsync($"api/users/following/{user1.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            IEnumerable<UserModel>? model = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(response);
            Assert.NotNull(model);
            string followingName = $"{user2.FirstName} {user2.LastName}";
            Assert.Contains(model, f => f.FullName == followingName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetFollows_Should_Return_BadRequest_When_Invalid_Id()
        {
            // Arrange
            int nonExistentUserId = 999;

            // Act
            HttpResponseMessage result = await _client.GetAsync($"api/users/following/{nonExistentUserId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            Assert.Equal($"User with id {nonExistentUserId} not found", response);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetUsersByName_Should_Return_UserModel_List()
        {
            // Arrange
            string searchName = "SUT";

            User user1 = new User(
                firstName: "User",
                lastName: "SUT",
                email: "testuser@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "SUT",
                lastName: "User",
                email: "testuserfollower@test.com",
                password: "test12345");

            await _userRepository.AddAsync(user1);
            await _userRepository.AddAsync(user2);
            await _userRepository.SaveChanges();

            // Act
            HttpResponseMessage result = await _client.GetAsync($"api/users?name={searchName}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            IEnumerable<UserModel>? model = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(response);
            Assert.NotNull(model);

            List<User> users = new List<User> { user1, user2 };
            bool isValidModel = ValidateUserModelList(users, model!);
            Assert.True(isValidModel);
        }

        private bool ValidateUserModelList(IEnumerable<User> users, IEnumerable<UserModel> modelList)
        {
            bool isValidModelList = true;
            foreach (var userModel in modelList)
            {
                isValidModelList = users.Any(u => u.Email == userModel.Email);

                if (!isValidModelList)
                    break;
            }

            return isValidModelList;
        }

    }
}
