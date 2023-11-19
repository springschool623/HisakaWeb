using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HisakaWeb.Models;

namespace HisakaWeb.Controllers
{
    public class ProductsController : Controller
    {
        private HisakaDBEntities db = new HisakaDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public static string GenerateRandomProductID()
        {
            // Tạo một đối tượng Random
            Random random = new Random();

            // Tạo một mảng chứa các ký tự chữ cái và số
            char[] chars = new char[] {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
                'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
                'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9'
            };


            // Tạo một chuỗi rỗng
            string code = "";

            // Lặp lại cho đến khi chuỗi có độ dài mong muốn
            for (int i = 0; i < 10; i++)
            {
                // Chọn ngẫu nhiên một ký tự từ mảng
                int index = random.Next(chars.Length);
                char c = chars[index];

                // Thêm ký tự vào chuỗi
                code += c;
            }

            return code;
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.productBrand = new SelectList(db.Brands, "brandName", "brandName");
            ViewBag.productCategory = new SelectList(db.Categories, "categoryName", "categoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,productName,productPrice,productQuantity,productCategory,productBrand,createdAt,productImage")] Product product)
        {
            string newProductID;

            do
            {
                newProductID = GenerateRandomProductID();
            }
            while (db.Products.Any(p => p.productID == newProductID));

            product.productID = newProductID;

            product.createdAt = DateTime.Now;

            if (product.productImage == null)
            {
                ModelState.AddModelError("productImage", "Please select an image.");

                ViewBag.productBrand = new SelectList(db.Brands, "brandName", "brandName", product.productBrand);
                ViewBag.productCategory = new SelectList(db.Categories, "categoryName", "categoryName", product.productCategory);
                return View(product);
            }

            if (ModelState.IsValid)
            {
                var urlTuongDoi = "/Content/img/products_image/";

                product.productImage = urlTuongDoi + product.productImage;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productBrand = new SelectList(db.Brands, "brandName", "brandName", product.productBrand);
            ViewBag.productCategory = new SelectList(db.Categories, "categoryName", "categoryName", product.productCategory);
            return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productBrand = new SelectList(db.Brands, "brandName", "brandName", product.productBrand);
            ViewBag.productCategory = new SelectList(db.Categories, "categoryName", "categoryName", product.productCategory);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,productName,productPrice,productQuantity,productCategory,productBrand,createdAt,productImage")] Product product)
        {
            if (product.productImage == null)
            {
                ModelState.AddModelError("productImage", "Please select an image.");
                return View(product);
            }
            if (ModelState.IsValid)
            {
                var urlTuongDoi = "/Content/img/products_image/";

                product.productImage = urlTuongDoi + product.productImage;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productBrand = new SelectList(db.Brands, "brandName", "brandName", product.productBrand);
            ViewBag.productCategory = new SelectList(db.Categories, "categoryName", "categoryName", product.productCategory);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
