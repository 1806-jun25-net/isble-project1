using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    public class Order
    {
        public int HowManyPizzas { get; set; }
        public List<Toppings> Toppings { get; set; }
    }
}
