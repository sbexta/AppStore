using AppStore.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Repositories.Abstract
{
    public interface IUserAuthenticationRepository
    {
        Task<Status> LoginAsync(LoginModel login);
        Task LogoutAsync();
    }
}
