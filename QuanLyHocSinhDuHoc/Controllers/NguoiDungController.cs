using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;
using QuanLyHocSinhDuHoc.CommonXuLy;
using System.Data.SqlClient;
namespace QuanLyHocSinhDuHoc.Controllers
{
    public class NguoiDungController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        // GET: NguoiDung
        public ActionResult Index(int? page)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                int count = db.NHANVIENs.ToList().Count;
                Session["chiasotrang"] = count % 10 == 0 ? count / 10 : count / 10 + 1;
                page = page ?? 1;
                int lineStart = (int)(page - 1) * 10; //dòng bắt đầu
                int soBanGhi = 10; //số bản ghi cần hiện thị mỗi trang
                Session["trangdangload"] = page;

                var idParam1 = new SqlParameter
                {
                    ParameterName = "LineStart",
                    Value = lineStart
                };
                var idParam2 = new SqlParameter
                {
                    ParameterName = "soBanGhi",
                    Value = soBanGhi
                };
                var list = db.Database.SqlQuery<NHANVIEN>("exec PhanTrangNguoiDung @LineStart,@soBanGhi ", idParam1, idParam2).ToList<NHANVIEN>();
                return View(list);
            }
            return RedirectToAction("Index", "Home");
        }
        //xử lý phân trang ve cuoi cung     
        public JsonResult XulyPhanTrangVeCuoicung()
        {
            int trangcuoi = (int)Session["chiasotrang"];
            return Json(trangcuoi, JsonRequestBehavior.AllowGet);
        }
        #region //GET: Them moi nguoi dung
        public ActionResult Themmoi()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                return View(db.QUYENs.ToList());
            } return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Themmoi(NHANVIEN nhanvien)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                NHANVIEN nguoiDung = Session["NguoiDungHT"] as NHANVIEN;
                if (nguoiDung != null)
                {
                    if (ModelState.IsValid)
                    {
                        nhanvien.NguoiTao = nguoiDung.id;
                        nhanvien.NgayTao = DateTime.Now;
                        Senmail senmail = new Senmail();
                        senmail.SendEmail("thanhhuyen010695@gmail.com", "Mật khẩu vào hệ thống của bạn \n là " + nhanvien.TenDangNhap);
                        Xuly xuly = new Xuly();
                        nhanvien.MatKhau = xuly.chuoiMaHoa(nhanvien.TenDangNhap);
                        db.NHANVIENs.Add(nhanvien);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    return View();
                }
                return View();
            } return RedirectToAction("Index", "Home");
        }        
        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            bool i=false;
            foreach(var item in db.NHANVIENs.ToList())
            {
                if(item.Email!=null)
                {
                    i = string.Equals(item.Email,email, StringComparison.OrdinalIgnoreCase);
                    if (i == true)
                        return Json("Yes", JsonRequestBehavior.AllowGet); //truong hop da ton tai                     
                }
            }
            return Json("No", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult CheckTenDN(string tenDN)
        {
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(n => n.TenDangNhap == tenDN);
             if(nhanvien !=null)
             {
                 return Json("Yes", JsonRequestBehavior.AllowGet); //truong hop da ton tai
             }else
                 return Json("No", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult CheckSoDT(string SoDT)
        {
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(n => n.SoDT == SoDT);
             if(nhanvien !=null)
             {
                 return Json("Yes", JsonRequestBehavior.AllowGet); //truong hop da ton tai
             }else
                 return Json("No", JsonRequestBehavior.AllowGet);

        }       
        #endregion

        #region //GET: Cap nhat nguoi dung
        public ActionResult SuaNguoiDung(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                ViewBag.listQuyen = db.QUYENs.ToList();
                return View(db.NHANVIENs.Find(id));

            } return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SuaNguoiDung(NHANVIEN nhanvien)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                if (ModelState.IsValid)
                {
                    NHANVIEN nhanvienUpdate = db.NHANVIENs.Find(nhanvien.id);
                    nhanvienUpdate.HoTen = nhanvien.HoTen;
                    nhanvienUpdate.DiaChi = nhanvien.DiaChi;
                    nhanvienUpdate.SoDT = nhanvien.SoDT;
                    nhanvienUpdate.id_Quyen = nhanvien.id_Quyen;
                    nhanvienUpdate.TenDangNhap = nhanvienUpdate.TenDangNhap;
                    db.Entry(nhanvienUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            } return RedirectToAction("Index", "Home");

        }
        #endregion

        [HttpPost]
        public JsonResult XoaNguoiDung(string id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && quyenNguoiDung.Quyen.Ten == "Admin")
            {
                if (ModelState.IsValid)
                {
                    NHANVIEN nhanvien = db.NHANVIENs.Find(Convert.ToInt32(id));
                    db.NHANVIENs.Remove(nhanvien);
                    db.SaveChanges();
                    return Json("Yes", JsonRequestBehavior.AllowGet);
                }
                return Json("No", JsonRequestBehavior.AllowGet);
            }
            return Json("No", JsonRequestBehavior.AllowGet);
        }      
    }
}