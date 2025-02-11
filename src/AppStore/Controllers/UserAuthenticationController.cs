using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace src.AppStore.Controllers;

public class UserAuthenticationController : Controller
{
    private readonly IUserAuthenticationService _authService;
    public UserAuthenticationController(IUserAuthenticationService authService)
    {
        _authService = authService;
    }

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginModel login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        var result = await _authService.LoginAsync(login);
        if (result.Success)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["Message"] = result.Message;
            return RedirectToAction("Login", "UserAuthentication");
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }
    
}