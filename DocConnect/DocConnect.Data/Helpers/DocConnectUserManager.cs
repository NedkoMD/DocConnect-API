using DocConnect.Data.Abstraction.Helpers;
using DocConnect.Data.Models.Entities;
using DocConnect.Data.Models.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Helpers
{
    public class DocConnectUserManager : IDocConnectUserManager
    {
        private readonly UserManager<User> _userManager;

        public DocConnectUserManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<string>> GetRolesAsync(User user)
        {
            var roles = await _userManager
                .GetRolesAsync(user);

            return roles;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<User> FindUserByIdAsync(uint userId)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, RoleConstants.UserRole);
        }

        public async Task CreateAdminAsync(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, RoleConstants.AdminRole);
        }

        public async Task<string> GenerateConfirmEmailTokenAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            var identityResult = await _userManager.ConfirmEmailAsync(user, token);

            return identityResult;
        }

        public async Task<string> GenerateResetPasswordTokenAsync(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword, string confirmPassword)
        {
            var identityResult = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return identityResult;
        }

        public async Task<bool> IsValidTokenAsync(User user, string tokenProvider, string purpose, string token)
        {
            var isValidToken = await _userManager.VerifyUserTokenAsync(user, "Default", "ResetPassword", token);

            return isValidToken;
        }
    }
}
