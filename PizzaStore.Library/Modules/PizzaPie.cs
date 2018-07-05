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
        public string Size { get; set; }
        public bool Sauce { get; set; }
        public HashSet<string> Toppings { get; set; }
        public double Price { get; set; }


        public void MakePizza(bool sauce, HashSet<string> toppings, string size)
        {
            Sauce = sauce;
            Size = size;
            Toppings = toppings;
        }

        public void pricePizza(string size, HashSet<string> toppings)
        {
            if(size == "S")
            {
                Price += 7.00;
            }
            if (size == "M")
            {
                Price += 8.00;
            }
            if (size == "L")
            {
                Price += 9.00;
            }
        }
    }
}
