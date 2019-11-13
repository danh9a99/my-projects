using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webQLCF.Areas.Admin.Controllers
{
    public class UpdateProductController : Controller
    {
        private webQLCFCode db = new webQLCFCode();
        // GET: Admin/UpdateProduct
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            Product p = new Product();
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/client/images"), _FileName);
                    p.Image = _path;
                    file.SaveAs(p.Image);
                    return View(p);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return Redirect("/Admin/Edit");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}