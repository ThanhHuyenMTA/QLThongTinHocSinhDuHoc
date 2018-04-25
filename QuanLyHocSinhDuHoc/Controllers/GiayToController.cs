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
            HOCSINH hs = db.HOCSINHs.Find(id);
            CMT cmt = db.CMTs.Find(hs.SoCMT);
            HOCBA hb = db.HOCBAs.Find(hs.id_HB);
            ViewBag.tenhs = dich.ReplaceUnicode(cmt.HoTen);
            ViewBag.noisinh = dich.ReplaceUnicode(cmt.QueQuan);
            ViewBag.noithuongtru = dich.ReplaceUnicode(cmt.NoiThuongTru);
            ViewBag.noisonghientai = dich.ReplaceUnicode(hb.NoiSongHienTai);
            ViewBag.sdt = hs.sdt;
            return View();
        }
    }
}