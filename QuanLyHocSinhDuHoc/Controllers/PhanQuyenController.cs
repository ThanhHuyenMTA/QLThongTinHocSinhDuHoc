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
    public class PhanQuyenController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: PhanQuyen
        public ActionResult Index()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                return View(db.PHANQUYENs.ToList());

            } return RedirectToAction("Index", "Home");
        }
        //GET: Thêm 
        public ActionResult Themmoi()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                ViewBag.Quyen = db.QUYENs.ToList();
                ViewBag.QuyenTC = db.QUYENTRUYCAPs.ToList();
                return View();
            } return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Themmoi(PHANQUYEN Pquyen)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.PHANQUYENs.Add(Pquyen);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            } return RedirectToAction("Index", "Home");
        }
        //GET: Check kiểm tra trùng
        [HttpPost]
        public JsonResult CheckPhanQuyen(string id_quyen, string id_truycap)
        {
             PHANQUYEN phanquyen = db.PHANQUYENs.Find(Convert.ToInt32(id_quyen), Convert.ToInt32(id_truycap));                
             if(phanquyen==null)
                return Json("No", JsonRequestBehavior.AllowGet);
             else return Json("Yes", JsonRequestBehavior.AllowGet);
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



        ////GET: Cập nhật
        //public ActionResult SuaPhanQuyen(int id_quyen, int id_truycap)
        //{
        //    ViewBag.Quyen = db.QUYENs.ToList();
        //    ViewBag.QuyenTC = db.QUYENTRUYCAPs.ToList();
        //    return View(db.PHANQUYENs.Find(id_quyen, id_truycap));
        //}
        //[HttpPost]
        //public ActionResult SuaPhanQuyen(PHANQUYEN phanquyen)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int id_quyen = Convert.ToInt32(Request["id_quyen"]);
        //        int id_truycap = Convert.ToInt32(Request["id_truycap"]);
        //        PHANQUYEN phanquyenOld = db.PHANQUYENs.Find(id_quyen, id_truycap);
        //        PHANQUYEN phanquyenUpdate = db.PHANQUYENs.Find(phanquyen.id_quyen, phanquyen.id_truycap);
        //        if (phanquyenUpdate == null)
        //        {
        //            phanquyenUpdate.id_quyen = phanquyen.id_quyen;
        //            phanquyenUpdate.id_truycap = phanquyen.id_truycap;
        //            phanquyenUpdate.MoTa = phanquyen.MoTa;
        //            db.Entry(phanquyenUpdate).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        return View();
        //    }
        //    return View();
        //}

    }
}