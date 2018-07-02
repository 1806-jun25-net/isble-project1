using PizzaStore.Library.Interface;
using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    //aspects of a pizza
    public class Pizza : IPizza
    {
        public string Crust { get; set; }
        public string Size { get; set; }
        public bool Sauce { get; set; }
        public List<Toppings> Toppings { get; set; }
    }
}
