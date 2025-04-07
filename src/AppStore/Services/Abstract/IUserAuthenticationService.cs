using AppStore.Models.DTO;

namespace AppStore.Services.Abstract;

public interface IUserAuthenticationService
{
    Task<Status> LoginAsync(LoginModel login);
    Task LogoutAsync();
}