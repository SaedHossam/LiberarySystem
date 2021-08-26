using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LiberarySystem.Models;
using LiberarySystem.ViewModels;

namespace LiberarySystem.Controllers
{
    public class BorrowInvoicesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: BorrowInvoices
        public ActionResult Index()
        {
            List<BorrowInvoice> borrowInvoices = db.BorrowInvoices.Include(b => b.Book).Include(b => b.Customer).ToList();
            List<BorrowInvoiceListDto> invoices = new List<BorrowInvoiceListDto>();
            foreach (var i in borrowInvoices)
            {
                invoices.Add(
                        new BorrowInvoiceListDto() { 
                            Id = i.Id, 
                            BookName = i.Book.Name, 
                            CustomerName = i.Customer.Name, 
                            BorrowDate = i.BorrowDate
                        }
                    );
            }

            return View(invoices);
        }

        // GET: BorrowInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowInvoice borrowInvoice = db.BorrowInvoices.Find(id);
            if (borrowInvoice == null)
            {
                return HttpNotFound();
            }
            var invoice = new BorrowInvoiceListDto()
            {
                Id = borrowInvoice.Id,
                BookName = borrowInvoice.Book.Name,
                CustomerName = borrowInvoice.Customer.Name,
                BorrowDate = borrowInvoice.BorrowDate
            };
            return View(invoice);
        }

        // GET: BorrowInvoices/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books.Where(b => b.IsVisible == true && b.AvalibleQuantity > 0), "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers.Where(c => c.IsVisible == true), "Id", "Name");
            return View();
        }

        // POST: BorrowInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BorrowInvoiceCreateDto borrowInvoice)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Where(b => b.IsVisible && b.Id == borrowInvoice.BookId).FirstOrDefault();
                // if quantity <= 0 or book doesn't exists
                if (book == null || book.AvalibleQuantity <= 0)
                    return HttpNotFound();
                
                book.AvalibleQuantity--;
                var invoice = new BorrowInvoice()
                {
                    BookId = borrowInvoice.BookId,
                    CustomerId = borrowInvoice.CustomerId,
                    BorrowDate = DateTime.Now,
                    IsReturned = false
                };
                db.Entry(book).State = EntityState.Modified;
                db.BorrowInvoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "Name", borrowInvoice.BookId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", borrowInvoice.CustomerId);
            return View(borrowInvoice);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
