using Model.Dao;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
namespace webQLCF.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
       public ActionResult Index()
        {
            return View();
        }
    }
}