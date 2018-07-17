using System;
using System.Net;
using System.Threading.Tasks;
using BlingRus.Domain.Ordering;
using BlingRus.Domain.Shopping;
using BlingRus.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<JsonResult> Empty()
        {
            var cart = await _checkoutService.CreateCart();
            return Json(cart);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            var cart = await _shoppingContext.GetCartById(id);
            return Json(cart);
        }

        [HttpGet("{id}/calculate")]
        public async Task<JsonResult> Calculate(int id)
        {
            var targetCart = await _shoppingContext.GetCartById(id);
            if (targetCart == null)
                return Json("Not found");

            var order = _checkoutService.CalculateOrder(targetCart);
            return Json(order);
        }

        [HttpPost("{id}/add")]
        public async Task<JsonResult> Add(int id, AddItemModel model)
        {
            var targetCart = await _shoppingContext.GetCartById(id);
            if (targetCart == null)
                return JsonError(HttpStatusCode.NotFound, "Cart not found");

            var itemToAdd = await _shoppingContext.GetJewelryById(model.ItemId);
            if (itemToAdd == null)
                return JsonError(HttpStatusCode.BadRequest, "Item not found");

            try
            {
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

            await _shoppingContext.Save();
            return Json(targetCart);
        }

        private JsonResult JsonError(HttpStatusCode code, object result)
        {
            _httpContext.HttpContext.Response.StatusCode = (int)code;
            return new JsonResult(result);
        }
    }
}
