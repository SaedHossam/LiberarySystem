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
    public class ReturnInvoicesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: ReturnInvoices
        public ActionResult Index()
        {
            var returnInvoices = db.ReturnInvoices.Include(r => r.Book).Include(r => r.Customer).ToList();
            List<ReturnInvoiceListDto> invoices = new List<ReturnInvoiceListDto>();
            foreach (var returnInvoice in returnInvoices)
            {
                invoices.Add(
                        new ReturnInvoiceListDto() { 
                            Id = returnInvoice.Id,
                            BookName = returnInvoice.Book.Name,
                            CustomerName = returnInvoice.Customer.Name,
                            ReturnDate = returnInvoice.ReturnDate
                        }
                    );
            }
            return View(invoices);
        }

        // GET: ReturnInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnInvoice returnInvoice = db.ReturnInvoices.Find(id);
            if (returnInvoice == null)
            {
                return HttpNotFound();
            }
            var invoice = new ReturnInvoiceListDto()
            {
                Id = returnInvoice.Id,
                BookName = returnInvoice.Book.Name,
                CustomerName = returnInvoice.Customer.Name,
                ReturnDate = returnInvoice.ReturnDate
            };
            return View(invoice);
        }

        // GET: ReturnInvoices/Create
        public ActionResult Create()
        {
            // return all borrowed books
            ViewBag.BookId = new SelectList(db.Books.Where(b => b.Quantity != b.AvalibleQuantity), "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: ReturnInvoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReturnInvoiceCreateDto returnInvoice)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Where(b => b.Id == returnInvoice.BookId).FirstOrDefault();
                if (book == null)
                    return HttpNotFound();
                book.AvalibleQuantity++;
                db.Entry(book).State = EntityState.Modified;

                var invoice = db.BorrowInvoices.Where(i => !i.IsReturned && i.BookId == returnInvoice.BookId && i.CustomerId == returnInvoice.CustomerId).FirstOrDefault();
                if (invoice == null)
                    return HttpNotFound();
                invoice.IsReturned = true;
                db.Entry(invoice).State = EntityState.Modified;

                ReturnInvoice ret = new ReturnInvoice() { CustomerId = returnInvoice.CustomerId, BookId = returnInvoice.BookId, ReturnDate = DateTime.Now };
                db.ReturnInvoices.Add(ret);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books.Where(b => b.Quantity != b.AvalibleQuantity), "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", returnInvoice.CustomerId);
            return View(returnInvoice);
        }

        // Get All books that customer borrowed and didn't return.
        // GET: ReturnInvoices/GetCustomerBooks
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetCustomerBooks(int CustomerId)
        {
            var books = db.BorrowInvoices
                .Include(i => i.Book)
                .Where(i => i.CustomerId == CustomerId && i.IsReturned == false)
                .Select(a => new { bookId = a.BookId, bookName = a.Book.Name })
                .ToList();
            return Json(books, JsonRequestBehavior.AllowGet);
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
