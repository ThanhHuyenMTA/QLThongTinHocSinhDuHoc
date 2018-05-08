using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using System.IO;
using PaymentSystem.Controllers;
using QuanLyHocSinhDuHoc.CommonXuLy;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class CMTController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: CMT
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
                Session["file"] = null;
                Session["chuyenTab"] = 2;
                Session["id_HS"] = id_hs;
                return View(id_hs);
            } return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult Themmoi1(CMT cmt1)
        {           
            if (ModelState.IsValid)
            {
                if (Session["file"] != null)
                    cmt1.fileCMT = (string)Session["file"];
                db.CMTs.Add(cmt1);
                db.SaveChanges();
                //Cập nhật lại bảng học sinh
                if (Session["id_HS"] != null)
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
        public JsonResult Themmoi2(CMT cmt2)
        {
            if (ModelState.IsValid)
            {
                if (Session["file"] != null)
                    cmt2.fileCMT = (string)Session["file"];
                db.CMTs.Add(cmt2);
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
        public JsonResult Themmoi3(CMT cmt3)
        {
            if (ModelState.IsValid)
            {
                if (Session["file"] != null)
                    cmt3.fileCMT =(string)Session["file"];
                db.CMTs.Add(cmt3);
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
        public ActionResult Detail()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                int id_hs = (int)Session["id_hsDetail"];
                HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
                CMT cmt = db.CMTs.SingleOrDefault(n => n.SoCMT == hocsinh.SoCMT);
                return View(cmt);
            } return RedirectToAction("Index", "Home");
        }
        public ActionResult SuaCMT(string soCMT,int loaiCMT)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                Session["file"] = null;
                CMT cmt = db.CMTs.Find(soCMT);
                ViewBag.LoaiCMT = loaiCMT;
                HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.SoCMT == soCMT);
                ViewBag.id_hs = hs.id;
                Session["chuyenTab"] = 2;
                return View(cmt);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SuaCMT(CMT cmt)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                if (ModelState.IsValid)
                {
                    if (Session["file"] != null)
                        cmt.fileCMT = (string)Session["file"];
                    HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.SoCMT == cmt.SoCMT);
                    db.Entry(cmt).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Session["chuyenTab"] = 2;
                    return RedirectToAction("DetailChung/" + hs.id, "HocSinh");

                }
                return View(cmt);
            } return RedirectToAction("Index", "Home");
        }
        //upload File -> them file trong phan chi tiet
        [HttpPost]
        public JsonResult UpLoadFileCMT()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var file = Request.Files["HelpSectionFile"];
                //lưu tên file
                var fileName = Path.GetFileName(file.FileName);
                //lưu đường dẫn
                var path = Path.Combine(Server.MapPath("~/Content/filePDF"), fileName);
                // file is uploaded
                var type = file.ContentType;
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "File đã tồn tại";
                }
                else
                {
                    if (type == "application/docx" || type == "application/pdf")
                        file.SaveAs(path);
                }
                Session["file"] = fileName;
                if(Session["id_hsDetail"] !=null)
                {
                    int id_hs = (int)Session["id_hsDetail"];
                    HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
                    if (hocsinh.SoCMT!=null)
                    {
                            CMT cmt = db.CMTs.Find(hocsinh.SoCMT);
                            cmt.fileCMT = fileName;
                            db.Entry(cmt).State = System.Data.Entity.EntityState.Modified;
                            Session["chuyenTab"] = 2;
                            db.SaveChanges();
                            return Json(hocsinh.id, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(fileName, JsonRequestBehavior.AllowGet);
            }
            Session["file"] = null;
            return Json("Khong", JsonRequestBehavior.AllowGet);
        }

    }
}