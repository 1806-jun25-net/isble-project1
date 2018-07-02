using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    //location of stores designated by store number rather than address
    //also order history of all orders placed at specified store number
    public class Location : Inventory
    {
        public List<Order> OrderHistory { get; set; }
        public string StoreNumber { get; set; }
    }
}
