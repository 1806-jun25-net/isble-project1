using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PizzaStore.Library
{
    //aspects of a pizza
    public class PizzaPie
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string Size { get; set; }
        public bool Sauce { get; set; }
        public HashSet<string> Toppings { get; set; }
        public decimal Price { get; set; }
        [XmlIgnore]
        public Dictionary<string,bool> ToppingsDict { get; set; }
        


        public void MakePizza(bool sauce, HashSet<string> toppings, string size)
        {
            Sauce = sauce;
            Size = size;
            List<string> ListOfToppings = new List<string> { "pepperoni", "sausage", "chicken", "ham", "bbqchicken", "onion", "pepper", "pineapple" };

            foreach (var top in toppings)
            {
                if (!ListOfToppings.Contains(top))
                {
                    toppings.Remove(top);
                    throw new ArgumentException($"{top} is not a valid topping");
                }
            }
            Toppings = toppings;
        }

        public void PricePizza(string size, HashSet<string> toppings, int numberofpizza)
        {
            if(size == "s")
            {
                Price += (decimal)7.00 * numberofpizza;
            }
            if (size == "m")
            {
                Price += (decimal)8.00 * numberofpizza;
            }
            if (size == "l")
            {
                Price += (decimal)9.00 * numberofpizza;
            }
            foreach (var top in toppings)
            {
                List<string> Meat = new List<string> { "pepperoni", "sausage", "chicken", "ham", "bbqchicken"};
                List<string> Other = new List<string> { "onion", "pepper", "pineapple" };
                if (Meat.Contains(top))
                {
                    Price += (decimal)1.00 * numberofpizza;
                }

                if (Other.Contains(top))
                {
                    Price += (decimal)0.50 * numberofpizza;
                }
            }   
        }
        public void UpdatePizzaID(int id)
        {
            ID = id;
        }
    }
}
