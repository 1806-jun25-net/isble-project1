using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.Interface
{
    //interface for aspects of a pizza
    public interface IPizza
    {
        string Size { get; set; }
        bool Sauce { get; set; }
        List<string> Toppings { get; set; }
    }
}
