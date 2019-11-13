using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using PagedList;

namespace webQLCF.Areas.Admin.Controllers
{
    public class LinkController : Controller
    {
        private webQLCFCode db = new webQLCFCode();

        // GET: Admin/Link
        public ActionResult Index(int? size, int? page)
        {

            // 1. Tạo list pageSize để người dùng có thể chọn xem để phân trang
            // Bạn có thể thêm bớt tùy ý --- dammio.com
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
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
            var links = (from l in db.Users
                         select l).OrderBy(x => x.ID);

            // 4. Tạo kích thước trang (pageSize), mặc định là 5.
            int pageSize = (size ?? 5);

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Link/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Link/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.UserGroups, "ID", "Name");
            return View();
        }

        // POST: Admin/Link/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,Password,GroupID,Name,Address,Email,Phone,ProvinceID,DistrictID,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.UserGroups, "ID", "Name", user.GroupID);
            return View(user);
        }

        // GET: Admin/Link/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.UserGroups, "ID", "Name", user.GroupID);
            return View(user);
        }

        // POST: Admin/Link/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password,GroupID,Name,Address,Email,Phone,ProvinceID,DistrictID,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.UserGroups, "ID", "Name", user.GroupID);
            return View(user);
        }

        // GET: Admin/Link/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
    }
}
