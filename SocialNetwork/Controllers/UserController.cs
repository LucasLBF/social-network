using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;
using SocialNetwork.API.Services.Abstractions;
using SocialNetwork.Data.Entities;
using System.Text.RegularExpressions;

namespace SocialNetwork.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private TimeSpan MATCH_TIMEOUT = TimeSpan.FromMilliseconds(100);
        public UserController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            User? user = await _userService.GetUser(userId);

            if (user == null)
            {
                ValidationModel.AddValidation("", $"User with Id {userId} was not found");
                return NotFound(ValidationModel);
            }

            UserModel model = _mapper.Map<UserModel>(user);

            return Ok(model);
        }

        [HttpGet("followers/{userId}")]
        public async Task<IActionResult> GetFollowers(int userId)
        {
            if (!(await _userService.CheckIfExists(userId)))
            {
                ValidationModel.AddValidation("", $"User with Id {userId} was not found");
                return BadRequest(ValidationModel);
            }

            IEnumerable<User> followers = await _userService.GetFollowers(userId);

            IEnumerable<UserModel> followerModels = _mapper.Map<IEnumerable<UserModel>>(followers);

            return Ok(followerModels);
        }

        [HttpGet("following/{userId}")]
        public async Task<IActionResult> GetFollows(int userId)
        {
            if (!(await _userService.CheckIfExists(userId)))
            {
                ValidationModel.AddValidation("", $"User with Id {userId} was not found");
                return BadRequest(ValidationModel);
            }

            IEnumerable<User> following = await _userService.GetFollowing(userId);

            IEnumerable<UserModel> followingModels = _mapper.Map<IEnumerable<UserModel>>(following);

            return Ok(followingModels);
        }

        [HttpGet()]
        public async Task<IActionResult> GetUsersByName([FromQuery] string? name)
        {
            IEnumerable<User> users = await _userService.GetUsersByName(name);

            IEnumerable<UserModel> usersModels = _mapper.Map<IEnumerable<UserModel>>(users);

            return Ok(usersModels);
        }

        [HttpPost("personalUsers")]
        public async Task<IActionResult> AddPersonalUser([FromBody] PostPersonalUserModel model)
        {
            ValidatePostUserModel(model);

            if (ValidationModel.HasError)
            {
                return BadRequest(ValidationModel);
            }

            User user = _mapper.Map<User>(model);
            PersonalUser personalUser = _mapper.Map<PersonalUser>(model);
            personalUser.User = user;

            await _userService.AddUser(user, personalUser);

            PersonalUserModel userModel = _mapper.Map<PersonalUserModel>((user, personalUser));

            return Created($"api/users/{user.Id}", userModel);
        }

        [HttpPost("enterpriseUsers")]
        public async Task<IActionResult> AddEntrepriseUser([FromBody] PostEnterpriseUserModel model)
        {
            ValidatePostUserModel(model);

            if (ValidationModel.HasError)
            {
                return BadRequest(ValidationModel);
            }   

            User user = _mapper.Map<User>(model);
            EnterpriseUser enterpriseUser = _mapper.Map<EnterpriseUser>(model);
            enterpriseUser.User = user;

            await _userService.AddUser(user, enterpriseUser);

            EnterpriseUserModel userModel = _mapper.Map<EnterpriseUserModel>((user, enterpriseUser));

            return Created($"api/users/{user.Id}", userModel);
        }

        #region Private Methods
        private async void ValidatePostUserModel(PostUserModel model)
        {
            ValidateName(model.FirstName, model.LastName);
            await ValidateEmail(model.Email);
            ValidatePassword(model.Password);
            ValidateProfile(model.Profile);
        }

        private void ValidateName(string firstName, string lastName) 
        { 
            if (string.IsNullOrWhiteSpace(firstName))
            {
                ValidationModel.AddValidation("FirstName", "First name is required");
                return;
            }

            if (!Regex.IsMatch(firstName, @"^[\w]{1,15}$", RegexOptions.None, MATCH_TIMEOUT))
            {
                ValidationModel.AddValidation("FirstName", "First name must be between 1 and 15 characters long." +
                    " Accepted special characters: _");
            }

            if (!Regex.IsMatch(lastName, @"^[\w]{0,15}$", RegexOptions.None, MATCH_TIMEOUT))
            {
                ValidationModel.AddValidation("LastName", "Last name must be at most 15 characters long." +
                    " Accepted special characters: _");
            }
        }

        private async Task ValidateEmail(string email) 
        { 
            if (string.IsNullOrWhiteSpace(email))
            {
                ValidationModel.AddValidation("Email", "Email is required");
                return;
            }

            if (await _userService.CheckExistingEmail(email))
            {
                ValidationModel.AddValidation("Email", "Email is already in use");
                return;
            }

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.None ,MATCH_TIMEOUT))
            {
                ValidationModel.AddValidation("Email", "Email is not valid");
            }
        }

        private void ValidatePassword(string password) 
        { 
            // Will be implemented in the future
        }

        private void ValidateProfile(ProfileModel profile) 
        { 
            if (profile.Description.Length > 140)
            {
                ValidationModel.AddValidation("Description", "Description must be at most 140 characters long");
            }
        }
        #endregion
    }
}
