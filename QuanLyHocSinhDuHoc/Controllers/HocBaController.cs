﻿using System;
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
        public ActionResult Themmoi(int? id_hs)
        {
            return View();
        }
        public ActionResult ThemmoiR(int? id_hs)
        {
            Session["chuyenTab"] = 5;
            Session["id_HS"] = id_hs;
            return View(id_hs);
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

        public ActionResult DetailHB()
        {
            int id_hs = (int)Session["id_hsDetail"];
            HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
            if (hocsinh.id_HB > 0)
            {
                ViewBag.ThongbaoHB = "OK";
                HOCBA hb = db.HOCBAs.Find(hocsinh.id_HB);
                return View(hb);
            }
            ViewBag.ThongbaoHB = "NO";
            return View();
        }
        public ActionResult SuaHB(int id)
        {
            HOCBA hb = db.HOCBAs.Find(id);
            return View(hb);
        }
        [HttpPost]
        public ActionResult SuaHB(HOCBA hb)
        {
            if (ModelState.IsValid)
            {
                HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.id_HB == hb.id);
                db.Entry(hb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["chuyenTab"] = 5;
                return RedirectToAction("DetailChung/" + hs.id, "HocSinh");
            }
            return View(hb);
        }
    }
}