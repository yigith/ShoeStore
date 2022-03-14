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
    public class OrderService : IOrderService
    {
        private readonly IRepository<Basket> _basketRepo;
        private readonly IRepository<Order> _orderRepo;

        public OrderService(IRepository<Basket> basketRepo, IRepository<Order> orderRepo)
        {
            _basketRepo = basketRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(int basketId, Address shippingAddress)
        {
            var spec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepo.FirstOrDefaultAsync(spec);
            if (basket == null)
                throw new ArgumentException($"Basket with id {basketId} can not be found.");
            if (basket.Items.Count == 0)
                throw new ArgumentException($"Basket does not contain any items.");

            Order order = new Order()
            {
                BuyerId = basket.BuyerId,
                OrderDate = DateTimeOffset.Now,
                ShipToAddress = shippingAddress,
                OrderItems = basket.Items.Select(x => new OrderItem()
                {
                    ProductId = x.ProductId,
                    PictureUri = x.Product.PictureUri,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.Product.Price
                }).ToList()
            };
            return await _orderRepo.AddAsync(order);
        }
    }
}
