
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webQLCF.Common;
using webQLCF.Models;

namespace webQLCF.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
    }
}