using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models;
using AeroCar.Models.Avio;
using AeroCar.Models.DTO.Avio;
using AeroCar.Models.Rating;
using AeroCar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroCar.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AvioController : ControllerBase
    {
        public AvioService AvioService { get; set; }

        public DestinationService DestinationService { get; set; }

        public AvioController(AvioService avioSerivce, DestinationService destinationService)
        {
            AvioService = avioSerivce;
            DestinationService = destinationService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("company/get")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            List<AvioCompanyProfileDTO> avioCompanyProfileDTOList = new List<AvioCompanyProfileDTO>();

            if (ModelState.IsValid)
            {
                List<AvioCompany> companies = await AvioService.GetCompanies();
                List<AvioCompanyProfile> companiesProfile = new List<AvioCompanyProfile>();
                List<double> avioCompanyRating = new List<double>();
                List<int> avioCompanyRatingPicture = new List<int>();

                foreach (var avioCompany in companies)
                {
                    companiesProfile.Add(await AvioService.GetCompanyProfile(avioCompany.AvioCompanyId));
                    avioCompanyRating.Add(await AvioService.GetAverageCompanyRating(avioCompany.AvioCompanyId));
                    avioCompanyRatingPicture.Add((int)(Math.Round(await AvioService.GetAverageCompanyRating(avioCompany.AvioCompanyId))));      
                }

                
                for (int i = 0; i < companies.Count; i++)
                {
                    string allDestinations = "";
                    for (int j = 0; j < companies[i].Destinations.Count; j++)
                    {
                        allDestinations += companies[i].Destinations[j].Name + ",";
                    }

                    AvioCompanyProfileDTO acpDTO = new AvioCompanyProfileDTO()
                    {
                        Id = companies[i].AvioCompanyId,
                        Name = companiesProfile[i].Name,
                        Destinations = allDestinations,
                        Address = companiesProfile[i].Address,
                        Description = companiesProfile[i].PromoDescription,
                        Rating = avioCompanyRating[i],
                        RatingPicture = avioCompanyRatingPicture[i]
                    };
                    avioCompanyProfileDTOList.Add(acpDTO);
                }

                return Ok(new { avioCompanyProfileDTOList });
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("company/details/get/{id}")]
        public async Task<IActionResult> GetCompanyDetails(long id)
        {
            AvioCompanyProfileDTO avioCompanyProfileDTO = new AvioCompanyProfileDTO();

            if (ModelState.IsValid)
            {
                AvioCompany company = await AvioService.GetCompany(id);
                AvioCompanyProfile companyProfile = new AvioCompanyProfile();
                int avioCompanyRatingPicture = 0;
                List<Destination> destinationList = company.Destinations;

                string allDestinations = "";
                for (int i = 0; i < destinationList.Count; i++)
                {
                    allDestinations += destinationList[i].Name + ",";
                }

                companyProfile = await AvioService.GetCompanyProfile(id);
                avioCompanyRatingPicture = (int)(Math.Round(await AvioService.GetAverageCompanyRating(id)));

                avioCompanyProfileDTO.Id = company.AvioCompanyId;
                avioCompanyProfileDTO.Name = companyProfile.Name;
                avioCompanyProfileDTO.RatingPicture = avioCompanyRatingPicture;
                avioCompanyProfileDTO.Address = companyProfile.Address;
                avioCompanyProfileDTO.Description = companyProfile.PromoDescription;
                avioCompanyProfileDTO.Destinations = allDestinations;
                avioCompanyProfileDTO.DestinationList = destinationList;

                return Ok(new { avioCompanyProfileDTO });
            }
            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }
    }
}
