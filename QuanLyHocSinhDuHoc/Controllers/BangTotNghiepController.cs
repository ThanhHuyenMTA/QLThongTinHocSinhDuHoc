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
        public ActionResult Themmoi()
        {
            return View();
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
    }
}