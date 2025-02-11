using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain
{
    public class AplicationUser : IdentityUser
    {
        public string? Nombre { get; set; }
    }
}