using PizzaStore.Library.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    public class Pizza : IPizza
    {
        public string Crust { get; set; }
        public string Size { get; set; }
        public List<string> Toppings { get; set; }
        public int Peperoni { get; set; }
        public int Onion { get; set; }
        public int Sauce { get; set; }
        public int Ham { get; set; }
        public int Sausage { get; set; }
        public int Chicken { get; set; }
        public int Pepper { get; set; }
        public int Pineapple { get; set; }
        public int BBQChicken { get; set; }
    }
}
