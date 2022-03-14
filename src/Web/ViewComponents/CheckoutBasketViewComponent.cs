using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.ViewComponents
{
    public class CheckoutBasketViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
