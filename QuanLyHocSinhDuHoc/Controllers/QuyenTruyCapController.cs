using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class QuyenTruyCapController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: QuyenTruyCap
        public ActionResult Index()
        {
            return View(db.QUYENTRUYCAPs.ToList());
        }
        //GET: Thêm mới quyền truy cập
        public ActionResult Themmoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoi(QUYENTRUYCAP quyenTC)
        {
            if (ModelState.IsValid)
            {
                db.QUYENTRUYCAPs.Add(quyenTC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        //GET: Cập nhật quyền truy cập
        public ActionResult SuaQuyenTruyCap(int id)
        {
            return View(db.QUYENTRUYCAPs.Find(id));
        }
        [HttpPost]
        public ActionResult SuaQuyenTruyCap(QUYENTRUYCAP quyenTC)
        {
            if (ModelState.IsValid)
            {
                QUYENTRUYCAP quyenTCUpdate = db.QUYENTRUYCAPs.Find(quyenTC.id);
                quyenTCUpdate.Ten = quyenTC.Ten;
                quyenTCUpdate.LinkTruy_Cap = quyenTC.LinkTruy_Cap;
                db.Entry(quyenTCUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET: Xóa quyền truy cập

        [HttpPost]
        public JsonResult XoaQuyenTruyCap(string id)
        {
            if (ModelState.IsValid)
            {
                QUYENTRUYCAP quyenTC = db.QUYENTRUYCAPs.Find(Convert.ToInt32(id));
                db.QUYENTRUYCAPs.Remove(quyenTC);
                db.SaveChanges();
                return Json("Yes", JsonRequestBehavior.AllowGet);
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }
    }
}