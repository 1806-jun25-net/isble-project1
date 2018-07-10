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

        public ICollection<Orders> Orders { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
