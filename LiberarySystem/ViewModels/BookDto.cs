using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiberarySystem.ViewModels
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string Author { get; set; }
        [Display(Name = "Avalible Quantity")]
        public int AvalibleQuantity { get; set; }
    }

    public class BookCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

    public class BookEditDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public string Author { get; set; }
        [Display(Name = "Avalible Quantity")]
        public int AvalibleQuantity { get; set; }
    }
}