using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webQLCF.Controllers
{
    public class ProductUController : Controller
    {
        // GET: Product
        private webQLCFCode db = new webQLCFCode();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDetail()
        {
            return View();
        }
    }
}