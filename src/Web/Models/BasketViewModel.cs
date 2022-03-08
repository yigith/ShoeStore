using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public string BuyerId { get; set; }

        public List<BasketItemViewModel> Items { get; set; }

        public int TotalItemsCount => Items.Sum(x => x.Quantity);
    }
}
