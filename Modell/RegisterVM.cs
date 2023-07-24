
using System.ComponentModel.DataAnnotations;

namespace AngularApi.Modell
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }
    }
}
