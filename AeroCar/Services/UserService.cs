using AeroCar.Models;
using AeroCar.Models.DTO;
using AeroCar.Models.Rating;
using AeroCar.Models.Registration;
using AeroCar.Models.Users;
using AeroCar.Repositories;
using AeroCar.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserManager<RegularUser> _userManager;
        private readonly SignInManager<RegularUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserRepository userRepository, UserManager<RegularUser> userManager, SignInManager<RegularUser> signInManager,
            IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
        }

        public bool IsCurrentUserAdmin()
        {
            return _contextAccessor.HttpContext.User.IsInRole("SystemAdmin");
        }

        public string GetCurrentUserId()
        {
            return _userManager.GetUserId(_contextAccessor.HttpContext.User);
        }

        public async Task<RegularUser> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            return await _userRepository.GetUserById(userId);
        }

        public async Task<List<string>> GetCurrentUserRoles()
        {
            var user = await GetCurrentUser();
            return (List<string>)await _userManager.GetRolesAsync(user);
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            var user = await GetUserById(userId);
            return (List<string>)await _userManager.GetRolesAsync(user);
        }

        public async Task<RegularUser> GetUserById(string id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<RegularUser> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task<RegularUser> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }

        public async Task<RegularUser> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }

        public async Task<IdentityResult> RegisterUser(RegularUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return result;
            }

            result = await _userManager.AddToRoleAsync(user, "RegularUser");

            if (!result.Succeeded)
            {
                return result;
            }

            EmailUtility.SendEmail(user.Email, "Profile Status", "Your profile is being verified." +
                "\nPlease go to the following link to verify your account: http://localhost:62541/api/user/validate?username=" + user.UserName + "&validate=true&email=" + user.Email);
            user.Status = UserStatus.InProcess;

            return result;
        }

        public async Task<IdentityResult> ValidateUser(string username, bool validate, string email)
        {
            RegularUser applicationUser = await _userManager.FindByNameAsync(username);
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

            IdentityResult result = await _userManager.UpdateAsync(applicationUser);
            return result;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginUser(UserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && user.Status == UserStatus.Activated)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                // get role
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("RegularUser")) model.RedirectUrl = "";
                else if (roles.Contains("SystemAdmin")) model.RedirectUrl = "/admin/system";
                else if (roles.Contains("AvioAdmin"))
                {
                    model.RedirectUrl = "/admin/avio";
                }
                else if (roles.Contains("CarAdmin"))
                {
                    model.RedirectUrl = "/admin/car";
                }

                return result;
            }

            return null;
        }

        public async Task LogoutCurrentUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task UpdateUser(RegularUser user)
        {
            await _userRepository.UpdateUser(user);
        }

        public async Task<List<Friend>> GetUserFriends(string id)
        {
            return await _userRepository.GetUserFriends(id);
        }

        public async Task AddFriend(RegularUser user, string friendUsername)
        {
            var friend = await GetUserByUsername(friendUsername);

            if (friend != null)
            {
                var friends = await GetUserFriends(user.Id);

                if (friends != null)
                {
                    var exists = friends.SingleOrDefault(f => f.FriendId == friend.Id) != null;
                    if (exists) return;

                    await _userRepository.AddFriend(new Friend() { FriendId = friend.Id, UserId = user.Id });
                }
            }
        }

        public async Task RemoveFriend(RegularUser user, string friendUsername)
        {
            var friend = await GetUserByUsername(friendUsername);

            await _userRepository.RemoveFriend(await _userRepository.GetUserFriend(user.Id, friend.Id));
        }

        public async Task<List<Invitation>> GetUserInvitations(string userId)
        {
            return await _userRepository.GetUserInvitations(userId);
        }

        public async Task<bool> InviteToFlight(long flightId, string friendUsername)
        {
            var user = await GetCurrentUser();
            var friends = await GetUserFriends(user.Id);

            var friend = await GetUserByUsername(friendUsername);
            if (friend != null)
            {
                var exists = friends.SingleOrDefault(f => f.FriendId == friend.Id) != null;

                if (exists)
                {
                    var invitation = new Invitation()
                    {
                        FlightId = flightId,
                        FromUserId = user.Id,
                        ToUserId = friend.Id,
                        SentDate = DateTime.Now,
                        Accepted = false
                    };

                    await _userRepository.InviteToFlight(invitation);
                    return true;
                }
            }

            return false;
        }

        public async Task AcceptInvitation(long invitationId, bool accept)
        {
            var user = await GetCurrentUser();
            var invitation = await _userRepository.GetInvitation(invitationId);

            await _userRepository.RemoveInvitation(invitation);
        }

        public async Task AddAvioRating(AvioCompanyRating acr)
        {
            await _userRepository.AddAvioRating(acr);
        }

        public async Task AddFlightRating(FlightRating fr)
        {
            await _userRepository.AddFlightRating(fr);
        }

        public async Task AddCarRating(CarCompanyRating acr)
        {
            await _userRepository.AddCarRating(acr);
        }

        public async Task AddVehicleRating(VehicleRating vr)
        {
            await _userRepository.AddVehicleRating(vr);
        }
    }
}
