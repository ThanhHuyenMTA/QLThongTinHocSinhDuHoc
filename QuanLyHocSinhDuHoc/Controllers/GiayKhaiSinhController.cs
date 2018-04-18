using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class GiayKhaiSinhController : Controller
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: GiayKhaiSinh
        public ActionResult Themmoi()
        {
            return View();
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
                GIAYKHAISINH gks = db.GIAYKHAISINHs.SingleOrDefault(n => n.id == hocsinh.id_GKS);
                return View(gks);
            }
            ViewBag.ThongbaoGKS = "NO";
            return View();
        }
    }
}