using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiberarySystem.ViewModels
{
    public class ReturnInvoiceListDto
    {
        public int Id { get; set; }
        [Display(Name="Book")]
        public string BookName { get; set; }
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        [Display(Name = "Return")]
        public DateTime ReturnDate { get; set; }
    }

    public class ReturnInvoiceCreateDto
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
    }
}