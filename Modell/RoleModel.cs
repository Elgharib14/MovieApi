using System.ComponentModel.DataAnnotations;

namespace AngularApi.Modell
{
    public class RoleModel
    {
        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
