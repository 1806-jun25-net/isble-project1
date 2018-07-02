using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    public class Location 
    {
        public List<Order> OrderHistory { get; set; }
        public string StoreNumber { get; set; }
    }
}
