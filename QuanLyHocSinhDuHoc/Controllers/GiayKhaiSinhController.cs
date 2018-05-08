using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;
using System.IO;
using QuanLyHocSinhDuHoc.CommonXuLy;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class GiayKhaiSinhController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: GiayKhaiSinh
        public ActionResult Themmoi(int? id_hs)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {

            Session["file"] = null;
            return View();
            }return RedirectToAction("Index", "Home");
        }
        public ActionResult ThemmoiR(int? id_hs)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {

                Session["file"] = null;
                Session["chuyenTab"] = 3;
                Session["id_HS"] = id_hs;
                return View(id_hs);
            } return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public JsonResult Themmoi(GIAYKHAISINH gks)
        {
            if(ModelState.IsValid)
            {
               if (Session["file"] != null)
                    gks.fileGKS = (string)Session["file"];
                db.GIAYKHAISINHs.Add(gks);
                db.SaveChanges();
                //Cập nhật lại bảng học sinh
                int id_HS = (int)Session["id_HS"];
                HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                hocsinh.id_GKS = gks.id;
                db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json("Thêm mới thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm mới thất bại", JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetailGKS()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {

                int id_hs = (int)Session["id_hsDetail"];
                HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
                if (hocsinh.id_GKS > 0)
                {
                    ViewBag.ThongbaoGKS = "OK";
                    GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(hocsinh.id_GKS);
                    return View(gks);
                }
                ViewBag.ThongbaoGKS = "NO";
                return View();
            } return RedirectToAction("Index", "Home");
        }
        public ActionResult SuaGKS( int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {

                GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(id);
                HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.id_GKS == id);
                ViewBag.id_hs = hs.id;
                Session["chuyenTab"] = 3;
                return View(gks);
            } return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SuaGKS(GIAYKHAISINH gks)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {

                if (ModelState.IsValid)
                {
                    HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.id_GKS == gks.id);
                    db.Entry(gks).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Session["chuyenTab"] = 3;
                    return RedirectToAction("DetailChung/" + hs.id, "HocSinh");

                }
                return View(gks);
            } return RedirectToAction("Index", "Home");
        }
        //upload File -> them file trong phan chi tiet -> được xử lý khi chưa có file
        [HttpPost]
        public JsonResult UpLoadFileGKS()
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
                if (Session["id_hsDetail"] != null)
                {
                    int id_hs = (int)Session["id_hsDetail"];
                    HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
                    if (hocsinh.id_GKS != null)
                    {
                        GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(hocsinh.id_GKS);
                        gks.fileGKS = fileName;
                        db.Entry(gks).State = System.Data.Entity.EntityState.Modified;
                        Session["chuyenTab"] = 3;
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