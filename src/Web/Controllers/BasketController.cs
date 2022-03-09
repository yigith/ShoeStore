using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketViewModelService.GetBasketViewModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId, int quantity = 1)
        {
            var basket = await _basketViewModelService.AddToBasketAsync(productId, quantity);
            return Json(new NavBasketViewModel() { TotalItemsCount = basket.TotalItemsCount });
        }
    }
}
