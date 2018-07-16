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
    public class OrderController : Controller
    {
        public PizzaStoreRepository Repo { get; }

        public OrderController(PizzaStoreRepository repo)
        {
            Repo = repo;
        }

        // GET: Order
        public ActionResult Index([FromQuery] string search)
        {
            var libOrder = Repo.SortOrderHistoryTimeOfOrderAscending();
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

        public ActionResult MostExpensive()
        {
            var libOrder = Repo.SortOrderHistoryPriceOfOrderDescending();
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

        public ActionResult LeastExpensive()
        {
            var libOrder = Repo.SortOrderHistoryPriceOfOrderAscending();
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

        public ActionResult MostRecent()
        {
            var libOrder = Repo.SortOrderHistoryTimeOfOrderDescending();
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
        public ActionResult Details(int id)
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

        //// GET: Order/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Order/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}