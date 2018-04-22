using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class BangTotNghiepController : Controller
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: BangTotNghiep
        public ActionResult Themmoi(int? id_hs)
        {
           
            return View();
        }
        public ActionResult ThemmoiR(int? id_hs)
        {
            Session["chuyenTab"] = 4;
            Session["id_HS"] = id_hs;
            return View(id_hs);
        }
        [HttpPost]
        public JsonResult Themmoi(BANGTOTNGHIEP btn)
        {
            if (ModelState.IsValid)
            {
                db.BANGTOTNGHIEPs.Add(btn);
                db.SaveChanges();
                //Cập nhật lại bảng học sinh
                int id_HS = (int)Session["id_HS"];
                HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                hocsinh.id_BTN = btn.id;
                db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json("Thêm mới thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm mới thất bại", JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetailBTN()
        {
            int id_hs = (int)Session["id_hsDetail"];
            HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
            if (hocsinh.id_BTN > 0)
            {
                ViewBag.ThongbaoBTN = "OK";
                BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.SingleOrDefault(n => n.id == hocsinh.id_BTN);
                return View(btn);
            }
            ViewBag.ThongbaoBTN = "NO";
            return View();
        }

        public ActionResult SuaBTN(int id)
        {
            BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.Find(id);
            HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.id_BTN == btn.id);
            ViewBag.id_hs = hs.id;
            Session["chuyenTab"] = 4;
            return View(btn);
        }
        [HttpPost]
        public ActionResult SuaBTN(BANGTOTNGHIEP btn)
        {

            if (ModelState.IsValid)
            {
                HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.id_BTN == btn.id);
                db.Entry(btn).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["chuyenTab"] = 4;
                return RedirectToAction("DetailChung/" + hs.id, "HocSinh");

            }
            return View(btn);
        }



    }
}