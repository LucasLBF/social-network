﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;
using SocialNetwork.API.Services.Abstractions;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
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
                return NotFound();
            }

            UserModel model = _mapper.Map<UserModel>(user);

            return Ok(model);
        }

        [HttpGet("followers/{userId}")]
        public async Task<IActionResult> GetFollowers(int userId)
        {
            IEnumerable<User> followers = await _userService.GetFollowers(userId);

            IEnumerable<UserModel> followerModels = _mapper.Map<IEnumerable<UserModel>>(followers);

            return Ok(followerModels);
        }

        [HttpGet("following/{userId}")]
        public async Task<IActionResult> GetFollows(int userId)
        {
            IEnumerable<User> following = await _userService.GetFollowing(userId);

            IEnumerable<UserModel> followingModels = _mapper.Map<IEnumerable<UserModel>>(following);

            return Ok(followingModels);
        }
    }
}