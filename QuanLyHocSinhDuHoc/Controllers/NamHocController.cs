using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class NamHocController : Controller
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: NamHoc
        public ActionResult Themmoi()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ThemNamhoc(NAMHOC namhoc)
        {
            if(ModelState.IsValid)
            {
                namhoc.id_HB = (int)Session["id_hocba"];
                db.NAMHOCs.Add(namhoc);
                db.SaveChanges();
                return Json(namhoc, JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm mới thất bại", JsonRequestBehavior.AllowGet);
        }
    }
}