using PizzaStore.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzaStoreTest
{
    public class PizzaTest
    {
        [Fact]
        public void MakePizzaTest()
        {
            var testPizza = new PizzaPie();

            testPizza.MakePizza(true, new HashSet<string> { "pineapple" }, "m");

            Assert.Equal("m", testPizza.Size);
            Assert.Contains("pineapple", testPizza.Toppings);
            Assert.True(testPizza.Sauce);
        }

        [Fact]
        public void PricePizzaTest()
        {
            var testPizza = new PizzaPie();

            testPizza.MakePizza(true, new HashSet<string> { "pineapple" }, "m");

            testPizza.PricePizza("m", testPizza.Toppings, 10);

            Assert.Equal(8.50 * 10, testPizza.Price);
        }

        [Fact]
        public void SetUserOrderHistoryTest()
        {
            var testLocation = new Location("1");
            User testUser = new User("joseph", "isble", "1");
            PizzaPie testPizza = new PizzaPie();
            testPizza.MakePizza(true, new HashSet<string> { "pineapple" }, "m");
            var testOrder = new Order(10, testPizza.Toppings, testUser, testLocation.StoreNumber, testPizza);

            testUser.SetOrderHistory(testOrder);

            Assert.Contains(testOrder, testUser.OrderHistory);

        }

    [Fact]
        public void DecreaseInventoryTestOneItem()
        {
            var testLocation = new Location("1");
            PizzaPie testPizza = new PizzaPie();
            testPizza.MakePizza(true, new HashSet<string> { "pineapple" }, "m");
            var testOrder = new Order(10, testPizza.Toppings, new User("joseph", "isble", "1"), testLocation.StoreNumber, testPizza);

            testLocation.DecreaseInventory(testOrder);

            Assert.Equal(990, testLocation.Pineapple);
        }

        //[Fact]
        //public void ()
        //{
            
        //}

    }
}
