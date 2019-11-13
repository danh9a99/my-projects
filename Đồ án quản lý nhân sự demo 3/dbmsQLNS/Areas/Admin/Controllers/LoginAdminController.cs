using dbmsQLNS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using webQLCF.Areas.Admin.Code;

namespace dbmsQLNS.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        QuanLyNhanSuEntities db = new QuanLyNhanSuEntities();
        // GET: Admin/LoginAdmin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NhanVien nhanVien)
        {
            object[] sqlParams =
            {
                new SqlParameter ("@UserName", nhanVien.MaNhanVien),
                new SqlParameter ("@PassWord", nhanVien.MatKhau)
            };
            var res = db.Database.SqlQuery<bool>("Sp_Account_Login @UserName, @PassWord", sqlParams).SingleOrDefault();
            if (res && ModelState.IsValid)
            {
                SessionHelper.SetSession(new UserSession() { UserName = nhanVien.MaNhanVien });
                Session["UserName"] = nhanVien.MaNhanVien;
                return RedirectToAction("Index", "HomeAdmin");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(nhanVien);
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return Redirect("/admin/loginadmin/index");
        }
    }
}