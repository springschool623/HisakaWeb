using HisakaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HisakaWeb.Controllers
{
    public class CartController : Controller
    {
        private HisakaDBEntities db = new HisakaDBEntities();

        // GET: Cart
        public ActionResult Index()
        {
            // Retrieve cart items from session
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();
            return View(cartItems);
        }

        public ActionResult AddToCart(string productId)
        {
            // Check if the user is logged in (Session["Username"] is not null)
            if (Session["Username"] != null)
            {
                Product product = db.Products.Find(productId);

                if (product == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Retrieve cart items from session
                    List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

                    // Check if the product is already in the cart
                    Cart existingItem = cartItems.FirstOrDefault(item => item.productId == productId);

                    if (existingItem != null)
                    {
                        // If the product is already in the cart, increment the quantity
                        existingItem.productQuantity += 1;
                    }
                    else
                    {
                        // If the product is not in the cart, add a new cart item
                        Cart cartItem = new Cart(product.productID, product.productName, (double)product.productPrice, 1, product.productImage);
                        cartItems.Add(cartItem);
                    }

                    // Save the updated cart items to session
                    Session["CartItems"] = cartItems;

                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Redirect to a login page or show a message indicating that the user needs to log in.
                return RedirectToAction("Login", "Home"); // Change the action and controller names accordingly.
            }
        }


        public ActionResult RemoveFromCart(string productId)
        {
            // Retrieve cart items from session
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

            // Find the item to be removed
            Cart itemToRemove = cartItems.FirstOrDefault(item => item.productId == productId);

            if (itemToRemove != null)
            {
                // If the quantity is greater than 1, decrement the quantity
                if (itemToRemove.productQuantity > 1)
                {
                    itemToRemove.productQuantity -= 1;
                }
                else
                {
                    // If the quantity is 1 or less, remove the item from the cart
                    cartItems.Remove(itemToRemove);
                }

                // Save the updated cart items to session
                Session["CartItems"] = cartItems;
            }

            // Redirect back to Index
            return RedirectToAction("Index", cartItems);
        }

        public ActionResult TotalCart()
        {
            // Retrieve cart items from session
            List<Cart> cartItems = Session["CartItems"] as List<Cart> ?? new List<Cart>();

            if(cartItems == null)
            {
                ViewBag.Total = 0;
                return View("Index");
            }

            // Calculate the total price
            double total = cartItems.Sum(item => item.productPrice * item.productQuantity);

            // Pass the total to the view
            ViewBag.Total = total;

            // Return a view to display the total
            return View("Index", cartItems);
        }
    }
}
