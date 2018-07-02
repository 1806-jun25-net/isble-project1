using PizzaStore.Library.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.Modules
{
    //possible items stores could have
    public class Inventory : IInventory
    {
        public int Dough { get; set; }
        public int Cheese { get; set; }
        public int Sauce { get; set; }
        public int Peperoni { get; set; }
        public int Onion { get; set; }
        public int Ham { get; set; }
        public int Sausage { get; set; }
        public int Chicken { get; set; }
        public int Pepper { get; set; }
        public int Pineapple { get; set; }
        public int BBQChicken { get; set; }
    }
}
