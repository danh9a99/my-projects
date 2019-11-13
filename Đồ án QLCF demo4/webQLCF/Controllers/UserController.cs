using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webQLCF.Models;
using System.Configuration;
using System.Xml.Linq;
using Raven.Abstractions.Util.Encryptors;
using webQLCF.Common;
using Demo.EF;
using Model.EF;

namespace webQLCF.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginForAddToCart()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, model.Password, false);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "Không có quyền đăng nhập bằng tài khoản này.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult LoginForAddToCart(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, model.Password, false);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    //return RedirectToAction("Index", "Cart");
                    return Redirect("/them-gio-hang?productId=4&quantity=1");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "Không có quyền đăng nhập bằng tài khoản này.");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        //[CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Password = model.Password;
                    //user.Phone = model.Phone;
                    user.Email = model.Email;
                    //user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    //if (!string.IsNullOrEmpty(model.ProvinceID))
                    //{
                    //    user.ProvinceID = int.Parse(model.ProvinceID);
                    //}
                    //if (!string.IsNullOrEmpty(model.DistrictID))
                    //{
                    //    user.DistrictID = int.Parse(model.DistrictID);
                    //}

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }
    }
}