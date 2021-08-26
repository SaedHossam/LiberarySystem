using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LiberarySystem.Models
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("LiberaryDb")
        { }
            
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BorrowInvoice> BorrowInvoices { get; set; }
        public DbSet<ReturnInvoice> ReturnInvoices { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //modelBuilder.Entity<BorrowInvoice>().Property(a => a.BorrowDate)
        //}
    }
}
    
