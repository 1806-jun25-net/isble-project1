using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    //questions that will be asked to user placeing an order for pizza
    public class Order
    {
        public string User { get; set; }
        public int HowManyPizzas { get; set; }
        public PizzaPie Pizza { get; set; }
        public HashSet<string> Toppings { get; set; }
        public Location Location { get; set; }
        public DateTime TimeOfOrder { get; set; }
        public double Price { get; set; }

        public Order()
        {

        }

        public Order(int numberofpizzas, HashSet<string> toppings, User user, Location location, PizzaPie pizza)
        {
            if (numberofpizzas <= 0 || numberofpizzas > 12)
            {
                throw new ArgumentException("Number of pizzas ordered is wrong");
            }

            List<string> ListOfToppings = new List<string> { "pepperoni", "sausage", "chicken", "ham", "bbqchicken", "onion", "pepper", "pineapple" };

            foreach (var top in toppings)
            {
                if (!ListOfToppings.Contains(top))
                {
                    toppings.Remove(top);
                    throw new ArgumentException($"{top} is not a valid topping");
                }
            }

            HowManyPizzas = numberofpizzas;
            Toppings = toppings;
            User = user.First+user.Last;
            Location = location;
            Pizza = pizza;
        }

        public void UpdateToppings(HashSet<string> toppings)
        {
            Toppings = toppings;
        }

        public void AddPizzaToOrder(PizzaPie pizza)
        {
            Pizza = pizza;
        }

        public void UpdatePriceOfOrder(double price)
        {
            Price = price;
        }

        public void TimepizzaWasOrdered()
        {
            TimeOfOrder = DateTime.Now;
        }
    }
}
