using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models.Admin;
using AeroCar.Models.Avio;
using AeroCar.Models.Car;
using AeroCar.Models.DTO.Admin;
using AeroCar.Models.DTO.Avio;
using AeroCar.Models.DTO.Car;
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
        public SystemController(UserManager<RegularUser> userManager, UserService userService, AvioService avioService, RentACarService rentACarService,
            AvioAdminService avioAdminService, CarAdminService carAdminService)
        {
            UserManager = userManager;
            UserService = userService;
            AvioService = avioService;
            RentACarService = rentACarService;
            AvioAdminService = avioAdminService;
            CarAdminService = carAdminService;
        }

        public UserManager<RegularUser> UserManager { get; set; }
        public UserService UserService { get; set; }
        public AvioService AvioService { get; set; }
        public RentACarService RentACarService { get; set; }
        public AvioAdminService AvioAdminService { get; set; }
        public CarAdminService CarAdminService { get; set; }

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

        #region Avio Company Admin Control Methods
        // GET api/system/avio/get/all
        [HttpGet]
        [Route("avio/get/all")]
        public async Task<IActionResult> GetAvioCompanies()
        {
            var avioCompanies = await AvioService.GetCompanies();

            List<AvioCompanyDTO> retVal = new List<AvioCompanyDTO>();
            foreach (AvioCompany avioCompany in avioCompanies)
            {
                var avioCompanyProfile = await AvioService.GetCompanyProfile(avioCompany.AvioCompanyProfileId);
                retVal.Add(new AvioCompanyDTO()
                {
                    Id = avioCompany.AvioCompanyId,
                    Name = avioCompanyProfile.Name,
                    Address = avioCompanyProfile.Address,
                    Description = avioCompanyProfile.PromoDescription
                });
            }

            return Ok(retVal);
        }

        // POST api/system/avio/create
        [HttpPost]
        [Route("avio/create")]
        public async Task<IActionResult> CreateAvioCompany([FromBody]AvioCompanyDTO avioCompanyDTO)
        {
            if (ModelState.IsValid)
            {
                if (await AvioService.CompanyExists(avioCompanyDTO.Name))
                {
                    return BadRequest("Company already exists with the same name.");
                }

                var profile = new AvioCompanyProfile()
                {
                    Name = avioCompanyDTO.Name,
                    Address = avioCompanyDTO.Address,
                    PromoDescription = avioCompanyDTO.Description
                };

                await AvioService.CreateCompany(new AvioCompany(), profile);

                return Ok(200);
            }

            return BadRequest("No sufficient data provided.");
        }

        // POST api/system/avio/remove/{id}
        [HttpPost]
        [Route("avio/remove/{id}")]
        public async Task<IActionResult> RemoveAvioCompany(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await AvioService.RemoveCompany(id);

                if (result) return Ok(200);
                else return BadRequest("Company wasn't found.");
            }

            return BadRequest("No ID provided.");
        }
        #endregion

        #region Car Company Admin Control Methods
        // GET api/system/car/get/all
        [HttpGet]
        [Route("car/get/all")]
        public async Task<IActionResult> GetCarCompanies()
        {
            var carCompanies = await RentACarService.GetCompanies();

            List<CarCompanyDTO> retVal = new List<CarCompanyDTO>();
            foreach (CarCompany carCompany in carCompanies)
            {
                var carCompanyProfile = await RentACarService.GetCompanyProfile(carCompany.CarCompanyProfileId);
                retVal.Add(new CarCompanyDTO()
                {
                    Id = carCompany.CarCompanyId,
                    Name = carCompanyProfile.Name,
                    Address = carCompanyProfile.Address,
                    Description = carCompanyProfile.PromoDescription
                });
            }

            return Ok(retVal);
        }

        // POST api/system/car/create
        [HttpPost]
        [Route("car/create")]
        public async Task<IActionResult> CreateCarCompany([FromBody]CarCompanyDTO carCompanyDTO)
        {
            if (ModelState.IsValid)
            {
                if (await RentACarService.CompanyExists(carCompanyDTO.Name))
                {
                    return BadRequest("Company already exists.");
                }

                var profile = new CarCompanyProfile()
                {
                    Name = carCompanyDTO.Name,
                    Address = carCompanyDTO.Address,
                    PromoDescription = carCompanyDTO.Description
                };

                await RentACarService.CreateCompany(profile);

                return Ok(200);
            }

            return BadRequest("No sufficient data provided.");
        }

        // POST api/system/car/remove/{id}
        [HttpPost]
        [Route("car/remove/{id}")]
        public async Task<IActionResult> RemoveCarCompany(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await RentACarService.RemoveCompany(id);

                if (result) return Ok(200);
                else return BadRequest("Company wasn't found.");
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion

        #region Subadmins Admin Control Methods
        // GET api/system/car/get/all
        [HttpGet]
        [Route("admin/get/all")]
        public async Task<IActionResult> GetAdmins()
        {
            List<AdminDTO> retVal = new List<AdminDTO>();

            var avioAdmins = await AvioAdminService.GetAdmins();
            foreach (AvioAdmin admin in avioAdmins)
            {
                var user = await AvioAdminService.GetAdminUser(admin.UserId);

                if (admin.AvioCompanyId > 0)
                {
                    var company = await AvioService.GetCompany(admin.AvioCompanyId);

                    if (company != null)
                    {
                        var companyProfile = await AvioService.GetCompanyProfile(company.AvioCompanyProfileId);

                        retVal.Add(new AdminDTO()
                        {
                            Id = user.Id,
                            Username = user.UserName,
                            AdminType = "Avio Company Admin",
                            Company = companyProfile.Name
                        });
                    }
                }
            }

            var carAdmins = await CarAdminService.GetAdmins();
            foreach (CarAdmin admin in carAdmins)
            {
                var user = await CarAdminService.GetAdminUser(admin.UserId);

                if (admin.CarCompanyId > 0)
                {
                    var company = await RentACarService.GetCompany(admin.CarCompanyId);

                    if (company != null)
                    {
                        var companyProfile = await RentACarService.GetCompanyProfile(company.CarCompanyProfileId);

                        retVal.Add(new AdminDTO()
                        {
                            Id = user.Id,
                            Username = user.UserName,
                            AdminType = "Car Company Admin",
                            Company = companyProfile.Name
                        });
                    }
                }
            }

            return Ok(retVal);
        }

        // POST api/system/admin/avio/create
        [HttpPost]
        [Route("admin/avio/create")]
        public async Task<IActionResult> CreateAvioAdmin([FromBody]RegisterAdminDTO adminDTO)
        {
            if (ModelState.IsValid)
            {
                if (await AvioAdminService.AdminExists(adminDTO.Username))
                {
                    return BadRequest("Admin already exists with that username.");
                }

                if (await CarAdminService.AdminExists(adminDTO.Username))
                {
                    return BadRequest("Admin already exists with that username.");
                }

                if (adminDTO.Password != adminDTO.ConfirmPassword)
                {
                    return BadRequest("Password and confirmation password don't match.");
                }

                RegularUser user = new RegularUser()
                {
                    UserName = adminDTO.Username
                };

                var foundAdmin = await UserManager.FindByNameAsync(user.UserName) != null;

                if (!foundAdmin)
                {
                    var createdAdmin = await UserManager.CreateAsync(user, adminDTO.Password);
                    if (createdAdmin.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user, "AvioAdmin");

                        AvioAdmin admin = new AvioAdmin()
                        {
                            UserId = user.Id,
                            AvioCompanyId = (await AvioService.GetCompanyByName(adminDTO.CompanyName)).AvioCompanyId
                        };

                        if (admin.AvioCompanyId > 0)
                        {
                            await AvioAdminService.RegisterAdmin(user.Id, admin);

                            return Ok(200);
                        }
                        else
                        {
                            return BadRequest("Avio company not found.");
                        }
                    }
                }

                return BadRequest("Admin already exists.");
            }

            return BadRequest("No sufficient data provided.");
        }

        // POST api/system/admin/avio/remove/{id}
        [HttpPost]
        [Route("admin/avio/remove/{id}")]
        public async Task<IActionResult> RemoveAvioAdmin(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await AvioAdminService.RemoveAdminById(id);

                if (result) return Ok(200);
                else return BadRequest("Admin wasn't found.");
            }

            return BadRequest("No sufficient data provided.");
        }

        // POST api/system/admin/car/create
        [HttpPost]
        [Route("admin/car/create")]
        public async Task<IActionResult> CreateCarAdmin([FromBody]RegisterAdminDTO adminDTO)
        {
            if (ModelState.IsValid)
            {
                if (await AvioAdminService.AdminExists(adminDTO.Username))
                {
                    return BadRequest("Admin already exists with that username!");
                }

                if (await CarAdminService.AdminExists(adminDTO.Username))
                {
                    return BadRequest("Admin already exists with that username!");
                }

                if (adminDTO.Password != adminDTO.ConfirmPassword)
                {
                    return BadRequest("Password and confirmation password don't match!");
                }

                RegularUser user = new RegularUser()
                {
                    UserName = adminDTO.Username
                };

                var foundAdmin = await UserManager.FindByNameAsync(user.UserName) != null;

                if (!foundAdmin)
                {
                    var createdAdmin = await UserManager.CreateAsync(user, adminDTO.Password);
                    if (createdAdmin.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user, "CarAdmin");

                        CarAdmin admin = new CarAdmin()
                        {
                            UserId = user.Id,
                            CarCompanyId = (await RentACarService.GetCompanyByName(adminDTO.CompanyName)).CarCompanyId
                        };

                        if (admin.CarCompanyId > 0)
                        {
                            await CarAdminService.RegisterAdmin(user.Id, admin);

                            return Ok(200);
                        }
                        else
                        {
                            return BadRequest("Car company not found!");
                        }
                    }
                }

                return BadRequest("Admin already exists!");
            }

            return BadRequest("No sufficient data provided.");
        }

        // POST api/system/admin/car/remove/{id}
        [HttpPost]
        [Route("admin/car/remove/{id}")]
        public async Task<IActionResult> RemoveCarAdmin(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await CarAdminService.RemoveAdminById(id);

                if (result) return Ok(200);
                else return BadRequest("Admin wasn't found.");
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion
    }
}