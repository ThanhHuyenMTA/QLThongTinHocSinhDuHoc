using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class GiayKhaiSinhController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: GiayKhaiSinh
        public ActionResult Themmoi(int? id_hs)
        {
            return View();
        }
        public ActionResult ThemmoiR(int? id_hs)
        {
            Session["chuyenTab"] = 3;
            Session["id_HS"] = id_hs;
            return View(id_hs);
        }
        [HttpPost]
        public JsonResult Themmoi(GIAYKHAISINH gks)
        {
            if(ModelState.IsValid)
            {
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
            int id_hs = (int)Session["id_hsDetail"];
            HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
            if(hocsinh.id_GKS >0)
            {
                ViewBag.ThongbaoGKS = "OK";
                GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(hocsinh.id_GKS);
                return View(gks);
            }
            ViewBag.ThongbaoGKS = "NO";
            return View();
        }
        public ActionResult SuaGKS( int id)
        {
            GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(id);
            HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.id_GKS == id);
            ViewBag.id_hs = hs.id;
            Session["chuyenTab"] = 3;
            return View(gks);
        }
        [HttpPost]
        public ActionResult SuaGKS(GIAYKHAISINH gks)
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
        }
    }
}