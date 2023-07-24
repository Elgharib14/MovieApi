﻿using System.ComponentModel.DataAnnotations;

namespace AngularApi.Modell
{
    public class MovieVM
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public IFormFile? Poster { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int  generaId { get; set; }
    }
}
