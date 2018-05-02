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
    public class NguoiDungController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {           
            return View(db.NHANVIENs.ToList());
        }
        public ActionResult Themmoi()
        {
            return View(db.QUYENs.ToList());
        }
        [HttpPost]
        public ActionResult Themmoi(NHANVIEN nhanvien)
        {
            Xuly xuly = new Xuly();
            nhanvien.MatKhau = xuly.chuoiMaHoa(nhanvien.MatKhau);
            if(ModelState.IsValid)
            {
                db.NHANVIENs.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult SuaNguoiDung(int id)
        {
            ViewBag.listQuyen = db.QUYENs.ToList();
            return View(db.NHANVIENs.Find(id));
        }
        [HttpPost]
        public ActionResult SuaNguoiDung(NHANVIEN nhanvien)
        {           
            if (ModelState.IsValid)
            {
                NHANVIEN nhanvienUpdate = db.NHANVIENs.Find(nhanvien.id);
                nhanvienUpdate.HoTen = nhanvien.HoTen;
                nhanvienUpdate.DiaChi = nhanvien.DiaChi;
                nhanvienUpdate.SoDT = nhanvien.SoDT;
                nhanvienUpdate.id_Quyen = nhanvien.id_Quyen;
                nhanvienUpdate.TenDangNhap = nhanvien.TenDangNhap;
                db.Entry(nhanvienUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public JsonResult XoaNguoiDung(string id)
        {
            if (ModelState.IsValid)
            {
                NHANVIEN nhanvien = db.NHANVIENs.Find(Convert.ToInt32(id));
                db.NHANVIENs.Remove(nhanvien);
                db.SaveChanges();
                return Json("Yes", JsonRequestBehavior.AllowGet);
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }
    }
}