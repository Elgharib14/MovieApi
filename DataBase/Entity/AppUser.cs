using Microsoft.AspNetCore.Identity;

namespace AngularApi.DataBase.Entity
{
    public class AppUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
