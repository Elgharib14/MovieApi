using System.ComponentModel.DataAnnotations;

namespace AngularApi.DataBase.Entity
{
    public class Genera
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? GName { get; set; }
    }
}
