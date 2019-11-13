using dbmsQLNS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dbmsQLNS.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        QuanLyNhanSuEntities db = new QuanLyNhanSuEntities();
        // GET: Admin/UserAdmin
        public ActionResult Index()
        {
            //var list = db.Database.SqlQuery<NhanVien>("Sp_NhanViens_ShowList").ToList();
            //return View(list);
            var user = db.NhanViens.Where(x => x.MaNhanVien != "admin" && x.TrangThai == true).ToList();
            return View(user);
        }

        public ActionResult Create()
        {
            ViewBag.MaChucVuNV = new SelectList(db.ChucVuNhanViens, "MaChucVuNV", "TenChucVu");
            ViewBag.MaPhongBan = new SelectList(db.PhongBans, "MaPhongBan", "TenPhongBan");
            ViewBag.MaChuyenNganh = new SelectList(db.ChuyenNganhs, "MaChuyenNganh", "TenChuyenNganh");
            ViewBag.MaTrinhDoHocVan = new SelectList(db.TrinhDoHocVans, "MaTrinhDoHocVan", "TenTrinhDo");
            return View();
        }
        [HttpPost]
        public ActionResult Create(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                object[] sqlParams =
                {
                new SqlParameter ("@manv", nhanVien.MaNhanVien)

                };
                var res = db.Database.SqlQuery<bool>("Sp_CheckMaNV @manv", sqlParams).SingleOrDefault();
                if (res)
                {
                    ViewBag.err = "Mã nhân viên đã bị trùng";
                    //ModelState.AddModelError("MaNhanVien", "Mã tài khoản đã tồn tại");
                    ViewBag.MaChucVuNV = new SelectList(db.ChucVuNhanViens, "MaChucVuNV", "TenChucVu", nhanVien.MaChucVuNV);
                    ViewBag.MaPhongBan = new SelectList(db.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
                    ViewBag.MaChuyenNganh = new SelectList(db.ChuyenNganhs, "MaChuyenNganh", "TenChuyenNganh", nhanVien.ChuyenNganh);
                    ViewBag.MaTrinhDoHocVan = new SelectList(db.TrinhDoHocVans, "MaTrinhDoHocVan", "TenTrinhDo", nhanVien.TrinhDoHocVan);
                    return View("Create");
                }
                else
                {
                    db.Sp_NhanViens_Insert(nhanVien.MaNhanVien,
                    nhanVien.MatKhau,
                    nhanVien.HoTen,
                    nhanVien.NgaySinh,
                    nhanVien.QueQuan,
                    nhanVien.HinhAnh,
                    nhanVien.GioiTinh,
                    nhanVien.DanToc,
                    nhanVien.sdt_NhanVien,
                    nhanVien.MaChucVuNV,
                    true,
                    nhanVien.MaPhongBan,
                    nhanVien.MaHopDong,
                    nhanVien.MaChuyenNganh,
                    nhanVien.MaTrinhDoHocVan,
                    nhanVien.CMND
                    );
                }

                return RedirectToAction("Index");
            }
            ViewBag.MaChucVuNV = new SelectList(db.ChucVuNhanViens, "MaChucVuNV", "TenChucVu", nhanVien.MaChucVuNV);
            ViewBag.MaPhongBan = new SelectList(db.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            ViewBag.MaChuyenNganh = new SelectList(db.ChuyenNganhs, "MaChuyenNganh", "TenChuyenNganh", nhanVien.ChuyenNganh);
            ViewBag.MaTrinhDoHocVan = new SelectList(db.TrinhDoHocVans, "MaTrinhDoHocVan", "TenTrinhDo", nhanVien.TrinhDoHocVan);
            return View(nhanVien);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien link = db.NhanViens.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: /Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien link = db.NhanViens.Find(id);
            db.NhanViens.Remove(link);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(string id)
        //{
        //    var a = db.NhanViens.Where(x => x.MaNhanVien == id).SingleOrDefault();

        //    db.NhanViens.Remove(a);

        //    db.SaveChanges();
        //    return Redirect("~/admin/UserAdmin");
        //}
        public ActionResult UpdateUser(String id)
        {
            var user = db.NhanViens.Where(n => n.MaNhanVien == id).FirstOrDefault();
            UserValidate userVal = new UserValidate();

            userVal.MaNhanVien = user.MaNhanVien;
            userVal.HoTen = user.HoTen;
            userVal.MatKhau = user.MatKhau;
            userVal.GioiTinh = user.GioiTinh;

            userVal.MaChucVuNV = user.MaChucVuNV;
            userVal.QueQuan = user.QueQuan;
            userVal.HinhAnh = user.HinhAnh;
            userVal.DanToc = user.DanToc;
            userVal.sdt_NhanVien = user.sdt_NhanVien;
            userVal.MaHopDong = user.MaHopDong;

            userVal.NgaySinh = user.NgaySinh;
            userVal.TrangThai = user.TrangThai;
            userVal.MaChuyenNganh = user.MaChuyenNganh;
            userVal.MaTrinhDoHocVan = user.MaTrinhDoHocVan;
            userVal.MaPhongBan = user.MaPhongBan;

            userVal.CMND = user.CMND;
            userVal.XacNhanMatKhau = user.MatKhau;

            return View(userVal);
            //  return View(user);
        }
        [HttpPost]
        public ActionResult UpdateUser(UserValidate upUser)
        {
            upUser.XacNhanMatKhau = upUser.MatKhau;
            var us = db.NhanViens.Where(n => n.MaNhanVien == upUser.MaNhanVien).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //var us = db.NhanViens.Where(n => n.MaNhanVien == upUser.MaNhanVien).FirstOrDefault();
                if (us != null)
                {

                    CapNhatTrinhDoHocVan capNhat = new CapNhatTrinhDoHocVan();
                    capNhat.MaNhanVien = upUser.MaNhanVien;
                    capNhat.NgayCapNhat = DateTime.Now.Date;
                    capNhat.MaTrinhDoTruoc = us.MaTrinhDoHocVan;
                    capNhat.MaTrinhDoCapNhat = upUser.MaTrinhDoHocVan;

                    us.MaNhanVien = upUser.MaNhanVien;
                    us.HoTen = upUser.HoTen;
                    us.MatKhau = upUser.MatKhau;
                    us.GioiTinh = upUser.GioiTinh;

                    us.MaChucVuNV = upUser.MaChucVuNV;
                    us.QueQuan = upUser.QueQuan;
                    us.HinhAnh = upUser.HinhAnh;
                    us.DanToc = upUser.DanToc;
                    us.sdt_NhanVien = upUser.sdt_NhanVien;
                    us.MaHopDong = upUser.MaHopDong;

                    us.NgaySinh = upUser.NgaySinh;
                    us.TrangThai = upUser.TrangThai;
                    us.MaChuyenNganh = upUser.MaChuyenNganh;
                    us.MaTrinhDoHocVan = upUser.MaTrinhDoHocVan;
                    us.MaPhongBan = upUser.MaPhongBan;
                    us.CMND = upUser.CMND;

                    var trinhdo = db.TrinhDoHocVans.Where(n => n.MaTrinhDoHocVan.Equals(us.MaTrinhDoHocVan)).FirstOrDefault();

                    var luong = db.Luongs.Where(n => n.MaNhanVien.Equals(us.MaNhanVien)).FirstOrDefault();

                    if (trinhdo.HeSoBac != null)
                    {
                        luong.HeSoLuong = luong.HeSoLuong < (double)trinhdo.HeSoBac ? (double)trinhdo.HeSoBac : luong.HeSoLuong;
                    }
                    else
                    { luong.HeSoLuong = 1; }



                    db.CapNhatTrinhDoHocVans.Add(capNhat);

                    db.SaveChanges();
                    return Redirect("/admin/UserAdmin");

                }
            }
            return View(upUser);

        }//end update
        public ActionResult ExportExcel()
        {

            var ds = db.NhanViens.Where(n => n.MaNhanVien != "admin" && n.MaHopDong != null).ToList();
            var phong = db.PhongBans.ToList();
            var gv = new GridView();
            //===================================================
            DataTable dt = new DataTable();
            //Add Datacolumn
            DataColumn workCol = dt.Columns.Add("Họ tên", typeof(String));

            dt.Columns.Add("Phòng ban", typeof(String));
            dt.Columns.Add("Chức vụ", typeof(String));
            dt.Columns.Add("Học vấn", typeof(String));
            dt.Columns.Add("Chuyên ngành", typeof(String));

            //Add in the datarow


            foreach (var item in ds)
            {
                DataRow newRow = dt.NewRow();
                newRow["Họ tên"] = item.HoTen;
                newRow["Phòng ban"] = item.PhongBan.TenPhongBan;
                newRow["Chức vụ"] = item.ChucVuNhanVien.TenChucVu;
                newRow["Học vấn"] = item.TrinhDoHocVan.TenTrinhDo;
                newRow["Chuyên ngành"] = item.ChuyenNganh.TenChuyenNganh;

                dt.Rows.Add(newRow);
            }

            //====================================================
            gv.DataSource = dt;
            // gv.DataSource = ds;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment; filename=danh-sach.xls");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return Redirect("/admin/UserAdmin");
        }
    }
}