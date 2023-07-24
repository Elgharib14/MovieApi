
using System.ComponentModel.DataAnnotations;

namespace AngularApi.Modell
{
    public class LoginVM
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
