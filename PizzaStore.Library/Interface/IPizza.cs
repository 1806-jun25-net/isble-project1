using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.Interface
{
    //interface for aspects of a pizza
    public interface IPizza
    {
        string Crust { get; set; }
        string Size { get; set; }
        bool Sauce { get; set; }
        List<Toppings> Toppings { get; set; }
    }
}
