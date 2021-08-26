using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberarySystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsVisible { get; set; }
        public virtual ICollection<BorrowInvoice> BorrowInvoices { get; set; }
        public virtual ICollection<ReturnInvoice> ReturnInvoices { get; set; }
    }
}