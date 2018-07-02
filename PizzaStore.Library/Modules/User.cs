using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PizzaStore.Library
{
    public class User
    {
        [XmlAttribute]
        public Name Name { get; set; }
        public Location PrefLocation { get; set; }
        public string PrefOrder { get; set; }
        List<string> OrderHistory { get; set; }
        
        
    }
}
