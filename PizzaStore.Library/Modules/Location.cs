using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library
{
    //location of stores designated by store number rather than address
    //also order history of all orders placed at specified store number
    public class Location
    {
        public string StoreNumber { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();

        public int Dough { get; set; } = 1000;
        public int Cheese { get; set; } = 1000;
        public int Sauce { get; set; } = 1000;
        public int Pepperoni { get; set; } = 1000;
        public int Onion { get; set; } = 1000;
        public int Ham { get; set; } = 1000;
        public int Sausage { get; set; } = 1000;
        public int Chicken { get; set; } = 1000;
        public int Pepper { get; set; } = 1000;
        public int Pineapple { get; set; } = 1000;
        public int BBQChicken { get; set; } = 1000;

        public void DecreaseInventory(Order order)
        {
            //comparing wrong types, need to check that item is actually Dough, then subtract
            int totalPizzas = order.HowManyPizzas;
            Dough = Dough - totalPizzas;
            Cheese = Cheese - totalPizzas;
            if (order.Pizza.Sauce)
            {
                Sauce = Sauce - totalPizzas;
            }
            foreach (var item in order.Toppings)
            {
                switch (item)
                {
                    case "pepperoni":
                        Pepperoni = Pepperoni - totalPizzas;
                        break;
                    case "onion":
                        Onion = Onion - totalPizzas;
                        break;
                    case "ham":
                        Ham = Ham - totalPizzas;
                        break;
                    case "sausage":
                        Sausage = Sausage - totalPizzas;
                        break;
                    case "chicken":
                        Chicken = Chicken - totalPizzas;
                        break;
                    case "pepper":
                        Pepper = Pepper - totalPizzas;
                        break;
                    case "pineapple":
                        Pineapple = Pineapple - totalPizzas;
                        break;
                    case "bbqchicken":
                        BBQChicken = BBQChicken - totalPizzas;
                        break;

                }
            } 
            HashSet<string> tops = order.Toppings;
        }

        public Location()
        {

        }

        public void SetOrderHistory(Order order)
        {
            OrderHistory.Add(order);
        }

        public Location(string storenumber)
        {
            StoreNumber = storenumber;
        }
    }
}
