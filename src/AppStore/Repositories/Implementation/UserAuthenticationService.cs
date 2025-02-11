using AppStore.Repositories.Abstract;
using AppStore.Models.DTO;
using Microsoft.AspNetCore.Identity;
using AppStore.Models.Domain;

namespace AppStore.Repositories.Implementation;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<AplicationUser> _userManager;
    private readonly SignInManager<AplicationUser> _signInManager;

    public UserAuthenticationService(UserManager<AplicationUser> userManager,
                                     SignInManager<AplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }



    public async Task<Status> LoginAsync(LoginModel login)
    {
        var Status = new Status();
        var user = await _userManager.FindByNameAsync(login.Username!);

        if (user == null)
        {
            Status.Success = false;
            Status.Message = "User not found";
            return Status;
        }
        
        if(!await _userManager.CheckPasswordAsync(user, login.Password!))
        {
            Status.Success = false;
            Status.Message = "Invalid password";
            return Status;
        }

        var result = await _signInManager.PasswordSignInAsync(user, login.Password!, true, false);
        if (!result.Succeeded)
        {
            Status.Success = false;
            Status.Message = "Invalid login attempt";
            return Status;
        }
        
        Status.Success = true;
        Status.Message = "Login successful";

        return Status;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}