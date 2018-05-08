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
                    List<PHANQUYEN> listPQ = db.PHANQUYENs.Where(n => n.id_quyen == nv.id_Quyen).ToList();
                    QUYEN quyen = db.QUYENs.Find(nv.id_Quyen);
                    ModelQuyenNguoiDung QuyenNguoiDung = new ModelQuyenNguoiDung(nv,quyen);
                    Session["QuyenNguoiDung"] = QuyenNguoiDung;

                    Session["DangNhap"] = "OK";
                    Session["thongbaoDN"] = null;                   
                    Session["NguoiDung"] = nv.TenDangNhap;
                    Session["NguoiDungHT"] = nv;
                    listTb();
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
        public ActionResult DangXuat()
        {
            Session.RemoveAll();
            return RedirectToAction("Dangnhap", "DangNhap");
        }
        public void listTb()
        {
            List<TABLE_LOI> listVang = new List<TABLE_LOI>();
            List<TABLE_LOI> listDo = new List<TABLE_LOI>();
            List<TABLE_LOI> listXanh = new List<TABLE_LOI>();

            DateTime today = DateTime.Now;
            var listLoi = db.TABLE_LOI.ToList();
            foreach (var i in listLoi)
            {
                if (i.TimeEnd > today)
                {
                    TimeSpan a = ((DateTime)i.TimeEnd).Subtract(today);
                    double day = a.TotalDays;
                    if (day > 5)
                    {
                        i.TrangThai = "1"; //mức xanh
                        listXanh.Add(i);
                    }
                    else
                    {

                        i.TrangThai = "2";//mức vàng
                        listVang.Add(i);
                    }
                }
                else
                {
                    i.TrangThai = "3"; //mức đỏ
                    listDo.Add(i);
                }
            }
            Session["ThongBaoVang"] = listVang;
            Session["ThongBaoDo"] = listDo;
            Session["ThongBaoXanh"] = listXanh;
            db.SaveChanges();
        }
        
    }
}