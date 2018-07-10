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
            _db = db;
        }
    }
}
