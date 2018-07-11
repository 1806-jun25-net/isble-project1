using System;
using System.Collections.Generic;

namespace PizzaStore.Data
{
    public partial class Locations
    {
        public Locations()
        {
            Orders = new HashSet<Orders>();
            Users = new HashSet<Users>();
        }

        public int StoreNumber { get; set; }
        public int Dough { get; set; }
        public int Cheese { get; set; }
        public int Sauce { get; set; }
        public int Onion { get; set; }
        public int Pepper { get; set; }
        public int Pineapple { get; set; }
        public int Ham { get; set; }
        public int Chicken { get; set; }
        public int Sausage { get; set; }
        public int Bbqchicken { get; set; }
        public int Pepperoni { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
