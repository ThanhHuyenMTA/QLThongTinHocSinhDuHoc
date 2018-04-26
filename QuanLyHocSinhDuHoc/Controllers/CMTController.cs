﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using System.IO;
using PaymentSystem.Controllers;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class CMTController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: CMT
        public ActionResult Themmoi(int? id_hs)
        {
            Session["file"] = null;
            return View();
        }
        public ActionResult ThemmoiR(int? id_hs)
        {
            Session["file"] = null;
            Session["chuyenTab"] = 2;
            Session["id_HS"] = id_hs;
            return View(id_hs);
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
                int id_HS = (int)Session["id_HS"];
                HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                hocsinh.SoCMT = cmt1.SoCMT;
                db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
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
                 int id_HS = (int)Session["id_HS"];
                 HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                 hocsinh.SoCMT = cmt2.SoCMT;
                 db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                 db.SaveChanges();
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
                 int id_HS = (int)Session["id_HS"];
                 HOCSINH hocsinh = db.HOCSINHs.Find(id_HS);
                 hocsinh.SoCMT = cmt3.SoCMT;
                 db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
                 db.SaveChanges();
                return Json("Thêm thành công!", JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm thất bại!", JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Loai1()
        {
            return View();
        }
        public ActionResult Loai2()
        {
            return View();
        }
        public ActionResult Loai3()
        {
            return View();
        }
        public ActionResult Detail()
        {
            int id_hs = (int)Session["id_hsDetail"];
            HOCSINH hocsinh = db.HOCSINHs.Find(id_hs);
            CMT cmt = db.CMTs.SingleOrDefault(n => n.SoCMT == hocsinh.SoCMT);
            return View(cmt);
        }
        public ActionResult SuaCMT(string soCMT,int loaiCMT)
        {
            Session["file"] = null;
            CMT cmt = db.CMTs.Find(soCMT);
            ViewBag.LoaiCMT = loaiCMT;
            HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.SoCMT == soCMT);
            ViewBag.id_hs = hs.id;
            Session["chuyenTab"] = 2;
            return View(cmt);
        }
        [HttpPost]
        public ActionResult SuaCMT(CMT cmt)
        {
            
            if (ModelState.IsValid)
            {
                if (Session["file"] != null)
                    cmt.fileCMT = (string)Session["file"];
                HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.SoCMT == cmt.SoCMT);
                db.Entry(cmt).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["chuyenTab"] = 2;
                return RedirectToAction("DetailChung/" + hs.id,"HocSinh");

            }
            return View(cmt);
        }
    }
}