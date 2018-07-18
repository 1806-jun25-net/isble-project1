using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //Add objects to respective table in Database
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
        
        //Sort order history section
        public List<Order> SortOrderHistoryTimeOfOrderDescending()
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderByDescending(x => x.TimeOfOrder));
        }
        public List<Order> SortOrderHistoryTimeOfOrderAscending()
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderBy(x => x.TimeOfOrder));
        }
        public List<Order> SortOrderHistoryPriceOfOrderDescending()
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderByDescending(x => x.Price));
        }
        public List<Order> SortOrderHistoryPriceOfOrderAscending()
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderBy(x => x.Price));
        }

        //Sort orders for users
        public List<Order> SortOrderHistoryTimeOfOrderDescendingForUser(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderByDescending(x => x.TimeOfOrder).Where(x => x.UserId == id));
        }
        public List<Order> SortOrderHistoryTimeOfOrderAscendingForUser(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderBy(x => x.TimeOfOrder).Where(x => x.UserId == id));
        }
        public List<Order> SortOrderHistoryPriceOfOrderDescendingForUser(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderByDescending(x => x.Price).Where(x => x.UserId == id));
        }
        public List<Order> SortOrderHistoryPriceOfOrderAscendingForUser(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderBy(x => x.Price).Where(x => x.UserId == id));
        }
        
        //Sort Order for locations
        public List<Order> SortOrderHistoryTimeOfOrderDescendingForLocation(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderByDescending(x => x.TimeOfOrder).Where(x => x.StoreNumber == id));
        }
        public List<Order> SortOrderHistoryTimeOfOrderAscendingForLocation(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderBy(x => x.TimeOfOrder).Where(x => x.StoreNumber == id));
        }
        public List<Order> SortOrderHistoryPriceOfOrderDescendingForLocation(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderByDescending(x => x.Price).Where(x => x.StoreNumber == id));
        }
        public List<Order> SortOrderHistoryPriceOfOrderAscendingForLocation(int id)
        {
            var orders = _db.Orders;
            return Mapper.Map(orders.OrderBy(x => x.Price).Where(x => x.StoreNumber == id));
        }

        public PizzaPie GetOrderPizzaByOrderId(int id)
        {
            var pizza = _db.PizzaPie;
            foreach (var item in pizza)
            {
                if (item.OrderId == id)
                {
                    return Mapper.Map(item);
                }
            }
            return null;
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

        public int GetUserLocation(string first, string last)
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

        public Location GetLocationById(int id)
        {
            var location = _db.Locations;
            foreach (var item in location)
            {
                if (item.StoreNumber == id)
                {
                    return Mapper.Map(item);
                }
            }
            return null;
        }

        public List<Location> GetLocations()
        {
            var location = _db.Locations;
            List<Location> listLocations = new List<Location>();
            foreach (var item in location)
            {
                listLocations.Add(Mapper.Map(item));
            }
            return listLocations;
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

        public List<User> GetUsers(string search = null)
        {
            var users = _db.Users;
            List<User> listOfUsers = new List<User>();
            if (search == null)
            {
                foreach (var item in users)
                {
                    listOfUsers.Add(Mapper.Map(item));
                }
            }
            foreach (var item in users)
            {
                if (item.FirstName == search || item.LastName == search)
                {
                    listOfUsers.Add(Mapper.Map(item));
                }
            }
            return listOfUsers;
        }

        public User GetUserById(int id)
        {
            var users = _db.Users;
            foreach (var item in users)
            {
                if (id == item.Id)
                {
                    return Mapper.Map(item);
                }
            }
            return null;
        }

        public int GetRecentOrderId()
        {
            var orders = _db.Orders;
            int id = 0;
            foreach (var item in orders)
            {
                id = item.OrderId;
            }
            return id;
        }

        public List<Order> GetOrderByUserId(int id)
        {
            var order = _db.Orders;
            List<Order> listOfOrder = new List<Order>();
            foreach (var item in order)
            {
                if (item.UserId == id)
                {
                    listOfOrder.Add(Mapper.Map(item));
                }
            }
            return listOfOrder;
        }
        public User GetUser(string first, string last)
        {
            var users = _db.Users;
            foreach (var item in users)
            {
                if (first == item.FirstName && last == item.LastName)
                {
                    return Mapper.Map(item);
                }
            }
            return new User(first,last);
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

        public List<Order> GetLocationOrderHistory(Location location)
        {
            var orders = _db.Orders;
            List<Order> ListOfOrders = new List<Order>();
            foreach (var item in orders)
            {
                if (location.StoreNumber == item.StoreNumber)
                {
                    ListOfOrders.Add(Mapper.Map(item));
                }
            }
            return ListOfOrders;
        }

        public List<Order> GetLocationOrderHistoryById(int id)
        {
            var orders = _db.Orders;
            List<Order> ListOfOrders = new List<Order>();
            foreach (var item in orders)
            {
                if (item.StoreNumber == id)
                {
                    ListOfOrders.Add(Mapper.Map(item));
                }
            }
            return ListOfOrders;
        }
        public Order GetUserRecentOrder(User user)
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

        public void UpdateLocationInventory(Location location)
        {
            _db.Entry(_db.Locations.Find(location.StoreNumber)).CurrentValues.SetValues(Mapper.Map(location));
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
