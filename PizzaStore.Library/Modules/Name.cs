using PizzaStore.Library.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.Modules
{
    public class Name : IName
    {
        public string First { get; set; }
        public string Last { get; set; }
    }
}
