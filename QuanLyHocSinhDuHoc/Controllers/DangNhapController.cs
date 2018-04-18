using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        public ActionResult Dangnhap()
        {
            Session["thongbaoDN"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(string TenTaiKhoan,string MatKhau)          
        {
            if (TenTaiKhoan == "Admin" && MatKhau == "Admin")
            {
                Session["thongbaoDN"] = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["thongbaoDN"] = "Đăng nhập thất bại";
                return View();
               
            }         
        }
    }
}