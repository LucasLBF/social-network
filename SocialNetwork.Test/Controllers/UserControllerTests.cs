using Newtonsoft.Json;
using SocialNetwork.API.Models;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Enums;
using SocialNetwork.Data.Repositories.Abstractions;
using SocialNetwork.Data.UnitOfWork.Abstractions;
using SocialNetwork.Test.Fixtures;
using System.Net;
using System.Text;

namespace SocialNetwork.Test.Controllers
{
    public class UserControllerTests : IClassFixture<SocialNetworkFixture>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _client;

        public UserControllerTests(SocialNetworkFixture fixture)
        {
            _client = fixture.CreateClient();
            _unitOfWork = SocialNetworkFixture.GetUnitOfWork(fixture.InMemoryDbContext);
            _userRepository = _unitOfWork.UserRepository;
        }


        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetUser_Should_Return_UserModel()
        {
            // Arrange
            User user = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser1@test.com",
                password:"test1234");

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChanges();

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
            
            string response = await result.Content.ReadAsStringAsync();
            ValidationResponseModel? validationModel =
                JsonConvert.DeserializeObject<ValidationResponseModel>(response);

            Assert.NotNull(validationModel);

            List<string> validationMessages = new List<string>
            {
                $"User with Id {nonExistentUserId} was not found"
            };
            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetFollowers_Should_Return_UserModel_List()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser2@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "Test",
                lastName: "User Follower",
                email: "testuserfollower1@test.com",
                password: "test12345")
            {
                Following = new List<User>
                {
                    user1
                }
            };

            await _userRepository.AddAsync(user1);
            await _userRepository.AddAsync(user2);
            await _unitOfWork.SaveChanges();

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

            ValidationResponseModel? validationModel =
                JsonConvert.DeserializeObject<ValidationResponseModel>(response);

            Assert.NotNull(validationModel);

            List<string> validationMessages = new List<string>
            {
                $"User with Id {nonExistentUserId} was not found"
            };

            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetFollows_Should_Return_UserModel_List()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser3@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "Test",
                lastName: "User Follower",
                email: "testuserfollower2@test.com",
                password: "test12345")
            {
                Followers = new List<User>
                {
                    user1
                }
            };

            await _userRepository.AddAsync(user1);
            await _userRepository.AddAsync(user2);
            await _unitOfWork.SaveChanges();

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

            ValidationResponseModel? validationModel =
                JsonConvert.DeserializeObject<ValidationResponseModel>(response);

            Assert.NotNull(validationModel);

            List<string> validationMessages = new List<string>
            {
                $"User with Id {nonExistentUserId} was not found"
            };

            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
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
                email: "testuser4@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "SUT",
                lastName: "User",
                email: "testuserfollower3@test.com",
                password: "test12345");

            await _userRepository.AddAsync(user1);
            await _userRepository.AddAsync(user2);
            await _unitOfWork.SaveChanges();

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

        [Fact]
        [Trait("Category", "Unit")]
        public async Task AddPersonalUser_Should_Return_PersonalUserModel()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser5@test.com",
                password:"test1234");

            PersonalUser personalUser = new PersonalUser
            {
                Age = 20,
                Genre = Genre.Female,
                Visibility = Visibility.Public,
                Status = Status.Online,
                User = user1,
            };

            PostPersonalUserModel postModel = new PostPersonalUserModel
            {
                FirstName = user1.FirstName, 
                LastName = user1.LastName,
                Email = user1.Email,
                Password = user1.Password,
                Age = personalUser.Age,
                Genre = personalUser.Genre,
                Status = personalUser.Status,
                Profile = new ProfileModel
                {
                    Description = "",
                }
            };

            string body = JsonConvert.SerializeObject(postModel);

            // Act
            HttpResponseMessage result = await _client.PostAsync("/api/users/personalUsers", new StringContent(body, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            PersonalUserModel? model = JsonConvert.DeserializeObject<PersonalUserModel>(response);
            Assert.NotNull(model);
            string fullName = $"{user1.FirstName} {user1.LastName}";
            Assert.Equal(fullName, model!.FullName);
            Assert.Equal(postModel.Age, model!.Age);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task AddPersonalUser_Should_Return_BadRequest_When_Existing_Email()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser6@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "SUT",
                lastName: "User",
                email: user1.Email,
                password: "test12345");


            PersonalUser personalUser = new PersonalUser
            {
                Age = 20,
                Genre = Genre.Female,
                Visibility = Visibility.Public,
                Status = Status.Online,
                User = user1,
            };

            PostPersonalUserModel postModel = new PostPersonalUserModel
            {
                FirstName = user2.FirstName, 
                LastName = user2.LastName,
                Email = user2.Email,
                Password = user2.Password,
                Age = personalUser.Age,
                Genre = personalUser.Genre,
                Status = personalUser.Status,
                Profile = new ProfileModel
                {
                    Description = "",
                }
            };

            await _userRepository.AddAsync(user1);
            await _unitOfWork.SaveChanges();
            string body = JsonConvert.SerializeObject(postModel);

            // Act
            HttpResponseMessage result = await _client.PostAsync("/api/users/personalUsers", new StringContent(body, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            ValidationResponseModel? validationModel = JsonConvert.DeserializeObject<ValidationResponseModel>(response);
            Assert.NotNull(validationModel);
            List<string> validationMessages = new List<string>
            {
                "Email is already in use"
            };

            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task AddEnterpriseUser_Should_Return_EnterpriseUserModel()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser7@test.com",
                password:"test1234");

            EnterpriseUser enterpriseUser = new EnterpriseUser
            {
                Category = EnterpriseCategory.Influencer
            };

            PostEnterpriseUserModel postModel = new PostEnterpriseUserModel
            {
                FirstName = user1.FirstName,
                LastName = user1.LastName,
                Email = user1.Email,
                Password = user1.Password,
                Category = enterpriseUser.Category,
                Profile = new ProfileModel
                {
                    Description = "Test Description",
                }   
            };

            string body = JsonConvert.SerializeObject(postModel);

            // Act
            HttpResponseMessage result = await _client.PostAsync("/api/users/enterpriseUsers", new StringContent(body, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            EnterpriseUserModel? model = JsonConvert.DeserializeObject<EnterpriseUserModel>(response);
            Assert.NotNull(model);
            string fullName = $"{user1.FirstName} {user1.LastName}";
            Assert.Equal(fullName, model!.FullName);
            Assert.Equal(postModel.Category, model!.Category);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task AddEnterpriseUser_Should_Return_BadRequest_When_Existing_Email()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser8@test.com",
                password:"test1234");

            User user2 = new User(
                firstName: "SUT",
                lastName: "User",
                email: user1.Email,
                password: "test12345");


            EnterpriseUser enterpriseUser = new EnterpriseUser
            {
                Category = EnterpriseCategory.Influencer,
                User = user2
            };

            PostEnterpriseUserModel postModel = new PostEnterpriseUserModel
            {
                FirstName = user2.FirstName, 
                LastName = user2.LastName,
                Email = user2.Email,
                Password = user2.Password,
                Category = enterpriseUser.Category,
                Profile = new ProfileModel
                {
                    Description = "",
                }
            };

            await _userRepository.AddAsync(user1);
            await _unitOfWork.SaveChanges();
            string body = JsonConvert.SerializeObject(postModel);

            // Act
            HttpResponseMessage result = await _client.PostAsync("/api/users/enterpriseUsers", new StringContent(body, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            ValidationResponseModel? validationModel = JsonConvert.DeserializeObject<ValidationResponseModel>(response);
            Assert.NotNull(validationModel);
            List<string> validationMessages = new List<string>
            {
                "Email is already in use"
            };

            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task AddEnterpriseUser_Should_Return_BadRequest_When_Invalid_Name()
        {
            // Arrange
            User user1 = new User(
                firstName: "VeryLongFirstNameTest",
                lastName: "VeryLongLastNameUser",
                email: "testuser9@test.com",
                password:"test12345");

            EnterpriseUser enterpriseUser = new EnterpriseUser
            {
                Category = EnterpriseCategory.Influencer,
                User = user1
            };

            PostEnterpriseUserModel postModel = new PostEnterpriseUserModel
            {
                FirstName = user1.FirstName, 
                LastName = user1.LastName,
                Email = user1.Email,
                Password = user1.Password,
                Category = enterpriseUser.Category,
                Profile = new ProfileModel
                {
                    Description = "",
                }
            };

            string body = JsonConvert.SerializeObject(postModel);

            // Act
            HttpResponseMessage result = await _client.PostAsync("/api/users/enterpriseUsers", new StringContent(body, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            ValidationResponseModel? validationModel = JsonConvert.DeserializeObject<ValidationResponseModel>(response);
            Assert.NotNull(validationModel);
            List<string> validationMessages = new List<string>
            {
                "First name must be between 1 and 15 characters long. Accepted special characters: _",
                "Last name must be at most 15 characters long. Accepted special characters: _"
            };

            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
        }
        
        [Fact]
        [Trait("Category", "Unit")]
        public async Task AddEnterpriseUser_Should_Return_BadRequest_When_Invalid_Email()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser10@test",
                password:"test12345");

            EnterpriseUser enterpriseUser = new EnterpriseUser
            {
                Category = EnterpriseCategory.Influencer,
                User = user1
            };

            PostEnterpriseUserModel postModel = new PostEnterpriseUserModel
            {
                FirstName = user1.FirstName, 
                LastName = user1.LastName,
                Email = user1.Email,
                Password = user1.Password,
                Category = enterpriseUser.Category,
                Profile = new ProfileModel
                {
                    Description = "Description",
                }
            };

            string body = JsonConvert.SerializeObject(postModel);

            // Act
            HttpResponseMessage result = await _client.PostAsync("/api/users/enterpriseUsers", new StringContent(body, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            ValidationResponseModel? validationModel = JsonConvert.DeserializeObject<ValidationResponseModel>(response);
            Assert.NotNull(validationModel);
            List<string> validationMessages = new List<string>
            {
                "Email is not valid"
            };

            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
        }
        
        [Fact]
        [Trait("Category", "Unit")]
        public async Task AddEnterpriseUser_Should_Return_BadRequest_When_Invalid_Description()
        {
            // Arrange
            User user1 = new User(
                firstName: "Test",
                lastName: "User",
                email: "testuser11@test",
                password:"test12345");

            EnterpriseUser enterpriseUser = new EnterpriseUser
            {
                Category = EnterpriseCategory.Influencer,
                User = user1
            };

            PostEnterpriseUserModel postModel = new PostEnterpriseUserModel
            {
                FirstName = user1.FirstName, 
                LastName = user1.LastName,
                Email = user1.Email,
                Password = user1.Password,
                Category = enterpriseUser.Category,
                Profile = new ProfileModel
                {
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
                    + " Aliquam et maximus neque. Pellentesque eget tortor vehicula, posuere turpis a donec."
                }
            };

            string body = JsonConvert.SerializeObject(postModel);

            // Act
            HttpResponseMessage result = await _client.PostAsync("/api/users/enterpriseUsers", new StringContent(body, Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            string response = await result.Content.ReadAsStringAsync();
            ValidationResponseModel? validationModel = JsonConvert.DeserializeObject<ValidationResponseModel>(response);
            Assert.NotNull(validationModel);
            List<string> validationMessages = new List<string>
            {
                "Description must be at most 140 characters long"
            };

            Assert.True(CheckValidationMessages(validationMessages, validationModel!));
        }

        #region Private Methods
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

        private bool CheckValidationMessages(IEnumerable<string> validationMessages, ValidationResponseModel validationModel)
        {
            bool isValidModel = true;

            foreach (var message in validationMessages)
            {
                isValidModel = validationModel.Validations
                    .Any(v => v.Messages.Any(m => m.Equals(message)));

                if (!isValidModel)
                    break;
            }

            return isValidModel;
        }
        #endregion
    }
}
