using AuthenticationApp.DB;
using AuthenticationApp.DTOs;
using AuthenticationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationAPIContext context;

        public UserService(AuthenticationAPIContext _context)
        {
            context = _context;
        }

        public async Task<List<User>> AddUser(User user)
        {
            context.User.Add(user);
            await context.SaveChangesAsync();
            return await context.User.ToListAsync();
        }

        public async Task<List<User>?> DeleteUser(int id)
        {
            var user = await context.User.FindAsync(id);
            if (user is null)
                return null;
            context.User.Remove(user);
            await context.SaveChangesAsync();
            return await context.User.ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await context.User.ToListAsync();
            return users;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var user = await context.User.FindAsync(id);
            if (user is null)
                return null;
            return user;
        }

        public async Task<User?> UpdateUser(int id, User request)
        {
            var user = await context.User.FindAsync(id);
            if (user is null)
                return null;

            user.PhoneNumber = request.PhoneNumber;
             await context.SaveChangesAsync();
            return user;
        }
    }
}
