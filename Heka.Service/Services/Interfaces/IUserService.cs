using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Service.Services.DTOs;
using Heka.Service.Services.DTOs.AuthenticationDTOs;
using Heka.Service.Services.DTOs.Users;

namespace Heka.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByUserId(int userId);
        Task<UserDto> RegisterUser(RegistrationDto registration);
        Task<UserDto> GetUserByUsernameAndPassword(string username,string password);
        Task<UserDto> LoginUser(UserCredentialDto userCredential);
    }
}
