using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PizzaStore.Library
{
    //user information, name prefered location, prefered order, and order history
    public class User
    {
        [XmlAttribute]
        public int ID { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public int PrefLocation { get; set; }
        
        

        public User()
        {

        }

        public void SetOrderHistory(Order order)
        {
            OrderHistory.Add(order);
        }

        public User(string first, string last)
        {
            First = first;
            Last = last;
        }

        public User(string first, string last, int storeNumber)
        {
            First = first;
            Last = last;
            PrefLocation = storeNumber;
        }
    }
}
