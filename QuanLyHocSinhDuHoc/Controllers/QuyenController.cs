using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class QuyenController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: List Quyền
        public ActionResult Index()
        {
            return View(db.QUYENs.ToList());
        }
        //GET: Thêm quyền
        public ActionResult Themmoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoi(QUYEN quyen)
        {           
            if (ModelState.IsValid)
            {
                db.QUYENs.Add(quyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        //GET: Cập nhật quyền

        public ActionResult SuaQuyen(int id)
        {
            return View(db.QUYENs.Find(id));
        }
        [HttpPost]
        public ActionResult SuaQuyen(QUYEN quyen)
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