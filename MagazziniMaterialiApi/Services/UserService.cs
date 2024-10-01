using System.Collections.Generic;
using System.Threading.Tasks;
using MagazziniMaterialiAPI.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MagazziniMaterialiAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> RegisterUserAsync(string email, string password, string role)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var created = await _userRepository.CreateUserAsync(user, password);
            if (created)
            {
                await _userRepository.AddUserToRoleAsync(user, role);
            }

            return created;
        }
    }
}