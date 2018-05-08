using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.CommonXuLy;
using QuanLyHocSinhDuHoc.Models.Entities;
using PaymentSystem.Controllers;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class GiayToController : BaseController
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        DichTuDong dich = new DichTuDong();
        // GET: GiayTo
        public ActionResult Giaynhaphoc(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyHoSo"|| quyenNguoiDung.Quyen.Ten=="Admin"))
            {
            
                Session["chuyenTab"] = null;
                HOCSINH hs = db.HOCSINHs.Find(id);
                ViewBag.id_hsss = hs.id;
                ViewBag.sdt = hs.sdt;
                CMT cmt = db.CMTs.Find(hs.SoCMT);
                HOCBA hb = db.HOCBAs.Find(hs.id_HB);
                if (cmt != null)
                {
                    ViewBag.tenhs = dich.ReplaceUnicode(cmt.HoTen);
                    ViewBag.noisinh = dich.ReplaceUnicode(cmt.QueQuan);
                    ViewBag.noithuongtru = dich.ReplaceUnicode(cmt.NoiThuongTru);
                }
                if (hb != null)
                {
                    ViewBag.noisonghientai = dich.ReplaceUnicode(hb.NoiSongHienTai);
                    ViewBag.gioiTinh = hb.GioiTinh;
                    ViewBag.ngaySinh = hb.NgaySinh;
                }

                // dua ra danh sach thanh vien gia dinh
                List<HoKhau> listHK = db.HoKhaus.Where(n => n.id_hs == id).ToList();
                ViewBag.dstv = listHK;
                //giay nhap hoc
                GiayNhapHoc giaynhaphoc = db.GiayNhapHocs.SingleOrDefault(n => n.id_hs == hs.id);
                if (giaynhaphoc != null)
                {
                    View(giaynhaphoc);
                }
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Giaynhaphoc(GiayNhapHoc gnh)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyHoSo" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                if (ModelState.IsValid)
                {
                    db.GiayNhapHocs.Add(gnh);
                    db.SaveChanges();
                    return RedirectToAction("Giaynhaphoc/" + gnh.id_hs);
                }
                return View(gnh);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}