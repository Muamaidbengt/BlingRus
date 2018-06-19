using BlingRus.Domain;
using BlingRus.Domain.Shopping;
using Microsoft.AspNetCore.Mvc;

namespace BlingRus.Web.ViewComponents
{
    public class ConfirmationMailViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Order order)
        {
            return View(order);
        }
    }
}
