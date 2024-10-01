

using Microsoft.AspNetCore.Identity;

namespace MagazziniMaterialiAPI.Services
{
    public interface IUserService
    {
        Task<List<IdentityUser>> GetAllUsersAsync();
        Task<IdentityUser> GetUserByIdAsync(string id);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<bool> RegisterUserAsync(string email, string password, string role);
    }
}
