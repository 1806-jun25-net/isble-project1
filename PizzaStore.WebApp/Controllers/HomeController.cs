using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Library.PizzaStoreRepo;
using PizzaStore.WebApp.Models;

namespace PizzaStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public PizzaStoreRepository Repo { get; }

        public HomeController(PizzaStoreRepository repo)
        {
            Repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(OrderView order)
        {
            order.StoreNumber = order.PrefLocation;

            if (Repo.IsUserInDB(order.FirstName, order.LastName))
            {
                order.UserId = Repo.GetUser(order.FirstName, order.LastName).ID;
                return View("Order", order);
            }
            var newUser = new Library.User
            {
                First = order.FirstName,
                Last = order.LastName,
                PrefLocation = order.PrefLocation
            };
            Repo.AddUserToDB(newUser);
            Repo.Save();
            return View("Order", order);
        }

        public ActionResult PerformOrder(OrderView order)
        {
            HashSet<string> toppings = new HashSet<string>();
            Dictionary<string, bool> toppingsdict = new Dictionary<string, bool>();

            if (order.Sausage)
            {
                toppings.Add("sausage");
            }
            if (order.Ham)
            {
                toppings.Add("ham");
            }
            if (order.Chicken)
            {
                toppings.Add("chicken");
            }
            if (order.Bbqchicken)
            {
                toppings.Add("bbqchicken");
            }
            if (order.Pepperoni)
            {
                toppings.Add("pepperoni");
            }
            if (order.Onion)
            {
                toppings.Add("onion");
            }
            if (order.Pepper)
            {
                toppings.Add("pepper");
            }
            if (order.Pineapple)
            {
                toppings.Add("pineapple");
            }

            toppingsdict.Add("pepperoni", order.Pepperoni);
            toppingsdict.Add("sausage", order.Sausage);
            toppingsdict.Add("ham", order.Ham);
            toppingsdict.Add("chicken", order.Chicken);
            toppingsdict.Add("bbqchicken", order.Bbqchicken);
            toppingsdict.Add("onion", order.Onion);
            toppingsdict.Add("pepper", order.Pepper);
            toppingsdict.Add("pineapple", order.Pineapple);

            Library.PizzaPie newPizza = new Library.PizzaPie()
            {
                Sauce = order.Sauce,
                Size = order.Size,
                Ham = order.Ham,
                Sausage = order.Sausage,
                Chicken = order.Chicken,
                Pepperoni = order.Pepperoni,
                Bbqchicken = order.Bbqchicken,
                Onion = order.Onion,
                Pepper = order.Pepper,
                Pineapple = order.Pineapple
            };

            newPizza.PricePizza(order.Size, toppings, order.TotalPizzas);

            Library.Order newOrder = new Library.Order()
            {
                UserID = Repo.GetUser(order.FirstName,order.LastName).ID,
                Location = order.PrefLocation,
                HowManyPizzas = order.TotalPizzas,
                Price = newPizza.Price,
                TimeOfOrder = DateTime.Now
            };
            Repo.AddOrderToDB(newOrder);
            Repo.Save();

            newPizza.UpdatePizzaOrderID(Repo.GetRecentOrderId());
            newPizza.UpdateToppingDict(toppingsdict);
            Repo.AddPizzaToDB(newPizza);
            Repo.Save();

            return View("OrderComplete", order);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
