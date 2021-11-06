using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Heka.DataAccess.Repositories.Interfaces;
using Heka.Domain.Models.Users;
using Heka.Service.Services.DTOs;
using Heka.Service.Services.DTOs.AuthenticationDTOs;
using Heka.Service.Services.DTOs.Users;
using Heka.Service.Services.Interfaces;

namespace Heka.Service.Services.Implementation
{
    public class UserService : IUserService
    {
        #region Dependancy Injection

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userService)
        {
            _userRepository = userService;
        }

        #endregion

        public  async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return users.Select(u => new UserDto()
            {
                IsDelete = u.IsDelete,
                Password = u.Password,
                RegistrationDate = u.RegistrationDate,
                RoleTitle = u.Role.RoleTitle,
                RoleId = u.Role.RoleId,
                UserId = u.UserId,
                Username = u.Username
            }).ToList();
        }

        private async Task SaveChanges()
        {
            try
            {
                await _userRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<UserDto> RegisterUser(RegistrationDto registration)
        {
            //Checks if Username is already Exists in database or not (avoiding Duplicate Username)
            if (await IsUsernameExists(registration.Username))
                return null;

            User newUser = new User()
            {
                IsDelete = false,
                Password = registration.Password,
                RegistrationDate = DateTime.Now,
                RoleId = 2,
                Username = registration.Username.Trim().Replace(" ","").ToLower() //If user uses Space, or type with Capital words, this is gonna correct them
            };

            var isRegistered = await _userRepository.AddUser(newUser);
            await SaveChanges();

            return new UserDto()
            {
                Username = newUser.Username,
                RoleId = newUser.RoleId,
                IsDelete = newUser.IsDelete,
                Password = newUser.Password,
                RegistrationDate = newUser.RegistrationDate
            };

        }
        private async Task<bool> IsUsernameExists(string username)
        {
            return await _userRepository.IsUsernameExists(username);
        }

        public async Task<UserDto> LoginUser(UserCredentialDto userCredential)
        {
            var user = await _userRepository.GetUserByUsernameAndPassword(userCredential.UserName,
                userCredential.Password);

            if (user is null)
                return null;

            return new UserDto
            {
                UserId = user.UserId,
                Password = user.Password,
                Username = user.Username,
                RoleTitle = user.Role.RoleTitle,
                RoleId = user.Role.RoleId,
                IsDelete = user.IsDelete
            };
        }

        public async Task<UserDto> GetUserByUserId(int userId)
        {
            var user = await _userRepository.GetUserByUserId(userId);

            return new UserDto()
            {
                IsDelete = user.IsDelete,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate,
                RoleTitle = user.Role.RoleTitle,
                RoleId = user.Role.RoleId,
                UserId = user.UserId,
                Username = user.Username
            };
        }

        public async Task<UserDto> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAndPassword(username,password);

            return new UserDto()
            {
                IsDelete = user.IsDelete,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate,
                RoleTitle = user.Role.RoleTitle,
                RoleId = user.Role.RoleId,
                UserId = user.UserId,
                Username = user.Username
            };
        }

    }
}
