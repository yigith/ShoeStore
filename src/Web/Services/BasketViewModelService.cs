using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Basket> _basketRepo;
        private readonly IBasketService _basketService;

        public BasketViewModelService(IHttpContextAccessor httpContextAccessor, IRepository<Basket> basketRepo, IBasketService basketService)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketRepo = basketRepo;
            _basketService = basketService;
        }

        public async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            var basketId = (await GetOrCreateBasketAsync()).Id;
            var specBasket = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepo.FirstOrDefaultAsync(specBasket);
            return BasketToViewModel(basket);
        }

        public async Task<int> GetBasketItemsCountAsync()
        {
            var basket = await GetBasketViewModelAsync();
            return basket.TotalItemsCount;
        }

        public async Task<BasketViewModel> AddToBasketAsync(int productId, int quantity)
        {
            var basket = await GetOrCreateBasketAsync();
            basket = await _basketService.AddItemToBasketAsync(basket.Id, productId, quantity);

            return BasketToViewModel(basket);
        }

        private BasketViewModel BasketToViewModel(Basket basket)
        {
            return new BasketViewModel()
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(x => new BasketItemViewModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    ProductName = x.Product.Name,
                    PictureUri = x.Product.PictureUri,
                    UnitPrice = x.Product.Price
                }).ToList()
            };
        }

        public async Task<Basket> GetOrCreateBasketAsync()
        {
            var buyerId = await GetOrCreateBuyerIdAsync();
            var specBasket = new BasketSpecification(buyerId);
            var basket = await _basketRepo.FirstOrDefaultAsync(specBasket); 
            if (basket == null)
            {
                basket = new Basket()
                {
                    BuyerId = buyerId
                };
                await _basketRepo.AddAsync(basket);
            }
            return basket;
        }

        public async Task<string> GetOrCreateBuyerIdAsync()
        {
            var userId = GetLoggedInUserId();
            if (userId != null) return userId;
            var anonId = GetAnonymousBuyerId();
            if (anonId != null) return anonId;

            var newId = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.BASKET_COOKIENAME, newId, new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddDays(28)
            });
            return newId;
        }

        private string GetAnonymousBuyerId()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[Constants.BASKET_COOKIENAME];
        }

        private string GetLoggedInUserId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); ;
        }
    }
}
