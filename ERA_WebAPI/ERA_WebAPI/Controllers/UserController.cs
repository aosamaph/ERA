using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERA_WebAPI.Data;
using ERA_WebAPI.ERA.Models;
using ERA_WebAPI.ERA.Models.UserModels;
using ERA_WebAPI.ERA.Models.UserModels.responseMessage;
using ERA_WebAPI.ERA.Models.UserModels.services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ERA_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController :ControllerBase
    {


        private IUserService _userService;

        public ERAContext DB { get; set; }

        public UserController(ERAContext context, IUserService userService)
        {
            DB = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> getbyId(string Id)
        {
            if (Id == null) return BadRequest("Null ID!!");

            try
            {
                userResponseMessage userResponseMessage = await _userService.GetUserAsync(Id);
                if (userResponseMessage.IsSuccess)
                {
                    return Ok(userResponseMessage);
                }
                return NotFound(userResponseMessage);
            }
            catch
            {
                return NotFound("error to get data");
            }


        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditUserAsync(AppUser user, string id)
        {
            if (id == null) return BadRequest("Null ID!!");

            try
            {
                userResponseMessage userResponseMessage = await _userService.EditUserAsync(user, id);
                if (userResponseMessage.IsSuccess)
                {
                    return Ok(userResponseMessage);
                }
                return NotFound(userResponseMessage);
            }
            catch
            {
                return NotFound("error to get data");
            }
        }


    }
}