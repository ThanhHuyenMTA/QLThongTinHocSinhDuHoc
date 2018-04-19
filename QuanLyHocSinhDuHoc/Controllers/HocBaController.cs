using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class HocBaController : Controller
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: HocBa
        public ActionResult Themmoi()
        {
            return View();
        }
        public ActionResult ThemmoiR(int id_hs)
        {
            return View();
        }
        [HttpPost]
        public JsonResult Themmoi(HOCBA hocba)
        {
            if (ModelState.IsValid)
            {
                db.HOCBAs.Add(hocba);
                db.SaveChanges();
                //Cập nhật lại bảng học sinh
                int id_HS = (int)Session["id_HS"];
                HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                hocsinh.id_HB = hocba.id;
                db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //dùng để khi tiến hành thêm mới năm học
                Session["id_hocba"] = hocba.id;
                return Json("Thêm mới thành công", JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm mới thất bại", JsonRequestBehavior.AllowGet);
        }
    }
}