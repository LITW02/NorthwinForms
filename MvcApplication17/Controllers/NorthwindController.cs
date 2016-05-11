using MvcApplication17.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;


namespace MvcApplication17.Controllers
{
    public class NorthwindController : Controller
    {
        public ActionResult Products(string query)
        {
            NorthwindManager manager = new NorthwindManager(Properties.Settings.Default.ConStr);
            return View(manager.Search(query));
        }

        [HttpPost]
        public ActionResult AddRandom(string foo, string bar)
        {
            NorthwindManager manager = new NorthwindManager(Properties.Settings.Default.ConStr);
            manager.AddRandom(foo, bar);
            return Redirect("/northwind/products");
        }
    }
}
