using PizzaStore.Library.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.Modules
{
    //possible items stores could have
    public class Inventory : IInventory
    {
        public int Dough { get; set; } = 1000;
        public int Cheese { get; set; } = 1000;
        public int Sauce { get; set; } = 1000;
        public int Peperoni { get; set; } = 1000;
        public int Onion { get; set; } = 1000;
        public int Ham { get; set; } = 1000;
        public int Sausage { get; set; } = 1000;
        public int Chicken { get; set; } = 1000;
        public int Pepper { get; set; } = 1000;
        public int Pineapple { get; set; } = 1000;
        public int BBQChicken { get; set; } = 1000;

        public void DecreaseInventory(List<int> toppings, int numberofpizzas)
        {
            foreach (var item in toppings)
            {
                if (item == Dough)
                {

                }
            }
        }
    }
}
