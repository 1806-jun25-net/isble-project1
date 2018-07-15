using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Library;
using PizzaStore.Library.PizzaStoreRepo;

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
            var libUser = Repo.GetUserOrderHistory(search);
            var webUser = libUser.Select(x => new User
            {
                Id = x.ID,
                FirstName = x.First,
                LastName = x.Last,
                PrefLocation = x.PrefLocation
            });
            return View(webUser);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            var libUser = Repo.GetUserOrderHistory(id);
            var webUser = new Order
            {
                FirstName = libUser.First,
                LastName = libUser.Last,
                PrefLocation = libUser.PrefLocation
            };
            return View(webUser);
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}