using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    //questions that will be asked to user placeing an order for pizza
    public class Order
    {
        public int HowManyPizzas { get; set; }
        public List<string> Toppings { get; set; }
        public string User { get; set; }
        public string Location { get; set; }


        public void MaxPizzas(int numberOfPizza)
        {
            if (numberOfPizza < 12 && numberOfPizza > 0)
            {
                HowManyPizzas = numberOfPizza;
            }
        }
    }
}
