using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> AddItemToBasketAsync(int basketId, int productId, int quantity);

        Task<Basket> SetQuantitiesAsync(int basketId, Dictionary<int, int> quantities);

        Task DeleteBasketAsync(int basketId);

        Task DeleteBasketItemAsync(int basketId, int basketItemId);
    }
}
