using PizzaStore.Data;
using System;
using System.Collections.Generic;
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
        };

        public static Locations Map(Location location) => new Locations
        {
            StoreNumber = location.StoreNumber
        };

        public static Order Map(Orders order) => new Order
        {
            OrderID = order.OrderId,
            Price = order.Price,
            HowManyPizzas = order.TotalPizzas
            
        };
    }
}
