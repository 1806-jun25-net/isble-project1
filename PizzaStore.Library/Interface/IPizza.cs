using PizzaStore.Library.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.Interface
{
    public interface IPizza
    {
        string Crust { get; set; }
        string Size { get; set; }
        List<Toppings> Toppings { get; set; }
    }
}
