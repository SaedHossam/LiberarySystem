using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiberarySystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int AvalibleQuantity { get; set; }
        public bool IsVisible { get; set; }
        public virtual ICollection<BorrowInvoice> BorrowInvoices { get; set; }
        public virtual ICollection<ReturnInvoice> ReturnInvoices { get; set; }
    }
}