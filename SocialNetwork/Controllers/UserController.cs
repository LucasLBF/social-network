﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;
using SocialNetwork.API.Services.Abstractions;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
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
    }
}
