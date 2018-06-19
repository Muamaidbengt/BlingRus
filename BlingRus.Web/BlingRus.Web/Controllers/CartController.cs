using System;
using System.Linq;
using System.Net;
using System.Threading;
using BlingRus.Domain;
using BlingRus.Domain.Ordering;
using BlingRus.Domain.Services;
using BlingRus.Domain.Shopping;
using BlingRus.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JewelrySize = BlingRus.Domain.Shopping.JewelrySize;

namespace BlingRus.Web.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IShoppingContext _shoppingContext;
        private readonly CheckoutService _checkoutService;
        public CartController(IHttpContextAccessor httpContext, IShoppingContext shoppingContext, CheckoutService checkoutService)
        {
            _httpContext = httpContext;
            _shoppingContext = shoppingContext;
            _checkoutService = checkoutService;
        }

        [HttpGet("empty")]
        public JsonResult Empty()
        {
            var cart = _checkoutService.CreateCart();
            return Json(cart);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(_shoppingContext.Carts.FirstOrDefault(cart => cart.Id == id));
        }

        [HttpGet("{id}/calculate")]
        public JsonResult Calculate(int id)
        {
            var targetCart = _shoppingContext.Carts.FirstOrDefault(cart => cart.Id == id);
            if (targetCart == null)
                return Json("Not found");

            var order = _checkoutService.CalculateOrder(targetCart);
            return Json(order);
        }

        [HttpPost("{id}/add")]
        public JsonResult Add(int id, AddItemModel model)
        {
            var targetCart = _shoppingContext.Carts.FirstOrDefault(cart => cart.Id == id);
            if (targetCart == null)
                return JsonError(HttpStatusCode.NotFound, "Cart not found");

            var itemToAdd = _shoppingContext.Catalog.FirstOrDefault(item => item.Id == model.ItemId);
            if (itemToAdd == null)
                return JsonError(HttpStatusCode.BadRequest, "Item not found");

            try
            {
                var rnd = new Random();
                if (rnd.Next(1, 100) > 70)
                {
                    // Oj vad jobbigt det var idag
                    Thread.Sleep(3000);
                }

                if (!string.IsNullOrEmpty(model.Customization))
                {
                    var customItem = new CustomizedJewelry<Jewelry>(model.Customization, itemToAdd);
                    targetCart.Add(model.Amount, model.Size, customItem);
                }
                else
                {
                    targetCart.Add(model.Amount, model.Size, itemToAdd);
                }
            }
            catch (Exception ex)
            {
                return JsonError(HttpStatusCode.BadRequest, ex.Message);
            }

            _shoppingContext.Save();
            return Json(targetCart);
        }

        private JsonResult JsonError(HttpStatusCode code, object result)
        {
            _httpContext.HttpContext.Response.StatusCode = (int)code;
            return new JsonResult(result);
        }
    }
}
