using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class SoHoKhauController : Controller
    {
        // GET: SoHoKhau
        public ActionResult Index()
        {
            return View();
        }
        #region //GET:Thêm mới
        public ActionResult Themmoi()
        {
            return View();
        }
        public ActionResult Themmoi()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //GET:Cập nhật-sửa
        public ActionResult Sua(int id)
        {
            //
            return View();
        }
        public ActionResult Sua()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}