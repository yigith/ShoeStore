using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetBasketViewModelAsync();

        Task<int> GetBasketItemsCountAsync();

        Task<BasketViewModel> AddToBasketAsync(int productId, int quantity);

        Task<BasketViewModel> UpdateBasketAsync(Dictionary<int, int> quantities);

        Task<Basket> GetOrCreateBasketAsync();

        Task<string> GetOrCreateBuyerIdAsync();

        Task<OrderCompleteViewModel> CompleteCheckoutAsync(Address shippingAddress);
    }
}
