using PizzaStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaStore.Library
{
    public static class Mapper
    {
        public static User Map(Users users) => new User
        {
            ID = users.Id,
            First = users.FirstName,
            Last = users.LastName,
            PrefLocation = users.PrefLocation
        };

        public static Users Map(User users) => new Users
        {
            Id = users.ID,
            FirstName = users.First,
            LastName = users.Last,
            PrefLocation = users.PrefLocation
        };

        public static Location Map(Locations location) => new Location
        {
            StoreNumber = location.StoreNumber,
            Dough = location.Dough,
            Cheese = location.Cheese,
            Sauce = location.Sauce,
            Onion = location.Onion,
            Pepper = location.Pepper,
            Pineapple = location.Pineapple,
            Ham = location.Ham,
            Chicken = location.Chicken,
            Sausage = location.Sausage,
            BBQChicken = location.Bbqchicken,
            Pepperoni = location.Pepperoni
        };

        public static Locations Map(Location location) => new Locations
        {
            StoreNumber = location.StoreNumber,
            Dough = location.Dough,
            Cheese = location.Cheese,
            Sauce = location.Sauce,
            Onion = location.Onion,
            Pepper = location.Pepper,
            Pineapple = location.Pineapple,
            Ham = location.Ham,
            Chicken = location.Chicken,
            Sausage = location.Sausage,
            Bbqchicken = location.BBQChicken,
            Pepperoni = location.Pepperoni
        };

        public static Order Map(Orders order) => new Order
        {
            OrderID = order.OrderId,
            Price = order.Price,
            HowManyPizzas = order.TotalPizzas,
            TimeOfOrder = order.TimeOfOrder,
            Location = order.StoreNumber,
            UserID = order.UserId
        };

        public static Orders Map(Order order) => new Orders
        {
            OrderId = order.OrderID,
            Price = order.Price,
            TotalPizzas = order.HowManyPizzas,
            TimeOfOrder = order.TimeOfOrder,
            StoreNumber = order.Location,
            UserId = order.UserID
        };

        public static PizzaPie Map(Data.PizzaPie pizza) => new PizzaPie
        {
            ID = pizza.Id,
            OrderID =pizza.OrderId,
            ToppingsDict = new Dictionary<string, bool>
            {
                {"pepper", pizza.Pepper },
                {"onion", pizza.Onion },
                {"pineapple", pizza.Pineapple},
                {"ham", pizza.Ham},
                {"chicken", pizza.Chicken},
                {"sausage", pizza.Sausage},
                {"bbqchicken", pizza.Bbqchicken},
                {"pepperoni", pizza.Pepperoni},
            }
        };

        public static Data.PizzaPie Map(PizzaPie pizza) => new Data.PizzaPie
        {
            Id = pizza.ID,
            OrderId = pizza.OrderID,
            Pepper = pizza.ToppingsDict["pepper"],
            Onion = pizza.ToppingsDict["onion"],
            Pineapple = pizza.ToppingsDict["pineapple"],
            Ham = pizza.ToppingsDict["ham"],
            Chicken = pizza.ToppingsDict["chicken"],
            Sausage = pizza.ToppingsDict["sausage"],
            Bbqchicken = pizza.ToppingsDict["bbqchicken"],
            Pepperoni = pizza.ToppingsDict["pepperoni"]
        };

        public static IEnumerable<User> Map(IEnumerable<Users> users) => users.Select(Map);
        public static IEnumerable<Users> Map(IEnumerable<User> users) => users.Select(Map);
        public static IEnumerable<Location> Map(IEnumerable<Locations> location) => location.Select(Map);
        public static IEnumerable<Locations> Map(IEnumerable<Location> location) => location.Select(Map);
        public static IEnumerable<Order> Map(IEnumerable<Orders> order) => order.Select(Map);
        public static IEnumerable<Orders> Map(IEnumerable<Order> order) => order.Select(Map);
        public static IEnumerable<PizzaPie> Map(IEnumerable<Data.PizzaPie> pizza) => pizza.Select(Map);
        public static IEnumerable<Data.PizzaPie> Map(IEnumerable<PizzaPie> pizza) => pizza.Select(Map);
    }
}
