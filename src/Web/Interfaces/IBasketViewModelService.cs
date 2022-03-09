using ApplicationCore.Entities;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetBasketViewModelAsync();

        Task<int> GetBasketItemsCountAsync();

        Task<BasketViewModel> AddToBasketAsync(int productId, int quantity);

        Task<Basket> GetOrCreateBasketAsync();

        Task<string> GetOrCreateBuyerIdAsync();
    }
}
