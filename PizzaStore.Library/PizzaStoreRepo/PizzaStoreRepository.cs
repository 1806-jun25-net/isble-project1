using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Library.PizzaStoreRepo
{
    public class PizzaStoreRepository
    {
        private readonly PizzaStoreDbContext _db;

        public PizzaStoreRepository(PizzaStoreDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddPizzaToDB(PizzaPie pizza)
        {
            _db.Add(Mapper.Map(pizza));
        }

        public void AddOrderToDB(Order order)
        {
            _db.Add(Mapper.Map(order));
        }

        public void AddLocationToDB(Location location)
        {
            _db.Add(Mapper.Map(location));
        }

        public void AddUserToDB(User user)
        {

            _db.Add(Mapper.Map(user));
        }

        public bool IsLocationInDB(int loc)
        {
            var locations = _db.Locations;
            foreach (var item in locations)
            {
                if (loc == item.StoreNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetUserLocation (string first, string last)
        {
            var users = _db.Users;
            foreach (var item in users)
            {
                if (first == item.FirstName && last == item.LastName)
                {
                    return item.PrefLocation;
                }
            }
            return 0;
        }
        public bool IsUserInDB(string first, string last)
        {
            var users = _db.Users;
            foreach (var item in users)
            {
                if (first == item.FirstName && last == item.LastName)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUser(string first, string last, int storenumber)
        {
            var users = _db.Users;
            foreach (var item in users)
            {
                if (first == item.FirstName && last == item.LastName && storenumber == item.PrefLocation)
                {
                    return Mapper.Map(item);
                }
            }
            return new User(first,last,storenumber);
        }

        public int GetUserID(User user)
        {
            var users = _db.Users;
            foreach (var item in users)
            {
                if (user.First == item.FirstName && user.Last == item.LastName)
                {
                    return item.Id;
                }
            }
            return -1;
        }

        public bool DoesUserHavePreviousOrders(User user)
        {
            var orders = _db.Orders;
            foreach (var item in orders)
            {
                if (user.ID == item.UserId)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Order> GetUserOrderHistory(User user)
        {
            var orders = _db.Orders;
            List<Order> ListOfOrders = new List<Order>();
            foreach (var item in orders)
            {
                if (user.ID == item.UserId)
                {
                    ListOfOrders.Add(Mapper.Map(item));
                }
            }
            return ListOfOrders;
        }
        public Order GetUserOrders(User user)
        {
            var orders = _db.Orders;
            Order lastitem = new Order();


            foreach (var item in orders)
            {
                if (user.ID == item.UserId)
                {
                    lastitem =  Mapper.Map(item);
                }
            }
            return lastitem;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
