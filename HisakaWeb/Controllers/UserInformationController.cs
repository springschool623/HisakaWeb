using HisakaWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HisakaWeb.Controllers
{
    public class UserInformationController : Controller
    {
        private HisakaDBEntities db = new HisakaDBEntities();

        public static string GenerateRandomCustomerID()
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

        public ActionResult Index()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "customerID,customerFirstName,customerLastName,customerEmail,customerPhoneNo")] Customer customer)
        {
            string username = Session["Username"] as string;

            var account = db.Accounts.FirstOrDefault(a => a.accountName == username);
            string newCustomerID;

            do
            {
                newCustomerID = GenerateRandomCustomerID();
            }
            while (db.Customers.Any(c => c.customerID == newCustomerID));

            customer.customerID = newCustomerID;

            account.customerID = newCustomerID;
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.Customers.Add(customer);
                db.SaveChanges();

                Session["AccountCustomerID"] = newCustomerID;
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Details/5
        public ActionResult ShowInfo(string id)
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
            return View(customer);
        }
    }
}