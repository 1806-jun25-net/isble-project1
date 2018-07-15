using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.WebApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int StoreNumber { get; set; }
        public int TotalPizzas { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeOfOrder { get; set; }
    }
}
