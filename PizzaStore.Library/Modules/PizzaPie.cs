using PizzaStore.Library.Interface;
using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    //aspects of a pizza
    public class PizzaPie : IPizza
    {
        public string Crust { get; set; }
        public string Size { get; set; }
        public bool Sauce { get; set; }
        public List<Toppings> Toppings { get; set; }


        public void MakePizza(bool sauce, int toppings, string crust, string size)
        {

        }
    }
}
