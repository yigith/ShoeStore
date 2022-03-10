using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<BasketItem> _basketItemRepo;

        public BasketService(IRepository<Basket> basketRepo, IRepository<Product> productRepo, IRepository<BasketItem> basketItemRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _basketItemRepo = basketItemRepo;
        }

        public async Task<Basket> AddItemToBasketAsync(int basketId, int productId, int quantity)
        {
            if (quantity < 1)
                throw new ArgumentException("Quantity must be greater than zero.");
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null) 
                throw new ArgumentException($"Product with the id {productId} can not be found.");
            var spec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepo.FirstOrDefaultAsync(spec);
            if (basket == null)
                throw new ArgumentException($"Basket with id {basketId} can not be found.");

            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (basketItem == null)
            {
                basketItem = new BasketItem() { BasketId = basketId, Quantity = quantity, ProductId = productId, Product = product };
                basket.Items.Add(basketItem);
            }
            else
            {
                basketItem.Quantity += quantity;
            }
            await _basketRepo.UpdateAsync(basket);

            return basket;
        }

        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await _basketRepo.GetByIdAsync(basketId);
            if (basket == null)
                throw new ArgumentException($"Basket with id {basketId} can not be found.");
            await _basketRepo.DeleteAsync(basket);
        }

        public async Task DeleteBasketItemAsync(int basketId, int basketItemId)
        {
            var basketItem = await _basketItemRepo.GetByIdAsync(basketItemId);
            if (basketItem == null || basketItem.BasketId != basketId)
                throw new ArgumentException("Basket item can not be found.");
            await _basketItemRepo.DeleteAsync(basketItem);
        }

        public async Task<Basket> SetQuantitiesAsync(int basketId, Dictionary<int, int> quantities)
        {
            if (quantities.Values.Any(x => x < 1))
                throw new ArgumentException("All quantities must be greater than zero.");
            var spec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepo.FirstOrDefaultAsync(spec);
            if (basket == null)
                throw new ArgumentException($"Basket with id {basketId} can not be found.");

            foreach (var item in basket.Items)
            {
                if (quantities.ContainsKey(item.ProductId))
                {
                    item.Quantity = quantities[item.ProductId];
                }
            }
            await _basketRepo.UpdateAsync(basket);

            return basket;
        }
    }
}
