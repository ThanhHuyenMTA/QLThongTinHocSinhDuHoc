using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;
using QuanLyHocSinhDuHoc.CommonXuLy;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class QuyenController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: List Quyền
        public ActionResult Index()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                return View(db.QUYENs.ToList());
            } return RedirectToAction("Index", "Home");
        }
        //GET: Thêm quyền
        public ActionResult Themmoi()
        {
             ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
             if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
             {
                 return View();
             } return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Themmoi(QUYEN quyen)
        {    
             ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
             if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
             {
                 if (ModelState.IsValid)
                 {
                     db.QUYENs.Add(quyen);
                     db.SaveChanges();
                     return RedirectToAction("Index");
                 }
                 return View();
             } return RedirectToAction("Index", "Home");
        }
        //GET: Cập nhật quyền

        public ActionResult SuaQuyen(int id)
        {
             ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
             if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
             {
                 return View(db.QUYENs.Find(id));
             } return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SuaQuyen(QUYEN quyen)
        {
             ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
             if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
             {
                 if (ModelState.IsValid)
                 {
                     QUYEN quyenUpdate = db.QUYENs.Find(quyen.Id);
                     quyenUpdate.Ten = quyen.Ten;
                     quyenUpdate.MoTa = quyen.MoTa;
                     db.Entry(quyenUpdate).State = System.Data.Entity.EntityState.Modified;
                     db.SaveChanges();
                     return RedirectToAction("Index");
                 }
                 return View();
             } return RedirectToAction("Index", "Home");
        }

        //GET: Xóa quyền
        [HttpPost]
        public JsonResult XoaQuyen(string id)
        {
            if (ModelState.IsValid)
            {
                QUYEN quyen = db.QUYENs.Find(Convert.ToInt32(id));
                db.QUYENs.Remove(quyen);
                db.SaveChanges();
                return Json("Yes", JsonRequestBehavior.AllowGet);
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }
    }
}