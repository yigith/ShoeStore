using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class BasketWithItemsSpecification : Specification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            Query.Where(x => x.Id == basketId)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product);
        }

        public BasketWithItemsSpecification(string buyerId)
        {
            Query.Where(x => x.BuyerId == buyerId)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product);
        }
    }
}
