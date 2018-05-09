using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class PhanQuyenController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: PhanQuyen
        public ActionResult Index()
        {
            return View(db.PHANQUYENs.ToList());
        }
        //GET: Thêm 
        public ActionResult Themmoi()
        {
            ViewBag.Quyen = db.QUYENs.ToList();
            ViewBag.QuyenTC = db.QUYENTRUYCAPs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Themmoi(PHANQUYEN Pquyen)
        {
            if (ModelState.IsValid)
            {
                db.PHANQUYENs.Add(Pquyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET: Cập nhật
        public ActionResult SuaPhanQuyen(int id_quyen, int id_truycap)
        {
            ViewBag.Quyen = db.QUYENs.ToList();
            ViewBag.QuyenTC = db.QUYENTRUYCAPs.ToList();
            return View(db.PHANQUYENs.Find(id_quyen, id_truycap));
        }
        [HttpPost]
        public ActionResult SuaPhanQuyen(PHANQUYEN phanquyen)
        {
            if (ModelState.IsValid)
            {
                //PHANQUYEN phanquyenUpdate = db.PHANQUYENs.Find(phanquyen.id_quyen, phanquyen.id_truycap);               
                //phanquyenUpdate.id_quyen = phanquyen.id_quyen;
                //phanquyenUpdate.id_truycap = phanquyen.id_truycap;
                //phanquyenUpdate.MoTa = phanquyen.MoTa;
                //db.Entry(phanquyenUpdate).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET: Xóa
        [HttpPost]
        public JsonResult XoaPhanQuyen(string id_quyen, string id_quyenTC)
        {
            if (ModelState.IsValid)
            {
                PHANQUYEN phanquyen = db.PHANQUYENs.Find(Convert.ToInt32(id_quyen), Convert.ToInt32(id_quyenTC));
                db.PHANQUYENs.Remove(phanquyen);
                db.SaveChanges();
                return Json("Yes", JsonRequestBehavior.AllowGet);
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }
    }
}