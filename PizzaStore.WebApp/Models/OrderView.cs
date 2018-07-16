using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.WebApp.Models
{
    public class OrderView
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PrefLocation { get; set; }

        public int OrderId { get; set; }
        public int OrderUserId { get; set; }
        public int StoreNumber { get; set; }
        public int TotalPizzas { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeOfOrder { get; set; }

        public int PizzaId { get; set; }
        public int PizzaOrderId { get; set; }
        public string Size { get; set; }
        public bool Sauce { get; set; }
        public bool Onion { get; set; }
        public bool Pepper { get; set; }
        public bool Pineapple { get; set; }
        public bool Ham { get; set; }
        public bool Chicken { get; set; }
        public bool Sausage { get; set; }
        public bool Bbqchicken { get; set; }
        public bool Pepperoni { get; set; }
    }
}
