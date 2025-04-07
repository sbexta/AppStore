using AppStore.Models.DTO;
using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Repositories.Implementation
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly SignInManager<AplicationUser> _signInManager;

        public UserAuthenticationRepository(UserManager<AplicationUser> userManager,
                                            SignInManager<AplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel login)
        {
            var status = new Status();

            var user = await _userManager.FindByNameAsync(login.Username!);
            if (user == null)
            {
                status.Success = false;
                status.Message = "User not found";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                status.Success = false;
                status.Message = "Invalid password";
                return status;
            }

            var result = await _signInManager.PasswordSignInAsync(user, login.Password!, true, false);
            if (!result.Succeeded)
            {
                status.Success = false;
                status.Message = "Invalid login attempt";
                return status;
            }

            status.Success = true;
            status.Message = "Login successful";
            return status;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
