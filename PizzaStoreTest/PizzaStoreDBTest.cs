using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaStore.Data;
using PizzaStore.Library;
using PizzaStore.Library.PizzaStoreRepo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace PizzaStoreTest
{
    public class PizzaStoreDBTest
    {

        [Fact]
        public void TestAddUserToDBAndGetUserFromDB()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzastoreDB"));
            var options = optionsBuilder.Options;

            var dbContext = new PizzaStoreDbContext(options);
            var PizzaStoreRepository = new PizzaStoreRepository(dbContext);


            User testuser = new User("Kylo", "Ren", 1);
            PizzaStoreRepository.AddUserToDB(testuser);
            PizzaStoreRepository.Save();
            Assert.Equal(testuser.First, PizzaStoreRepository.GetUser(testuser.First,testuser.Last).First);
        }

        [Fact]
        public void TestGetLocation()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzastoreDB"));
            var options = optionsBuilder.Options;

            var dbContext = new PizzaStoreDbContext(options);
            var PizzaStoreRepository = new PizzaStoreRepository(dbContext);

            var actual = PizzaStoreRepository.GetLocationById(3);

            
            Assert.Equal(3 ,actual.StoreNumber);
        }

        [Fact]
        public void TestIsUserInDB()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzastoreDB"));
            var options = optionsBuilder.Options;

            var dbContext = new PizzaStoreDbContext(options);
            var PizzaStoreRepository = new PizzaStoreRepository(dbContext);

            var actual = PizzaStoreRepository.IsUserInDB("joseph","isble");


            Assert.True(actual);
        }

        [Fact]
        public void TestAddOrderToDB()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzastoreDB"));
            var options = optionsBuilder.Options;

            var dbContext = new PizzaStoreDbContext(options);
            var PizzaStoreRepository = new PizzaStoreRepository(dbContext);

            Order neworder = new Order()
            {
                HowManyPizzas = 4,
                Location = 2,
                UserID = 4,
                TimeOfOrder = DateTime.Now,
            };
            PizzaStoreRepository.AddOrderToDB(neworder);
            var actual = PizzaStoreRepository.GetRecentOrderId();


            Assert.Equal(14,actual);
        }

        //[Fact]
        //public void GetOrderHistoryFromDBTest()
        //{

        //}

    }
}
