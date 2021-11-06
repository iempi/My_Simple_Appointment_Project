using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Domain.Models.Users;

namespace Heka.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> IsUsernameExists(string username);
        Task<bool> AddUser(User user);
        Task<User> GetUserByUserId(int userId);
        Task<User> GetUserByUsernameAndPassword(string username, string password);
        Task SaveChanges();
    }
}
