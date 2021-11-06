using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.DataAccess.Context;
using Heka.DataAccess.Repositories.Interfaces;
using Heka.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Heka.DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {

        #region Dependancy Injection

        private readonly HekaDbContext _context;

        public UserRepository(HekaDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<User> GetUserByUserId(int userId)
        {
            return await _context.Users.Include(u=>u.Role).SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> IsUsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
        public async Task<bool> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            return true;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            return await _context.Users.Include(u => u.Role).Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
