using System.ComponentModel.DataAnnotations;

namespace AngularApi.Modell
{
    public class GeneraVM
    {
        [Required]
        public string? GName { get; set; }
    }
}
