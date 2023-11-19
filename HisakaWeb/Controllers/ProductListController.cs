using HisakaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HisakaWeb.Controllers
{
    public class ProductListController : Controller
    {
        private HisakaDBEntities db = new HisakaDBEntities();

        // GET: ProductList
        public ActionResult Index(double? max, double? min)
        {           
            // Xử lý logic sắp xếp sản phẩm theo giá ở đây
            // Sử dụng các tham số min và max để áp dụng bộ lọc theo giá

            // Ví dụ: sắp xếp theo giá tăng dần
            var sortedProducts = db.Products.ToList();

            ViewBag.sideProducts = db.Products.ToList();

            // Nếu max có giá trị, áp dụng bộ lọc giá trị tối đa
            if (max.HasValue)
            {
                sortedProducts = sortedProducts.Where(p => p.productPrice <= max.Value).ToList();
            }

            if (min.HasValue)
            {
                // Áp dụng bộ lọc giá trị tối thiểu
                sortedProducts = sortedProducts.Where(p => p.productPrice >= min).ToList();
            }
            // Trả về view hoặc làm cái gì đó với sortedProducts ở đây
            return View(sortedProducts);
        }

    }
}