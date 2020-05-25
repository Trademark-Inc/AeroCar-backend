using AeroCar.Models.Admin;
using AeroCar.Models.Avio;
using AeroCar.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AvioAdminController : ControllerBase
    {
        public AvioAdminController(UserManager<RegularUser> userManager, SignInManager<RegularUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }
        public UserManager<RegularUser> UserManager { get; set; }
        public SignInManager<RegularUser> SignInManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
    }
}
