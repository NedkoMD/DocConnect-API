using DocConnect.Data.Abstraction.Helpers;
using DocConnect.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DocConnect.Data.Helpers
{
    public class DocConnectSignInManager : IDocConnectSignInManager
    {
        private readonly SignInManager<User> _signInManager;

        public DocConnectSignInManager(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistant, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistant, lockoutOnFailure);

            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
