using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using AppStore.Services.Abstract;

namespace AppStore.Services.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;

        public UserAuthenticationService(IUserAuthenticationRepository userAuthenticationRepository)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
        }

        public async Task<Status> LoginAsync(LoginModel login)
        {
            return await _userAuthenticationRepository.LoginAsync(login);
        }

        public async Task LogoutAsync()
        {
            await _userAuthenticationRepository.LogoutAsync();
        }
    }
}
