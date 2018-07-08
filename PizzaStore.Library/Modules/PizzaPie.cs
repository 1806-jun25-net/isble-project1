using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    //aspects of a pizza
    public class PizzaPie
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

        public void PricePizza(string size, HashSet<string> toppings)
        {
            if(size == "s")
            {
                Price += 7.00;
            }
            if (size == "m")
            {
                Price += 8.00;
            }
            if (size == "l")
            {
                Price += 9.00;
            }
            foreach (var top in toppings)
            {
                List<string> Meat = new List<string> { "pepperoni", "sausage", "chicken", "ham", "bbqchicken"};
                List<string> Other = new List<string> { "onion", "pepper", "pineapple" };
                if (Meat.Contains(top))
                {
                    Price += 1.00;
                }

                if (Other.Contains(top))
                {
                    Price += 0.50;
                }
            }
        }
    }
}
