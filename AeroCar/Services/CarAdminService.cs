using AeroCar.Models;
using AeroCar.Models.Admin;
using AeroCar.Models.DTO;
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
    public class CarAdminService
    {
        private readonly CarAdminRepository _carAdminRepository;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CarAdminService(CarAdminRepository carAdminRepository, UserService userService,
            IHttpContextAccessor contextAccessor)
        {
            _carAdminRepository = carAdminRepository;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<CarAdmin>> GetAdmins()
        {
            return await _carAdminRepository.GetAdmins();
        }

        public async Task<bool> AdminExists(string username)
        {
            var admin = await _carAdminRepository.GetAdmin(username);
            return admin != null;
        }

        public string GetCurrentUserId()
        {
            return _userService.GetCurrentUserId();
        }

        public async Task<CarAdmin> GetCurrentUser()
        {
            var userId = await _userService.GetCurrentUser();
            return await _carAdminRepository.GetAdminById(userId.Id);
        }

        public async Task<RegularUser> GetAdminUser(string id)
        {
            return await _carAdminRepository.GetAdminUser(id);
        }

        public async Task<CarAdmin> GetAdminById(string id)
        {
            return await _carAdminRepository.GetAdminById(id);
        }

        public async Task<CarAdmin> GetAdminByEmailAndPassword(string email, string password)
        {
            var user = await _userService.GetUserByEmailAndPassword(email, password);
            return await _carAdminRepository.GetAdminById(user.Id);
        }

        public async Task<CarAdmin> GetAdminByUsernameAndPassword(string username, string password)
        {
            var user = await _userService.GetUserByUsernameAndPassword(username, password);
            return await _carAdminRepository.GetAdminById(user.Id);
        }

        public async Task RegisterAdmin(string userId, CarAdmin admin)
        {
            if (userId != null)
            {
                admin.UserId = userId;

                await _carAdminRepository.CreateAdmin(admin);
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

        public async Task UpdateAdmin(CarAdmin admin)
        {
            await _carAdminRepository.UpdateAdmin(admin);
        }

        public async Task<bool> RemoveAdmin(string username)
        {
            var result = await _carAdminRepository.RemoveAdmin(username);
            return result;
        }

        public async Task<bool> RemoveAdminById(string id)
        {
            var result = await _carAdminRepository.RemoveAdminById(id);
            return result;
        }
    }
}
