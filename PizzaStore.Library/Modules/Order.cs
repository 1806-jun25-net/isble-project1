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
        public HashSet<string> Toppings { get; set; }
        public User User { get; set; }
        public Location Location { get; set; }
        public PizzaPie Pizza { get; set; }
        public DateTime TimeOfOrder { get; set; }

        public Order(int numberofpizzas, HashSet<string> toppings, User user, Location location)
        {
            if (numberofpizzas <= 0 || numberofpizzas > 12)
            {
                throw new ArgumentException("Number of pizzas ordered is wrong");
            }
            HowManyPizzas = numberofpizzas;
            Toppings = toppings;
            User = user;
            Location = location;
        }
    }
}
