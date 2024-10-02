using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazziniMaterialiAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CreateUserAsync(IdentityUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> AddUserToRoleAsync(IdentityUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
        public async Task<List<string>> GetUserRolesAsync(IdentityUser user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }
        public async Task<IdentityUser> GetUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<bool> DeleteUserAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    
                    return result.Succeeded;
                }
                else
                {
                   
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"Error deleting user: {error.Description}");
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
             
                _logger.LogError(ex, $"Exception occurred while deleting user {user.UserName}");
                return false;
            }
        }
    }
}