using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HisakaWeb.Models;

namespace HisakaWeb.Controllers
{
    public class HomeController : Controller
    {
        private HisakaDBEntities db = new HisakaDBEntities();

        public ActionResult Index()
        {
            // Lấy danh sách sản phẩm
            var products = db.Products.ToList();

            // Lấy danh sách danh mục
            var categories = db.Categories.ToList();

            // Lấy danh sách thương hiệu
            var brands = db.Brands.ToList();

            var customers = db.Customers.ToList();

            // Gán dữ liệu cho View
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Brands = brands;
            ViewBag.Customers = customers;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.customerID = new SelectList(db.Customers, "customerID", "customerFirstName");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "accountID,accountName,accountPassword,customerID,createdAt,account_status")] Account account)
        {
            if (ModelState.IsValid)
            {
                // Lấy giá trị lớn nhất của TestSchedule_ID trong database
                int maxTestId = db.Accounts.Max(ts => (int?)ts.accountID) ?? 0;

                // Tăng giá trị lên 1
                account.accountID = maxTestId + 1;

                account.createdAt = DateTime.Now;

                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.Customers, "customerID", "customerID", account.customerID);
            return View(account);
        }

        public ActionResult Login()
        {
            ViewBag.customerID = new SelectList(db.Customers, "customerID", "customerID");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfo loginInfo)
        {
            // Check if the loginInfo is for a Student
            var isAccount = db.Accounts.Any(u => u.accountName == loginInfo.Username && u.accountPassword == loginInfo.Password);

            if (isAccount)
            {
                // Set the "UserCode" session variable to the user code
                var account = db.Accounts.FirstOrDefault(a => a.accountName == loginInfo.Username);

                var customer = db.Customers.FirstOrDefault(c => c.customerID == account.customerID);

                Session["Username"] = loginInfo.Username;
                if(customer != null)
                {
                    Session["customerID"] = customer.customerID;
                }

                return RedirectToAction("Index");
            }
            else if (loginInfo.Username == "admin" && loginInfo.Password == "admin12345")
            {
                // Set the "UserCode" session variable to "admin" for admin
                Session["Username"] = "admin";

                return RedirectToAction("AdminHome");
            }
            else
            {
                // Invalid login, return to the login page with an error message
                ModelState.AddModelError("", "Invalid username or password");

                // Return the loginInfo object to repopulate the form with user-entered values
                return View( loginInfo);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            // Clear session
            Session.Clear();

            // Redirect to the login page
            return RedirectToAction("Index");
        }

        public ActionResult AdminHome()
        {            
            // Lấy danh sách sản phẩm
            var products = db.Products.ToList();

            // Lấy danh sách danh mục
            var categories = db.Categories.ToList();

            // Lấy danh sách thương hiệu
            var brands = db.Brands.ToList();

            var customers = db.Customers.ToList();

            var accounts = db.Accounts.ToList();

            // Gán dữ liệu cho View
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Brands = brands;
            ViewBag.Customers = customers;
            ViewBag.Accounts = accounts;

            return View();
        }
    }
}