using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models;
using AeroCar.Models.Registration;
using AeroCar.Models.Users;
using AeroCar.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(UserManager<RegularUser> userManager,
                              SignInManager<RegularUser> signInManager,
                              RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public UserManager<RegularUser> UserManager { get; set; }
        public SignInManager<RegularUser> SignInManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }

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

            EmailUtility.SendEmail(user.Email, "Profile Status", "Your profile is being verified." +
                "\nPlease go to the following link to verify your account: http://localhost:62541/api/user/validate?email=" + user.Email + "&validate=true");
            user.Status = UserStatus.InProcess;

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var rmResult = await RoleManager.RoleExistsAsync("RegularUser");
            if (!rmResult)
            {
                result = await RoleManager.CreateAsync(new IdentityRole("RegularUser"));

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }

            result = await UserManager.AddToRoleAsync(user, "RegularUser");

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
            RegularUser applicationUser = await UserManager.FindByEmailAsync(email);
            if (validate)
            {
                EmailUtility.SendEmail(email, "Profile Status", "Your profile has been verified. Congratulations!");
                applicationUser.Status = UserStatus.Activated;
            }
            else
            {
                EmailUtility.SendEmail(email, "Profile Status", "Your profile has been declined.");
                applicationUser.Status = UserStatus.Declined;
            }

            IdentityResult result = await UserManager.UpdateAsync(applicationUser);
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
            await SignInManager.SignOutAsync();
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
                var user = await UserManager.FindByNameAsync(model.Username);

                if (user.Status == UserStatus.Activated)
                {
                    var result = await SignInManager.PasswordSignInAsync(user, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        return Ok(200);
                    }
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
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