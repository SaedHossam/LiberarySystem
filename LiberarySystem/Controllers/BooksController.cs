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
    public class BooksController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Books
        public ActionResult Index()
        {
            List<Book> books = db.Books.Where(b => b.IsVisible == true).ToList();
            List<BookListDto> bookList = new List<BookListDto>();
            foreach (var book in books)
            {
                bookList.Add(
                    new BookListDto()
                    {
                        Id = book.Id, 
                        Name = book.Name, 
                        Code = book.Code, 
                        Author = book.Author, 
                        AvalibleQuantity = book.AvalibleQuantity
                    }
                    );
            }
            return View(bookList);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null || book.IsVisible == false)
            {
                return HttpNotFound();
            }
            var bookDto = new BookListDto() { 
                Id = book.Id,
                Name = book.Name,
                Code = book.Code,
                Author = book.Author,
                AvalibleQuantity = book.AvalibleQuantity
            };
            return View(bookDto);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreateDto book)
        {
            if (ModelState.IsValid)
            {
                Book b = new Book()
                {
                    Name = book.Name,
                    Code = book.Code,
                    Author = book.Author,
                    Quantity = book.Quantity,
                    AvalibleQuantity = book.Quantity,
                    IsVisible = true
                };
                db.Books.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null || book.IsVisible == false)
            {
                return HttpNotFound();
            }
            var bookDto = new BookEditDto()
            {
                Id = book.Id,
                Name = book.Name,
                Code = book.Code,
                Author = book.Author,
                AvalibleQuantity = book.AvalibleQuantity
            };

            return View(bookDto);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookEditDto book)
        {
            if (ModelState.IsValid)
            {
                Book b = db.Books.Where(a => a.Id == book.Id).FirstOrDefault();
                if (b == null || b.IsVisible == false)
                {
                    return HttpNotFound();
                }

                b.Name = book.Name;
                b.Code = book.Code;
                b.Author = book.Author;
                b.Quantity = book.AvalibleQuantity;
                b.AvalibleQuantity = book.AvalibleQuantity;

                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null || book.IsVisible == false)
            {
                return HttpNotFound();
            }
            var bookDto = new BookEditDto()
            {
                Id = book.Id,
                Name = book.Name,
                Code = book.Code,
                Author = book.Author,
                AvalibleQuantity = book.AvalibleQuantity
            };
            return View(bookDto);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            book.IsVisible = false;
            //db.Books.Remove(book);
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
