using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class NguoiGiamHoController : Controller
    {
        // GET: NguoiGiamHo
        public ActionResult Themmoi(int? id_hs)
        {
            return View();
        }
        public ActionResult ThemmoiR(int? id_hs)
        {
            Session["id_HS"] = id_hs;
            return View();
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
    }
}