using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Library.PizzaStoreRepo;
using PizzaStore.WebApp.Models;
using lib = PizzaStore.Library;

namespace PizzaStore.WebApp.Controllers
{
    public class LocationController : Controller
    {
        public PizzaStoreRepository Repo { get; }

        public LocationController(PizzaStoreRepository repo)
        {
            Repo = repo;
        }

        // GET: Location
        public ActionResult Index()
        {
            var libLocation = Repo.GetLocations();
            var webOrder = libLocation.Select(x => new Location
            {
                StoreNumber = x.StoreNumber,
                Cheese = x.Cheese,
                Dough = x.Dough,
                Sauce = x.Sauce,
                Pepperoni = x.Pepperoni,
                Bbqchicken = x.BBQChicken,
                Chicken = x.Chicken,
                Ham = x.Ham,
                Sausage = x.Sausage,
                Onion = x.Onion,
                Pepper = x.Pepper,
                Pineapple = x.Pineapple
            });
            return View(webOrder);
        }

        public ActionResult OrderHistory(int id)
        {
            var libOrder = Repo.SortOrderHistoryTimeOfOrderAscendingForLocation(id);
            var webOrder = libOrder.Select(x => new Order
            {
                OrderId = x.OrderID,
                UserId = x.UserID,
                StoreNumber = x.Location,
                TotalPizzas = x.HowManyPizzas,
                Price = x.Price,
                TimeOfOrder = x.TimeOfOrder
            });
            return View(webOrder);
        }

        public ActionResult MostExpensive(int id)
        {
            var libOrder = Repo.SortOrderHistoryPriceOfOrderDescendingForLocation(id);
            var webOrder = libOrder.Select(x => new Order
            {
                OrderId = x.OrderID,
                UserId = x.UserID,
                StoreNumber = x.Location,
                TotalPizzas = x.HowManyPizzas,
                TimeOfOrder = x.TimeOfOrder,
                Price = x.Price
            });
            return View(webOrder);
        }

        public ActionResult LeastExpensive(int id)
        {
            var libOrder = Repo.SortOrderHistoryPriceOfOrderAscendingForLocation(id);
            var webOrder = libOrder.Select(x => new Order
            {
                OrderId = x.OrderID,
                UserId = x.UserID,
                StoreNumber = x.Location,
                TotalPizzas = x.HowManyPizzas,
                TimeOfOrder = x.TimeOfOrder,
                Price = x.Price
            });
            return View(webOrder);
        }

        public ActionResult MostRecent(int id)
        {
            var libOrder = Repo.SortOrderHistoryTimeOfOrderDescendingForLocation(id);
            var webOrder = libOrder.Select(x => new Order
            {
                OrderId = x.OrderID,
                UserId = x.UserID,
                StoreNumber = x.Location,
                TotalPizzas = x.HowManyPizzas,
                TimeOfOrder = x.TimeOfOrder,
                Price = x.Price
            });
            return View(webOrder);
        }
        // GET: Order/Details/5
        public ActionResult OrderDetails(int id)
        {
            var libPizza = Repo.GetOrderPizzaByOrderId(id);
            var webPizza = new PizzaPie
            {
                OrderId = libPizza.OrderID,
                Ham = libPizza.ToppingsDict["ham"],
                Sausage = libPizza.ToppingsDict["sausage"],
                Chicken = libPizza.ToppingsDict["chicken"],
                Pepperoni = libPizza.ToppingsDict["pepperoni"],
                Bbqchicken = libPizza.ToppingsDict["bbqchicken"],
                Onion = libPizza.ToppingsDict["onion"],
                Pepper = libPizza.ToppingsDict["pepper"],
                Pineapple = libPizza.ToppingsDict["pineapple"]
            };
            return View(webPizza);
        }
    }
}