using BlingRus.Domain;
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
