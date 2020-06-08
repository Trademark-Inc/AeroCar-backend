using AeroCar.Models;
using AeroCar.Models.Admin;
using AeroCar.Models.DTO;
using AeroCar.Models.Rating;
using AeroCar.Models.Users;
using AeroCar.Repositories;
using AeroCar.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class AvioAdminService
    {
        private readonly AvioAdminRepository _avioAdminRepository;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AvioAdminService(AvioAdminRepository avioAdminRepository, UserService userService,
            IHttpContextAccessor contextAccessor)
        {
            _avioAdminRepository = avioAdminRepository;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<AvioAdmin>> GetAdmins()
        {
            return await _avioAdminRepository.GetAdmins();
        }

        public async Task<bool> AdminExists(string username)
        {
            var admin = await _avioAdminRepository.GetAdmin(username);
            return admin != null;
        }

        public string GetCurrentUserId()
        {
            return _userService.GetCurrentUserId();
        }

        public async Task<AvioAdmin> GetCurrentUser()
        {
            var userId = await _userService.GetCurrentUser();

            if (userId != null)
                return await _avioAdminRepository.GetAdmin(userId.UserName);
            else
                return null;
        }

        public async Task<RegularUser> GetAdminUser(string id)
        {
            return await _avioAdminRepository.GetAdminUser(id);
        }

        public async Task<AvioAdmin> GetAdminById(string id)
        {
            return await _avioAdminRepository.GetAdminById(id);
        }

        public async Task<AvioAdmin> GetAdminByEmailAndPassword(string email, string password)
        {
            var user = await _userService.GetUserByEmailAndPassword(email, password);
            return await _avioAdminRepository.GetAdminById(user.Id);
        }

        public async Task<AvioAdmin> GetAdminByUsernameAndPassword(string username, string password)
        {
            var user = await _userService.GetUserByUsernameAndPassword(username, password);
            return await _avioAdminRepository.GetAdminById(user.Id);
        }

        public async Task RegisterAdmin(string userId, AvioAdmin admin)
        {
            if (userId != null)
            {
                admin.UserId = userId;

                await _avioAdminRepository.CreateAdmin(admin);
            }
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginUser(UserLogin model)
        {
            return await _userService.LoginUser(model);
        }

        public async Task LogoutCurrentUser()
        {
            await _userService.LogoutCurrentUser();
        }

        public async Task UpdateAdmin(AvioAdmin admin)
        {
            await _avioAdminRepository.UpdateAdmin(admin);
        }

        public async Task<bool> RemoveAdmin(string username)
        {
            var result = await _avioAdminRepository.RemoveAdmin(username);
            return result;
        }

        public async Task<bool> RemoveAdminById(string id)
        {
            var result = await _avioAdminRepository.RemoveAdminById(id);
            return result;
        }
    }
}
