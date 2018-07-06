using PizzaStore.Library.Modules;
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
        public string First { get; set; }
        public string Last { get; set; }
        public Location PrefLocation { get; set; }
        public Order PrefOrder { get; set; }
        List<Order> OrderHistory { get; set; }

        public User()
        {

        }


        public User(string first, string last, string storeNumber)
        {
            First = first;
            Last = last;
            PrefLocation = new Location(storeNumber);
        }

        public User(string first, string last, Order preforder, Location preflocation)
        {
            First = first;
            Last = last;
            PrefOrder = preforder;
            PrefLocation = preflocation;
        }


    }
}
