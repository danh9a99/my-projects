using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Dao;
using Model.EF;
using PagedList;
using webQLCF.Areas.Admin.Models;

namespace webQLCF.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private webQLCFCode db = new webQLCFCode();

        // GET: Admin/Product
        public ActionResult Index(int? size, int? page, string searchString)
        {
            // 1. Tạo list pageSize để người dùng có thể chọn xem để phân trang
            // Bạn có thể thêm bớt tùy ý --- dammio.com
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "9", Value = "9" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            // 1.1. Giữ trạng thái kích thước trang được chọn trên DropDownList
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }

            // 1.2. Tạo các biến ViewBag
            ViewBag.size = items; // ViewBag DropDownList
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại

            // 2. Nếu page = null thì đặt lại là 1.
            page = page ?? 1; //if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in db.Products
                         select l);
            
            // 4. Tạo kích thước trang (pageSize), mặc định là 5.
            int pageSize = (size ?? 9);

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            //var linksS = from l in db.Products
                        //select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.Name.Contains(searchString));
            }
            links = links.OrderBy(x => x.ID);
            return View(links.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ProductDetail(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }
        // GET: Admin/Product/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Code,MetaTitle,Description,Image,MoreImages,Price,PromotionPrice,IncludedVAT,Quantity,CategoryID,Detail,Warranty,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        //GET: Admin/Product/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        //// POST: Admin/Product/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,Code,MetaTitle,Description,Image,MoreImages,Price,PromotionPrice,IncludedVAT,Quantity,CategoryID,Detail,Warranty,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product, HttpPostedFileBase HinhAnh)
        //{
        //    //HttpPostedFileBase Image = null;
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        if (product.Image != null)
        //        {
        //            HinhAnh.SaveAs(HttpContext.Server.MapPath("~/Content/images/")
        //                                                     + HinhAnh.FileName);
        //            product.Image = HinhAnh.FileName;
        //            //user.Image = userVal.Image;
        //        }
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryID = new SelectList(db.ProductCategories, "ID", "Name", product.CategoryID);
        //    return View(product);
        //}
        IEnumerable<Product> GetAllProduct()
        {
            using (webQLCFCode db = new webQLCFCode())
            {
                return db.Products.ToList<Product>();
            }

        }
        //public ActionResult Edit(int id = 0)
        //{
        //    Product product = new Product();
        //    if (id != 0)
        //    {
        //        using (webQLCFCode db = new webQLCFCode())
        //        {
        //            product = db.Products.Where(x => x.ID == id).FirstOrDefault<Product>();
        //        }
        //    }
        //    return View(product);
        //}

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            // try
            //{
            if (product.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                string extension = Path.GetExtension(product.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                product.Image = "/Assets/client/images/" + fileName;
                product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Assets/client/images/"), fileName));
            }
            using (webQLCFCode db = new webQLCFCode())
            {
                if (product.ID == 0)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            // return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllProduct()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{

            //    return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            //}
            return View(product);
        }
        // GET: Admin/Product/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpDelete]
        public ActionResult Delete2(int id)
        {
            Product product = db.Products.Find(id);
            new ProductDao().Delete(id);
            return View("Index");
        }
        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //

    }
}
