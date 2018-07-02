using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.Interface
{
    public interface IInventory
    {
        int Dough { get; set; }
        int Cheese { get; set; }
        int Peperoni { get; set; }
        int Onion { get; set; }
        int Sauce { get; set; }
        int Ham { get; set; }
        int Sausage { get; set; }
        int Chicken { get; set; }
        int Pepper { get; set; }
        int Pineapple { get; set; }
        int BBQChicken { get; set; }
    }
}
