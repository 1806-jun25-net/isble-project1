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
    public class UsersController : Controller
    {
        public PizzaStoreRepository Repo { get; }

        public UsersController(PizzaStoreRepository repo)
        {
            Repo = repo;
        }

        // GET: Users
        public ActionResult Index([FromQuery] string search = null)
        {
            var libUser = Repo.GetUsers(search);
            var webUser = libUser.Select(x => new User
            {
                Id = x.ID,
                FirstName = x.First,
                LastName = x.Last,
                PrefLocation = x.PrefLocation
            });
            return View(webUser);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            var libUser = Repo.GetUserById(id);
            var webUser = new User
            {
                FirstName = libUser.First,
                LastName = libUser.Last,
                PrefLocation = libUser.PrefLocation
            };
            return View(webUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Repo.IsUserInDB(user.FirstName,user.LastName))
                    {
                        return View();
                    }
                    Repo.AddUserToDB(new lib.User
                    {
                        First = user.FirstName,
                        Last = user.LastName,
                        PrefLocation = user.PrefLocation
                    });
                    Repo.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Repo.IsUserInDB(user.FirstName, user.LastName))
                    {
                        return View();
                    }
                    Repo.AddUserToDB(new lib.User
                    {
                        First = user.FirstName,
                        Last = user.LastName,
                        PrefLocation = user.PrefLocation
                    });
                    Repo.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OrderHistory(int id)
        {
            var libOrder = Repo.SortOrderHistoryTimeOfOrderAscendingForUser(id);
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

        // GET: PizzaPie/Edit/5
        public ActionResult Edit(int id)
        {
            var libUser = Repo.GetUserById(id);
            var webUser = new User
            {
                PrefLocation = libUser.PrefLocation
            };
            return View(webUser);
        }

        // POST: PizzaPie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var libUser = new lib.User
                    {
                        PrefLocation = user.PrefLocation
                    };
                    //Repo.UpdateRestaurant(libRest);
                    //Repo.Save();

                    return RedirectToAction(nameof(Index));
                }
               // return View(restaurant);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}