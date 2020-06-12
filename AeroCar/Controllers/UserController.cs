using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AeroCar.Models;
using AeroCar.Models.Avio;
using AeroCar.Models.Car;
using AeroCar.Models.DTO;
using AeroCar.Models.DTO.Avio;
using AeroCar.Models.DTO.Rate;
using AeroCar.Models.DTO.Registration;
using AeroCar.Models.DTO.Reservation;
using AeroCar.Models.Rating;
using AeroCar.Models.Registration;
using AeroCar.Models.Reservation;
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
        private readonly UserService _userService;
        private readonly FlightService _flightService;
        private readonly VehicleService _vehicleService;
        private readonly ReservationService _reservationService;
        private readonly AvioService _avioService;
        private readonly RentACarService _rentACarService;

        public UserController(UserService userService, FlightService flightService, ReservationService reservationService, 
            AvioService avioService, VehicleService vehicleService, RentACarService rentACarService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _flightService = flightService;
            _reservationService = reservationService;
            _avioService = avioService;
            _vehicleService = vehicleService;
            _rentACarService = rentACarService;
        }

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

            var result = await _userService.RegisterUser(user, model.Password);

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
        public async Task<IActionResult> ValidateUser(string username, bool validate, string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ValidateUser(username, validate, email);

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
            await _userService.LogoutCurrentUser();
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
                var result = await _userService.LoginUser(model);
                
                if (result != null && result.Succeeded)
                {
                    var user = await _userService.GetUserByUsernameAndPassword(model.Username, model.Password);

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
        
        // POST api/user/update
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateProfile([FromBody]UserUpdate model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetCurrentUser();

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Phone = model.Phone;
                user.Email = model.Email;
                user.City = model.City;

                await _userService.UpdateUser(user);
                return Ok(200);
            }

            return BadRequest("Not enough data provided.");
        }

        // GET api/user/friends
        [HttpGet]
        [Route("friends")]
        public async Task<IActionResult> GetFriends()
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetCurrentUser();

                if (user != null)
                {
                    var friends = await _userService.GetUserFriends(user.Id);

                    List<FriendDTO> retVal = new List<FriendDTO>();
                    foreach (var friend in friends)
                    {
                        var friendUser = await _userService.GetUserById(friend.FriendId);
                        retVal.Add(new FriendDTO() 
                        {
                            Id = friendUser.Id,
                            Username = friendUser.UserName,
                            Name = friendUser.Name,
                            Surname = friendUser.Surname,
                            Email = friendUser.Email,
                        });
                    }

                    return Ok(retVal);
                }
            }

            ModelState.AddModelError("", "Cannot retrieve user data.");
            return BadRequest(ModelState);
        }

        // POST api/user/friends/add/{username}
        [HttpPost]
        [Route("friends/add/{username}")]
        public async Task<IActionResult> AddFriend(string username)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetCurrentUser();

                if (user != null)
                {
                    if (user.UserName == username) return BadRequest("Friend cannot be the same user!");

                    await _userService.AddFriend(user, username);
                    return Ok(200);
                }
            }

            return BadRequest("No username provided.");
        }

        // POST api/user/friends/remove/{username}
        [HttpPost]
        [Route("friends/remove/{username}")]
        public async Task<IActionResult> RemoveFriend(string username)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetCurrentUser();

                if (user != null)
                {
                    await _userService.RemoveFriend(user, username);
                    return Ok(200);
                }
            }

            return BadRequest("No username provided.");
        }

        [HttpGet]
        [Route("flight/invitations")]
        public async Task<IActionResult> GetUserInvitations()
        {
            var user = await _userService.GetCurrentUser();

            if (user != null)
            {
                var invitations = await _userService.GetUserInvitations(user.Id);

                if (invitations != null)
                {
                    List<InvitationDTO> flightInvitations = new List<InvitationDTO>();
                    
                    foreach (Invitation i in invitations)
                    {
                        var fromUser = await _userService.GetUserById(i.FromUserId);
                        var flight = await _flightService.GetFlight(i.FlightId);

                        flightInvitations.Add(new InvitationDTO()
                        {
                            FlightId = i.FlightId,
                            FriendUsername = fromUser.UserName,
                            Id = i.InvitationId,
                            FlightDestination = flight.ArrivalLocation.Name
                        });
                    }

                    return Ok(new { flightInvitations });
                }

                return BadRequest("Invitations not found!");
            }

            return BadRequest("User not found.");
        }

        [HttpGet]
        [Route("history/flights")]
        public async Task<IActionResult> GetFlightsHistory()
        {
            var user = await _userService.GetCurrentUser();

            if (user != null)
            {
                var reservations = user.ReservedFlights;

                if (reservations != null)
                {
                    List<FlightHistoryDTO> flightsHistory = new List<FlightHistoryDTO>();

                    foreach (FlightReservation fr in reservations)
                    {
                        var flight = await _flightService.GetFlight(fr.FlightId);

                        if (flight != null)
                        {
                            if (DateTime.Now > (flight.Departure.AddHours(-3)))
                            {
                                var company = await _avioService.GetCompany(flight.AvioCompanyId);

                                if (company != null)
                                {
                                    var companyProfile = await _avioService.GetCompanyProfile(company.AvioCompanyProfileId);

                                    if (companyProfile != null)
                                    {
                                        flightsHistory.Add(new FlightHistoryDTO()
                                        {
                                            ReservationId = fr.FlightReservationId,
                                            DepartureLocation = flight.DepartureLocation,
                                            ArrivalLocation = flight.ArrivalLocation,
                                            Departure = flight.Departure,
                                            AvioCompanyName = companyProfile.Name
                                        });
                                    }
                                }
                            }
                        }
                    }

                    return Ok(new { flightsHistory });
                }

                return BadRequest("No reservations found!");
            }

            return BadRequest("User not found.");
        }

        [HttpGet]
        [Route("history/cars")]
        public async Task<IActionResult> GetCarsHistory()
        {
            var user = await _userService.GetCurrentUser();

            if (user != null)
            {
                var reservations = user.ReservedCars;

                if (reservations != null)
                {
                    List<CarHistoryDTO> carsHistory = new List<CarHistoryDTO>();

                    foreach (CarReservation cr in reservations)
                    {
                        if (DateTime.Now > (cr.PickUpDate.AddDays(-2)))
                        {
                            var vehicle = await _vehicleService.GetVehicleById(cr.VehicleId);

                            if (vehicle != null)
                            {
                                var company = await _rentACarService.GetCompany(vehicle.CarCompanyId);

                                if (company != null)
                                {
                                    var companyProfile = await _rentACarService.GetCompanyProfile(company.CarCompanyProfileId);

                                    if (companyProfile != null)
                                    {
                                        carsHistory.Add(new CarHistoryDTO()
                                        {
                                            ReservationId = cr.CarReservationId,
                                            CarCompanyName = companyProfile.Name,
                                            VehicleName = vehicle.Name,
                                            PickUpDate = cr.PickUpDate,
                                            ReturnDate = cr.ReturnDate,
                                            PickUpLocation = cr.PickUpLocation.Name,
                                            ReturnLocation = cr.ReturnLocation.Name
                                        });
                                    }
                                }
                            }
                        }
                    }

                    return Ok(new { carsHistory });
                }

                return BadRequest("No car reservations found!");
            }

            return BadRequest("User not found.");
        }

        [HttpGet]
        [Route("reservations/flights")]
        public async Task<IActionResult> GetFlightsTerminableReservations()
        {
            var user = await _userService.GetCurrentUser();

            if (user != null)
            {
                var reservations = user.ReservedFlights;

                if (reservations != null)
                {
                    List<FlightHistoryDTO> flightsHistory = new List<FlightHistoryDTO>();

                    foreach (FlightReservation fr in reservations)
                    {
                        var flight = await _flightService.GetFlight(fr.FlightId);

                        if (flight != null)
                        {
                            if (DateTime.Now <= (flight.Departure.AddHours(-3)))
                            {
                                var company = await _avioService.GetCompany(flight.AvioCompanyId);

                                if (company != null)
                                {
                                    var companyProfile = await _avioService.GetCompanyProfile(company.AvioCompanyProfileId);

                                    if (companyProfile != null)
                                    {
                                        flightsHistory.Add(new FlightHistoryDTO()
                                        {
                                            ReservationId = fr.FlightReservationId,
                                            DepartureLocation = flight.DepartureLocation,
                                            ArrivalLocation = flight.ArrivalLocation,
                                            Departure = flight.Departure,
                                            AvioCompanyName = companyProfile.Name
                                        });
                                    }
                                }
                            }
                        }
                    }

                    return Ok(new { flightsHistory });
                }

                return BadRequest("No reservations found!");
            }

            return BadRequest("User not found.");
        }

        [HttpGet]
        [Route("reservations/cars")]
        public async Task<IActionResult> GetCarsTerminableReservations()
        {
            var user = await _userService.GetCurrentUser();

            if (user != null)
            {
                var reservations = user.ReservedCars;

                if (reservations != null)
                {
                    List<CarHistoryDTO> carsHistory = new List<CarHistoryDTO>();

                    foreach (CarReservation cr in reservations)
                    {
                        if (DateTime.Now <= (cr.PickUpDate.AddDays(-2)))
                        {
                            var vehicle = await _vehicleService.GetVehicleById(cr.VehicleId);

                            if (vehicle != null)
                            {
                                var company = await _rentACarService.GetCompany(vehicle.CarCompanyId);

                                if (company != null)
                                {
                                    var companyProfile = await _rentACarService.GetCompanyProfile(company.CarCompanyProfileId);

                                    if (companyProfile != null)
                                    {
                                        carsHistory.Add(new CarHistoryDTO()
                                        {
                                            ReservationId = cr.CarReservationId,
                                            CarCompanyName = companyProfile.Name,
                                            VehicleName = vehicle.Name,
                                            PickUpDate = cr.PickUpDate,
                                            ReturnDate = cr.ReturnDate,
                                            PickUpLocation = cr.PickUpLocation.Name,
                                            ReturnLocation = cr.ReturnLocation.Name
                                        });
                                    }
                                }
                            }
                        }
                    }

                    return Ok(new { carsHistory });
                }

                return BadRequest("No car reservations found!");
            }

            return BadRequest("User not found.");
        }

        // GET api/user/current
        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetCurrentUser();
                var userRole = await _userService.GetCurrentUserRoles();

                if (user != null && userRole != null)
                {
                    return Ok(new { user, userRole });
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

        [HttpPost]
        [Route("rate/flight/{id}")]
        public async Task<IActionResult> PostAvioRate(long id, [FromBody]AvioFlightRatingDTO model)
        {
            if (ModelState.IsValid)
            {
                FlightReservation flightReser = await _reservationService.GetFlightReservationById(id);
                Flight flight = await _flightService.GetFlight(flightReser.FlightId);
                RegularUser user = await _userService.GetCurrentUser();

                AvioCompanyRating acRate = new AvioCompanyRating();
                acRate.AvioCompanyId = flight.AvioCompanyId;
                acRate.FlightReservationId = id;
                acRate.UserId = user.Id;
                acRate.Rate = model.ratingAvioCompany;

                FlightRating flightRate = new FlightRating();
                flightRate.FlightId = flight.FlightId;
                flightRate.FlightReservationId = id;
                flightRate.Rate = model.ratingFlight;
                flightRate.UserId = user.Id;

                await _userService.AddAvioRating(acRate);

                await _userService.AddFlightRating(flightRate);

                return Ok(200);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("rate/vehicle/{id}")]
        public async Task<IActionResult> PostCarRate(long id, [FromBody]CarVehicleRatingDTO model)
        {
            if (ModelState.IsValid)
            {
                CarReservation vehicleReser = await _reservationService.GetCarReservationById(id);
                Vehicle vehicle = await _vehicleService.GetVehicleById(vehicleReser.VehicleId);
                RegularUser user = await _userService.GetCurrentUser();

                CarCompanyRating ccRate = new CarCompanyRating();
                ccRate.CarReservationId = id;
                ccRate.CarCompanyId = vehicle.CarCompanyId;
                ccRate.UserId = user.Id;
                ccRate.Rate = model.ratingCarCompany;

                VehicleRating vehicleRate = new VehicleRating();
                vehicleRate.CarReservationId = id;
                vehicleRate.VehicleId = vehicle.VehicleId;
                vehicleRate.Rate = model.ratingVehicle;
                vehicleRate.UserId = user.Id;

                await _userService.AddCarRating(ccRate);

                await _userService.AddVehicleRating(vehicleRate);

                return Ok(200);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("history/flights/rating")]
        public async Task<IActionResult> GetAvioRating()
        {
            var user = await _userService.GetCurrentUser();

            if (user != null)
            {
                var reservations = user.ReservedFlights;

                if (reservations != null)
                {
                    List<FlightHistoryDTO> flightsHistory = new List<FlightHistoryDTO>();

                    foreach (FlightReservation fr in reservations)
                    {
                        var flight = await _flightService.GetFlight(fr.FlightId);

                        if (flight != null)
                        {
                            if (DateTime.Now > (flight.Departure.AddHours(-3)))
                            {
                                var company = await _avioService.GetCompany(flight.AvioCompanyId);

                                if (company != null)
                                {
                                    var companyProfile = await _avioService.GetCompanyProfile(company.AvioCompanyProfileId);

                                    if (companyProfile != null)
                                    {
                                        List<FlightRating> flightRa = await _flightService.FlightRating();

                                        bool boolFind = false;

                                        foreach (var flR in flightRa)
                                        {
                                            if (flR.FlightReservationId == fr.FlightReservationId && user.Id == flR.UserId)
                                            {
                                                boolFind = true;
                                            }
                                        }

                                        if (boolFind == false)
                                        {
                                            flightsHistory.Add(new FlightHistoryDTO()
                                            {
                                                ReservationId = fr.FlightReservationId,
                                                DepartureLocation = flight.DepartureLocation,
                                                ArrivalLocation = flight.ArrivalLocation,
                                                Departure = flight.Departure,
                                                AvioCompanyName = companyProfile.Name
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return Ok(new { flightsHistory });
                }

                return BadRequest("No reservations found!");
            }

            return BadRequest("User not found.");
        }

        [HttpGet]
        [Route("history/vehicles/rating")]
        public async Task<IActionResult> GetCarRating()
        {
            var user = await _userService.GetCurrentUser();

            if (user != null)
            {
                var reservations = user.ReservedCars;

                if (reservations != null)
                {
                    List<CarHistoryDTO> carsHistory = new List<CarHistoryDTO>();

                    foreach (CarReservation cr in reservations)
                    {
                        if (DateTime.Now > (cr.PickUpDate.AddDays(-2)))
                        {
                            var vehicle = await _vehicleService.GetVehicleById(cr.VehicleId);

                            if (vehicle != null)
                            {
                                var company = await _rentACarService.GetCompany(vehicle.CarCompanyId);

                                if (company != null)
                                {
                                    var companyProfile = await _rentACarService.GetCompanyProfile(company.CarCompanyProfileId);

                                    if (companyProfile != null)
                                    {
                                        List<VehicleRating> vehicleR = await _vehicleService.VehicleRating();

                                        bool boolFind = false;

                                        foreach (var veR in vehicleR)
                                        {
                                            if (veR.CarReservationId == cr.CarReservationId && user.Id == veR.UserId)
                                            {
                                                boolFind = true;
                                            }
                                        }

                                        if (boolFind == false)
                                        {
                                            carsHistory.Add(new CarHistoryDTO()
                                            {
                                                ReservationId = cr.CarReservationId,
                                                CarCompanyName = companyProfile.Name,
                                                VehicleName = vehicle.Name,
                                                PickUpDate = cr.PickUpDate,
                                                ReturnDate = cr.ReturnDate,
                                                PickUpLocation = cr.PickUpLocation.Name,
                                                ReturnLocation = cr.ReturnLocation.Name
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return Ok(new { carsHistory });
                }

                return BadRequest("No car reservations found!");
            }

            return BadRequest("User not found.");
        }
    }
}