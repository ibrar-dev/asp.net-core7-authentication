
using AuthenticationApp.DTOs;
using AuthenticationApp.Models;

namespace AuthenticationApp.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetSingleUser(int id);
        Task<List<User>> AddUser(User user);
        Task<User?> UpdateUser(int id, User request);
        Task<List<User>?> DeleteUser(int id);
    }
}
