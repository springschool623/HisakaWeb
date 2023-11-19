using HisakaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HisakaWeb.Controllers
{
    public class ProductDetailController : Controller
    {
        private HisakaDBEntities db = new HisakaDBEntities();

        // GET: Products/Details/5
        public ActionResult Index(string id)
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
            // Lấy danh sách sản phẩm
            var relatedProduct = db.Products.Where(p => p.productBrand == product.productBrand || p.productCategory == product.productCategory).ToList();

            ViewBag.relatedProduct = relatedProduct;

            var productCates = db.Products.Where(p => p.productCategory == product.productCategory).ToList();

            ViewBag.sameCate = productCates;

            return View(product);
        }
    }
}