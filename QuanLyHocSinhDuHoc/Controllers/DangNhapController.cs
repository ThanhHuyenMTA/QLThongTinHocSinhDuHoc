using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using QuanLyHocSinhDuHoc.CommonXuLy;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class DangNhapController : Controller
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: DangNhap
        public ActionResult Dangnhap()
        {
            Session["thongbaoDN"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(string tenDangNhap,string matKhau)          
        {
            Xuly xuly = new Xuly();
            string matKhauNew = xuly.chuoiMaHoa(matKhau);
            NHANVIEN Listnv = db.NHANVIENs.SingleOrDefault(n => n.TenDangNhap == tenDangNhap);
            NHANVIEN nv = Listnv != null ? Listnv : null;

            if (nv !=null)
            {
                if(nv.MatKhau ==matKhauNew)
                {
                    Session["DangNhap"] = "OK";
                    Session["thongbaoDN"] = null;
                    int id_nv = nv.id;
                    List<PHANQUYEN> listPQ = db.PHANQUYENs.Where(n => n.id_nv == id_nv).ToList();
                    List<string> listQuyenTC = new List<string>();
                    foreach (var item in listPQ)
                    {
                        QUYENTRUYCAP quyenTruycap = db.QUYENTRUYCAPs.Find(item.id_quyen);//luu quyen khac
                        listQuyenTC.Add(quyenTruycap.LinkTruy_Cap);
                    }
                    Session["ListLinkQuyen"] = listQuyenTC;
                    return RedirectToAction("Index", "Home");
                }
                Session["DangNhap"] = "NO";
                Session["thongbaoDN"] = "Đăng nhập thất bại";
                return View();
                
            }
            else
            {
                Session["DangNhap"] = "NO";
                Session["thongbaoDN"] = "Đăng nhập thất bại";
                return View();
               
            }         
        }
    }
}