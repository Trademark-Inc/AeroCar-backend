using AeroCar.Models.Admin;
using AeroCar.Models.Avio;
using AeroCar.Models.DTO.Avio;
using AeroCar.Models.Users;
using AeroCar.Services;
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
        public AvioAdminController(AvioService avioService, AvioAdminService avioAdminService, PriceListItemService priceListItemService)
        {
            AvioService = avioService;
            AvioAdminService = avioAdminService;
            PriceListItemService = priceListItemService;
        }

        public AvioService AvioService { get; set; }
        public AvioAdminService AvioAdminService { get; set; }
        public PriceListItemService PriceListItemService { get; set; }

        // GET api/avioadmin/company/get/profile
        [HttpGet]
        [Route("company/get/profile")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var avioCompanyProfile = await AvioService.GetCompanyProfile(avioCompany.AvioCompanyProfileId);

                        return Ok(new { avioCompany, avioCompanyProfile });
                    }
                }
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        // POST api/avioadmin/company/update/profile
        [HttpPost]
        [Route("company/update/profile")]
        public async Task<IActionResult> UpdateCompanyProfile([FromBody] AvioCompanyProfile model)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var avioCompanyProfile = await AvioService.GetCompanyProfile(avioCompany.AvioCompanyProfileId);

                        avioCompanyProfile.Name = model.Name;
                        avioCompanyProfile.Address = model.Address;
                        avioCompanyProfile.PromoDescription = model.PromoDescription;

                        await AvioService.UpdateCompanyProfile(avioCompanyProfile);
                        return Ok(200);
                    }
                }
            }

            return BadRequest("Not enough data provided.");
        }

        #region Price List Items
        // POST api/avioadmin/company/create/item
        [HttpPost]
        [Route("company/create/item")]
        public async Task<IActionResult> CreateItem([FromBody] PriceListItemDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        PriceListItem item = new PriceListItem()
                        {
                            Name = model.Name,
                            Price = model.Price
                        };

                        avioCompany.PriceList.Add(item);
                        await AvioService.UpdateCompany(avioCompany);

                        return Ok(200);
                    }
                }
            }

            return BadRequest("Not enough data provided.");
        }

        // POST api/avioadmin/company/remove/item/{id}
        [HttpPost]
        [Route("company/remove/item/{id}")]
        public async Task<IActionResult> RemoveItem(long id)
        {
            if (ModelState.IsValid)
            {
                var user = await AvioAdminService.GetCurrentUser();

                if (user != null)
                {
                    var avioCompany = await AvioService.GetCompany(user.AvioCompanyId);

                    if (avioCompany != null)
                    {
                        var item = avioCompany.PriceList.Where(t => t.PriceListIdemId == id).SingleOrDefault();

                        if (item != null)
                        {
                            await PriceListItemService.RemovePriceListItem(item);

                            return Ok(200);
                        }
                    }
                    else return BadRequest("Company wasn't found.");
                }
            }

            return BadRequest("No sufficient data provided.");
        }
        #endregion
    }
}
