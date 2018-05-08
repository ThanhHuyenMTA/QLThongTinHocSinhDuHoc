using PaymentSystem.Controllers;
using QuanLyHocSinhDuHoc.CommonXuLy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class NguoiGiamHoController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: NguoiGiamHo
        public ActionResult Themmoi(int? id_hs)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                Session["file"] = null;
                return View();
            } return RedirectToAction("Index", "Home");
        }
        public ActionResult ThemmoiR(int? id_hs)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                Session["id_HS"] = id_hs;
                return View();
            } return RedirectToAction("Index", "Home");
        }
        public ActionResult Loai1()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                return View();
            } return RedirectToAction("Index", "Home");
        }
        public ActionResult Loai2()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                return View();
            } return RedirectToAction("Index", "Home");
        }
        public ActionResult Loai3()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                return View();
            } return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult Themmoi1(NGUOIGIAMHO cmt1)
        {
            if (ModelState.IsValid)
            {
                if (Session["file"] != null)
                    cmt1.file_CMTNGH = (string)Session["file"];
                db.NGUOIGIAMHOes.Add(cmt1);
                db.SaveChanges();
                //Cập nhật lại bảng học sinh
                if(Session["id_HS"]!=null)
                {
                    int id_HS = (int)Session["id_HS"];
                    HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                    hocsinh.id_NgGiamHo = cmt1.SoCMT;
                    db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                
                return Json("Thêm thành công!", JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm thất bại!", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Themmoi2(NGUOIGIAMHO cmt2)
        {
            if (ModelState.IsValid)
            {
                if (Session["file"] != null)
                    cmt2.file_CMTNGH = (string)Session["file"];
                db.NGUOIGIAMHOes.Add(cmt2);
                db.SaveChanges();
                //Cập nhật lại bảng học sinh
                if (Session["id_HS"] != null)
                {
                    int id_HS = (int)Session["id_HS"];
                    HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                    hocsinh.id_NgGiamHo = cmt2.SoCMT;
                    db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Json("Thêm thành công!", JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm thất bại!", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Themmoi3(NGUOIGIAMHO cmt3)
        {
            if (ModelState.IsValid)
            {
                if (Session["file"] != null)
                    cmt3.file_CMTNGH = (string)Session["file"];
                db.NGUOIGIAMHOes.Add(cmt3);
                db.SaveChanges();
                //Cập nhật lại bảng học sinh
                if (Session["id_HS"] != null)
                {
                    int id_HS = (int)Session["id_HS"];
                    HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                    hocsinh.id_NgGiamHo = cmt3.SoCMT;
                    db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Json("Thêm thành công!", JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm thất bại!", JsonRequestBehavior.AllowGet);
        }
    }
}