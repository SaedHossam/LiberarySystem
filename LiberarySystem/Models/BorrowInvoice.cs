using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberarySystem.Models
{
    public class BorrowInvoice
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime BorrowDate { get; set; }
        public bool IsReturned { get; set; }
    }
}