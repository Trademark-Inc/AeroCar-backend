using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models.Users;
using AeroCar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AeroCar.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        public SystemController(UserService userService)
        {
            UserService = userService;
        }

        public UserService UserService { get; set; }

        // GET api/system/check
        [HttpGet]
        [Route("check")]
        public async Task<IActionResult> CheckRole()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(404);
            }

            var user = await UserService.GetCurrentUser();
            var roles = await UserService.GetUserRoles(user.Id);

            if (!roles.Contains("SystemAdmin"))
            {
                return BadRequest(404);
            }

            return Ok(200);
        }
    }
}