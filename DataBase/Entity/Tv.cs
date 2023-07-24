﻿using System.ComponentModel.DataAnnotations;

namespace AngularApi.DataBase.Entity
{
    public class Tv
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Poster { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public double Rate { get; set; }
    }
}
