using System;
using System.Linq;
using BlingRus.Domain;
using BlingRus.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlingRus.Web.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly IShoppingContext _shoppingContext;
        private readonly CheckoutService _checkoutService;
        public CartController(IShoppingContext shoppingContext, CheckoutService checkoutService)
        {
            _shoppingContext = shoppingContext;
            _checkoutService = checkoutService;
        }

        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    var cart = new ShoppingCart();
        //    cart.Add(6, new Jewelry("Bracelet", JewelrySize.Humongous, "foo.jpg"));

        //    var order = _checkoutService.CalculateOrder(cart);

        //    _shoppingContext.Add(order);
        //    _shoppingContext.Save();
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet("empty")]
        public JsonResult Empty()
        {
            var cart = _checkoutService.CreateCart();
            return Json(cart);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            return Json(_shoppingContext.Carts.FirstOrDefault(cart => cart.Id == id));
        }

        [HttpGet("{id}/calculate")]
        public JsonResult Calculate(Guid id)
        {
            var targetCart = _shoppingContext.Carts.FirstOrDefault(cart => cart.Id == id);
            if (targetCart == null)
                return Json("Not found");

            var order = _checkoutService.CalculateOrder(targetCart);
            return Json(order);
        }

        [HttpPost("{id}/add")]
        public JsonResult Add(Guid id, AddItemModel model)
        {
            var targetCart = _shoppingContext.Carts.FirstOrDefault(cart => cart.Id == id);
            if (targetCart == null)
                return Json("Cart not found");

            var itemToAdd = _shoppingContext.Catalog.FirstOrDefault(item => item.Id == model.ItemId);
            if (itemToAdd == null)
                return Json("Item not found");

            if (!string.IsNullOrEmpty(model.Customization))
            {
                var customItem = new CustomizedJewelry<Jewelry>(model.Customization, itemToAdd);
                targetCart.Add(model.Amount, model.Size, customItem);
            }
            else
            {
                targetCart.Add(model.Amount, model.Size, itemToAdd);
            }
            
            _shoppingContext.Save();
            return Json(targetCart);
        }
    }
}
