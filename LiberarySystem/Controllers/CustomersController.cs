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
    public class CustomersController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = db.Customers.Where(c => c.IsVisible == true).ToList();
            List<CustomerListDto> customerList = new List<CustomerListDto>();
            foreach (var c in customers)
            {
                customerList.Add(
                        new CustomerListDto() { 
                            Id = c.Id,
                            Name = c.Name,
                            Address = c.Address,
                            Phone = c.Phone,
                            Code = c.Code
                        }
                    );
            }
            return View(customerList);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var c = new CustomerListDto()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                Code = customer.Code
            };
            return View(c);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreateDto customer)
        {
            if (ModelState.IsValid)
            {
                var cus = new Customer() {
                    Name = customer.Name,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Code = customer.Code,
                    IsVisible = true
                };
                db.Customers.Add(cus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null || customer.IsVisible == false)
            {
                return HttpNotFound();
            }

            var cus = new CustomerEditDto()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                Code = customer.Code,
            };

            return View(cus);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEditDto customer)
        {
            if (ModelState.IsValid)
            {
                var cus = db.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();

                cus.Id = customer.Id;
                cus.Name = customer.Name;
                cus.Address = customer.Address;
                cus.Phone = customer.Phone;
                cus.Code = customer.Code;
                
                db.Entry(cus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null || customer.IsVisible == false)
            {
                return HttpNotFound();
            }
            var cus = new CustomerEditDto()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                Code = customer.Code,
            };
            return View(cus);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            customer.IsVisible = false;
            db.Entry(customer).State = EntityState.Modified;
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
