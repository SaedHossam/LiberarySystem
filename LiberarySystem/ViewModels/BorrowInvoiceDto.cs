using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LiberarySystem.ViewModels
{
    public class BorrowInvoiceListDto
    {
        public int Id { get; set; }
        [Display(Name ="Book Name")]
        public string BookName { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }
    }

    public class BorrowInvoiceCreateDto
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
    }

    public class BorrowInvoiceEditDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }

    }
}