using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Infrastructure;
using Shop.Models;
using Shop.Models.ViewModels;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext _db;
        public CartController(ShopContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart")?? new List<CartItem>();

            CartViewModel CartVM = new CartViewModel()
            {
                CartItems = cart,
                GrantTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(CartVM);
        }

        public async Task<IActionResult> Add(long id)
        {
            //find product
            Product product = await _db.Products.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            //specific id
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if(cartItem== null)
            {
                cart.Add(new CartItem(product));
                
            }
            else
            {
                cartItem.Quantity += 1;
            }

            //SetJson
            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The product has been added";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult>Remove(long id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            //get specific cart item
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            //check quantity
            if (cartItem.Quantity >1)
            {
                --cartItem.Quantity;

            }
            else //if its 1 remove
            {
                cart.RemoveAll(c=>c.ProductId ==id);
            }
             // check if there is anything in the cart
             if(cart.Count==0)// means cart is empty
            {
                HttpContext.Session.Remove("Cart");
            }
            else    //set that session cart again
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            //SetJson
            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The product has been removed";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Decrease(long id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);
           
            // check if there is anything in the cart
            if (cart.Count == 0)// means cart is empty
            {
                HttpContext.Session.Remove("Cart");
            }
            else    //set that session cart again
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

           

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }
}
