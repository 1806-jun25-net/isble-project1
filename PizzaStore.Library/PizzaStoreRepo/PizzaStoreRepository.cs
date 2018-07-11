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

        public void DeleteUserFromDB(User userID)
        {
            _db.Remove(_db.Users.Find(userID.ID));
        }

        public void UpdateLocationInventory(Location inventory)
        {
            _db.Entry(_db.Locations.Find(inventory.StoreNumber)).CurrentValues.SetValues(Mapper.Map(inventory));
        }

        public IEnumerable<Order> GetOrderHistory()
        {
            var OrderList = Mapper.Map(_db.Orders.Include(x => x.StoreNumber).Include(y => y.UserId).AsNoTracking());

            return OrderList;
        }

        public IEnumerable<User> GetUsers()
        {
            return Mapper.Map(_db.Users.Include(u => u.Orders).AsNoTracking());
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
