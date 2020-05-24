﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AeroCar.Models;
using AeroCar.Models.DTO;
using AeroCar.Models.Registration;
using AeroCar.Models.Users;
using AeroCar.Services;
using AeroCar.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(UserService userService, IConfiguration configuration)
        {
            UserService = userService;
            _configuration = configuration;
        }

        public UserService UserService { get; set; }

        // POST api/user/register
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistration model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match!");
                return BadRequest(ModelState);
            }

            var user = new RegularUser()
            {
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                City = model.City,
                Phone = model.Phone
            };

            var result = await UserService.RegisterUser(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(200);
        }
        
        // GET api/user/validate
        [AllowAnonymous]
        [HttpGet]
        [Route("validate")]
        public async Task<IActionResult> ValidateUser(string email, bool validate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await UserService.ValidateUser(email, validate);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(200);
        }

        // POST api/user/logout
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await UserService.LogoutCurrentUser();
            return Ok(200);
        }

        // POST api/user/login
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserService.LoginUser(model);
                
                if (result != null && result.Succeeded)
                {
                    var user = await UserService.GetUserByUsernameAndPassword(model.Username, model.Password);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName)
                        }),
                        Expires = DateTime.UtcNow.AddYears(3),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var t = tokenHandler.WriteToken(token);
                    return Ok(new { t, model.RedirectUrl });
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return BadRequest(ModelState);
        }

        // GET api/user/current
        [AllowAnonymous]
        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetCurrentUser();
                
                if (user != null)
                {
                    return Ok(new { user });
                }
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return StatusCode(500);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Code + " " + error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}