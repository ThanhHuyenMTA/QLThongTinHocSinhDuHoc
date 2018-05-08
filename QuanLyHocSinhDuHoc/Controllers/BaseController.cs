using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using QuanLyHocSinhDuHoc.CommonXuLy;
namespace PaymentSystem.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            //List<String> ListQ = Session["ListLinkQuyen"] as List<String>;
            string url = context.HttpContext.Request.RawUrl;
            string result =(string)Session["DangNhap"];
            if (result =="NO")
            {
                context.Result = RedirectToAction("Dangnhap", "DangNhap");
                return;
            }
            if (result == "OK") return;
            context.Result = RedirectToAction("Dangnhap", "DangNhap");
            return;          
        }
    }
}