using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HisakaWeb.Models;

namespace HisakaWeb.Controllers
{
    public class BrandsController : Controller
    {
        private HisakaDBEntities db = new HisakaDBEntities();

        // GET: Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: Brands/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "brandName,brandLogo")] Brand brand)
        {
            if (brand.brandLogo == null)
            {
                ModelState.AddModelError("brandLogo", "Please select a brand logo.");
                return View(brand);
            }
            if (ModelState.IsValid)
            {
                var urlTuongDoi = "/Content/img/brands_logo/";

                brand.brandLogo = urlTuongDoi + brand.brandLogo;

                // Add the brand to the database and save changes
                db.Brands.Add(brand);
                db.SaveChanges();

                // Redirect to the Index action
                return RedirectToAction("Index");
            }

            // Return the View with the brand object if validation fails
            return View(brand);
        }


        // GET: Brands/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "brandName,brandLogo")] Brand brand)
        {
            if (brand.brandLogo == null)
            {
                ModelState.AddModelError("brandLogo", "Please select a brand logo.");
                return View(brand);
            }
            if (ModelState.IsValid)
            {
                var urlTuongDoi = "/Content/img/brands_logo/";

                brand.brandLogo = urlTuongDoi + brand.brandLogo;

                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
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
