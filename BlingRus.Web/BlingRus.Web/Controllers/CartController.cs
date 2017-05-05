using System;
using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain;
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

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var cart = new ShoppingCart();
            cart.Add(6, new Jewelry("Bracelet", JewelrySize.Humongous, "foo.jpg"));

            var order = _checkoutService.CalculateOrder(cart);

            _shoppingContext.Add(order);
            _shoppingContext.Save();
            return new string[] { "value1", "value2" };
        }

        [HttpGet("empty")]
        public JsonResult Empty()
        {
            var cart = new ShoppingCart();
            _shoppingContext.Add(cart);
            _shoppingContext.Save();
            return Json(cart);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            return Json(_shoppingContext.Carts.FirstOrDefault(cart => cart.Id == id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
