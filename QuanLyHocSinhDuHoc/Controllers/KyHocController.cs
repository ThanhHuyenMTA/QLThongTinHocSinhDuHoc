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
    public class KyHocController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: KyHoc
        //Thông tin kì học (Nhập điểm)
        [HttpGet]
        public JsonResult ViewNhapDiem(int id) //id: id_namhoc tuong ung
        {
            Session["id_namhoc"] = id;
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult NhapDiem(KIHOC kihoc)
        {
            if (ModelState.IsValid)
            {
                kihoc.id_NAMHOC = (int)Session["id_namhoc"];
                db.KIHOCs.Add(kihoc);
                db.SaveChanges();
                return Json(kihoc, JsonRequestBehavior.AllowGet);
            }
            return Json(kihoc, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ThemmoiDiem(int id_hb)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                var model = db.NAMHOCs.Where(n => n.id_HB == id_hb).ToList();
                if (model.Count > 0) return View(model);
                else return RedirectToAction("ThemmoiR", "NamHoc", id_hb);

            } return RedirectToAction("Index", "Home");
        }
    }
}