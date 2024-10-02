using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IUserRepository
    {
        Task<List<IdentityUser>> GetAllUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(string id);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(IdentityUser user, string password);
        Task<bool> AddUserToRoleAsync(IdentityUser user, string role);
        Task<List<string>> GetUserRolesAsync(IdentityUser user);
        Task<bool> DeleteUserAsync(IdentityUser user);
        Task<IdentityUser> GetUserByNameAsync(string userName);
    }
}