using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using QuanLyHocSinhDuHoc.CommonXuLy;
using System.IO;
using PaymentSystem.Controllers;
using System.Data.Entity;
using System.Data.SqlClient;

namespace QuanLyHocSinhDuHoc.Controllers
{

    public class XulyHocSinhController : BaseController
    {
        
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        List<string> list = new List<string>();
        // GET: XulyHocSinh
        //upload File
        [HttpPost]
        public JsonResult UpLoadFile()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var file = Request.Files["HelpSectionFile"];              
                //lưu tên file
                var fileName = Path.GetFileName(file.FileName);
                //lưu đường dẫn
                var path = Path.Combine(Server.MapPath("~/Content/filePDF"), fileName);
                // file is uploaded
                var type = file.ContentType;
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "File đã tồn tại";
                }
                else
                {
                    if (type == "application/docx" || type == "application/pdf")
                        file.SaveAs(path);
                }
                Session["file"] = fileName;
               
                
                return Json(fileName, JsonRequestBehavior.AllowGet);
            }
            Session["file"] = null;
            return Json("Khong", JsonRequestBehavior.AllowGet);
        }
        //load file 
        public ActionResult TestPdf(string url)
        {
            string[] arrListStr = url.Split('.');//tách trong chuỗi str trên khi gặp ký tự '.'
            string type = arrListStr[arrListStr.Length - 1];
            var path = Path.Combine(Server.MapPath("~/Content/filePDF"), url);
            if (type == "pdf")
            {
                ViewBag.tbHien = "YES";
                return File(path, "application/pdf");
            }
            else return View();
        }
        public ActionResult Index(int? page)
        {           
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh"|| quyenNguoiDung.Quyen.Ten=="Admin"))
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


                int count = db.TABLE_LOI.ToList().Count;
                ViewBag.All = count;
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
                var list = db.Database.SqlQuery<TABLE_LOI>("exec PhanTrangLoi @LineStart,@soBanGhi ", idParam1, idParam2).ToList<TABLE_LOI>();
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
        public ActionResult IndexTbXanh()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                return View(db.TABLE_LOI.Where(n => n.TrangThai == "1").ToList());
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult IndexTbVang()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                return View(db.TABLE_LOI.Where(n => n.TrangThai == "2").ToList());
            }
            return RedirectToAction("Index", "Home");
            
        }
        public ActionResult IndexTbDo()
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                return View(db.TABLE_LOI.Where(n => n.TrangThai == "3").ToList());
            }
            return RedirectToAction("Index", "Home");
           
        }
        public ActionResult ChinhSua (int id) //id -- mã bên lỗi
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                LoiModel chitietLoi = new LoiModel();
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(id);
                string typeLoi = tableLoi.TypeLOI;
                ViewBag.typeLoi = typeLoi;

                if (typeLoi == "HoTen")
                {
                    return RedirectToAction("ChinhSuaLoiHoTen", new { id = id });
                }
                if (typeLoi == "NgaySinh")
                {

                    return RedirectToAction("ChinhSuaLoiNgaySinh", new { id = id });
                }
                if (typeLoi == "NoiSinh")
                {

                    return RedirectToAction("ChinhSuaLoiNoiSinh", new { id = id });
                }
                if (typeLoi == "QueQuan")
                {
                    return RedirectToAction("ChinhSuaLoiQueQuan", new { id = id });
                }
                if (typeLoi == "GioiTinh")
                {
                    return RedirectToAction("ChinhSuaLoiGioiTinh", new { id = id });
                }
                if (typeLoi == "DanToc")
                {
                    return RedirectToAction("ChinhSuaLoiDanToc", new { id = id });
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChinhSuaLoiHoTen(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                LoiModel chitietLoi = new LoiModel();
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(id);
                string typeLoi = tableLoi.TypeLOI;
                ViewBag.typeLoi = typeLoi;
                ViewBag.idLoi = id;
                Xuly xuly = new Xuly();
                ViewBag.HocSinhLoi = xuly.ReturnHoten(id);
                int id_HS = tableLoi.id_HS ?? 0;
                int id_GKS = tableLoi.id_GKS ?? 0;
                int id_BTN = tableLoi.id_BTN ?? 0;
                int id_HB = tableLoi.id_HB ?? 0;
                string so_CMT = tableLoi.So_CMT ?? null;
                if (check(id_HS))
                {
                    chitietLoi.Hocsinh = db.HOCSINHs.Find(id_HS);
                }
                if (check(id_GKS))
                {
                    chitietLoi.Giaykhaisinh = db.GIAYKHAISINHs.Find(id_GKS);
                }
                if (check(id_BTN))
                {
                    chitietLoi.Bangtotnghiep = db.BANGTOTNGHIEPs.Find(id_BTN);
                }
                if (check(id_HB))
                {
                    chitietLoi.Hocba = db.HOCBAs.Find(id_HB);
                }
                if (so_CMT != null)
                {
                    chitietLoi.Cmt = db.CMTs.Find(so_CMT);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChinhSuaLoiNgaySinh(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                LoiModel chitietLoi = new LoiModel();
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(id);
                string typeLoi = tableLoi.TypeLOI;
                ViewBag.typeLoi = typeLoi;
                Xuly xuly = new Xuly();
                ViewBag.HocSinhLoi = xuly.ReturnHoten(id);
                ViewBag.idLoi = id;
                int id_GKS = tableLoi.id_GKS ?? 0;
                int id_BTN = tableLoi.id_BTN ?? 0;
                int id_HB = tableLoi.id_HB ?? 0;
                string so_CMT = tableLoi.So_CMT ?? null;
                if (check(id_GKS))
                {
                    chitietLoi.Giaykhaisinh = db.GIAYKHAISINHs.Find(id_GKS);
                }
                if (check(id_BTN))
                {
                    chitietLoi.Bangtotnghiep = db.BANGTOTNGHIEPs.Find(id_BTN);
                }
                if (check(id_HB))
                {
                    chitietLoi.Hocba = db.HOCBAs.Find(id_HB);
                }
                if (so_CMT != null)
                {
                    chitietLoi.Cmt = db.CMTs.Find(so_CMT);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChinhSuaLoiNoiSinh(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                LoiModel chitietLoi = new LoiModel();
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(id);
                string typeLoi = tableLoi.TypeLOI;
                ViewBag.idLoi = id;
                ViewBag.typeLoi = typeLoi;
                Xuly xuly = new Xuly();
                ViewBag.HocSinhLoi = xuly.ReturnHoten(id);
                int id_GKS = tableLoi.id_GKS ?? 0;
                int id_BTN = tableLoi.id_BTN ?? 0;
                int id_HB = tableLoi.id_HB ?? 0;
                if (check(id_GKS))
                {
                    chitietLoi.Giaykhaisinh = db.GIAYKHAISINHs.Find(id_GKS);
                }
                if (check(id_BTN))
                {
                    chitietLoi.Bangtotnghiep = db.BANGTOTNGHIEPs.Find(id_BTN);
                }
                if (check(id_HB))
                {
                    chitietLoi.Hocba = db.HOCBAs.Find(id_HB);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChinhSuaLoiQueQuan(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                LoiModel chitietLoi = new LoiModel();
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(id);
                string typeLoi = tableLoi.TypeLOI;
                ViewBag.idLoi = id;
                ViewBag.typeLoi = typeLoi;
                Xuly xuly = new Xuly();
                ViewBag.HocSinhLoi = xuly.ReturnHoten(id);
                int id_GKS = tableLoi.id_GKS ?? 0;
                string so_CMT = tableLoi.So_CMT ?? null;
                if (check(id_GKS))
                {
                    chitietLoi.Giaykhaisinh = db.GIAYKHAISINHs.Find(id_GKS);
                }
                if (so_CMT != null)
                {
                    chitietLoi.Cmt = db.CMTs.Find(so_CMT);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChinhSuaLoiGioiTinh(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                LoiModel chitietLoi = new LoiModel();
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(id);
                string typeLoi = tableLoi.TypeLOI;
                ViewBag.typeLoi = typeLoi;
                ViewBag.idLoi = id;
                Xuly xuly = new Xuly();
                ViewBag.HocSinhLoi = xuly.ReturnHoten(id);
                int id_GKS = tableLoi.id_GKS ?? 0;
                int id_BTN = tableLoi.id_BTN ?? 0;
                int id_HB = tableLoi.id_HB ?? 0;
                string so_CMT = tableLoi.So_CMT ?? null;
                if (check(id_GKS))
                {
                    chitietLoi.Giaykhaisinh = db.GIAYKHAISINHs.Find(id_GKS);
                }
                if (check(id_BTN))
                {
                    chitietLoi.Bangtotnghiep = db.BANGTOTNGHIEPs.Find(id_BTN);
                }
                if (check(id_HB))
                {
                    chitietLoi.Hocba = db.HOCBAs.Find(id_HB);
                }
                if (so_CMT != null)
                {
                    chitietLoi.Cmt = db.CMTs.Find(so_CMT);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChinhSuaLoiDanToc(int id)
        {
            ModelQuyenNguoiDung quyenNguoiDung = Session["QuyenNguoiDung"] as ModelQuyenNguoiDung;
            if (quyenNguoiDung != null && (quyenNguoiDung.Quyen.Ten == "QuanLyThongTinHocSinh" || quyenNguoiDung.Quyen.Ten == "Admin"))
            {
                LoiModel chitietLoi = new LoiModel();
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(id);
                string typeLoi = tableLoi.TypeLOI;
                ViewBag.typeLoi = typeLoi;
                ViewBag.idLoi = id;
                Xuly xuly = new Xuly();
                ViewBag.HocSinhLoi = xuly.ReturnHoten(id);
                int id_GKS = tableLoi.id_GKS ?? 0;
                int id_BTN = tableLoi.id_BTN ?? 0;
                int id_HB = tableLoi.id_HB ?? 0;
                if (check(id_GKS))
                {
                    chitietLoi.Giaykhaisinh = db.GIAYKHAISINHs.Find(id_GKS);
                }
                if (check(id_BTN))
                {
                    chitietLoi.Bangtotnghiep = db.BANGTOTNGHIEPs.Find(id_BTN);
                }
                if (check(id_HB))
                {
                    chitietLoi.Hocba = db.HOCBAs.Find(id_HB);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public bool check(int a)
        {
            if (a > 0)
                return true;
            else return false;
        }
        //Phần sửa
        [HttpPost]
        public JsonResult ChinhSuaLoiHoTen(List<string> listKey)
        {
            LoiModel chitietLoi = new LoiModel();
            int idLoi =Int32.Parse(listKey[0]);
            string TenHS = listKey[1];
            string TenCMT = listKey[2];
            string TenGKS = listKey[3];
            string TenBTN = listKey[4];
            string TenHB = listKey[5];
            TABLE_LOI tableLoi = db.TABLE_LOI.Find(idLoi);
            string typeLoi = tableLoi.TypeLOI;           
            if(TenHS !=null)
            {
                HOCSINH hocsinh = db.HOCSINHs.Find(tableLoi.id_HS);
                hocsinh.TenHS = TenHS;
                db.Entry(hocsinh).State = System.Data.Entity.EntityState.Modified;
            }
            if(TenCMT !=null)
            {
                CMT cmt = db.CMTs.Find(tableLoi.So_CMT);
                cmt.HoTen = TenCMT;
                db.Entry(cmt).State = System.Data.Entity.EntityState.Modified;                
            }
            if(TenGKS !=null)
            {
                GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(tableLoi.id_GKS);
                gks.HoTen = TenGKS;
                db.Entry(gks).State = System.Data.Entity.EntityState.Modified;
            }
            if(TenBTN !=null)
            {
                BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.Find(tableLoi.id_BTN);
                btn.HoTen = TenBTN;
                db.Entry(btn).State = System.Data.Entity.EntityState.Modified;
            }
            if(TenHB !=null)
            {
                HOCBA hocba = db.HOCBAs.Find(tableLoi.id_HB);
                hocba.HoTen = TenHB;
                db.Entry(hocba).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChinhSuaLoiNgaySinh(List<string> listKey)
        {
            try
            {
                LoiModel chitietLoi = new LoiModel();
                int idLoi = Int32.Parse(listKey[0]);
                TABLE_LOI tableLoi = db.TABLE_LOI.Find(idLoi);
                string typeLoi = tableLoi.TypeLOI;
                if (listKey[1] != null)
                {
                    DateTime NgaySinhCMT = DateTime.Parse(listKey[1]);
                    CMT cmt = db.CMTs.Find(tableLoi.So_CMT);
                    cmt.NgaySinh = NgaySinhCMT;
                    db.Entry(cmt).State = EntityState.Modified;
                }

                if (listKey[2] != null)
                {
                    DateTime NgaySinhGKS = DateTime.Parse(listKey[2]);
                    GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(tableLoi.id_GKS);
                    gks.NgaySinh = NgaySinhGKS;
                    db.Entry(gks).State = EntityState.Modified;
                }

                if (listKey[3] != null)
                {
                    DateTime NgaySinhBTN = DateTime.Parse(listKey[3]);
                    BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.Find(tableLoi.id_BTN);
                    btn.NgaySinh = NgaySinhBTN;
                    db.Entry(btn).State = EntityState.Modified;
                }

                if (listKey[4] != null)
                {
                    DateTime NgaySinhHB = DateTime.Parse(listKey[4]);
                    HOCBA hocba = db.HOCBAs.Find(tableLoi.id_HB);
                    hocba.NgaySinh = NgaySinhHB;
                    db.Entry(hocba).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e, JsonRequestBehavior.AllowGet);
               
            }
        }
        public bool sats(GIAYKHAISINH hb)
        {
            if (ModelState.IsValid)
            {
                return true;
            }
            else
                return false;
        }
        [HttpPost]
        public JsonResult ChinhSuaLoiNoiSinh(List<string> listKey)
        {
            LoiModel chitietLoi = new LoiModel();
            int idLoi = Int32.Parse(listKey[0]);
            string NoiSinhGKS = listKey[1];
            string NoiSinhBTN = listKey[2];
            string NoiSinhHB = listKey[3];
            TABLE_LOI tableLoi = db.TABLE_LOI.Find(idLoi);
            string typeLoi = tableLoi.TypeLOI;
            if (NoiSinhGKS != null)
            {
                GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(tableLoi.id_GKS);
                gks.NoiSinh = NoiSinhGKS;
                db.Entry(gks).State = System.Data.Entity.EntityState.Modified;
            }
            if (NoiSinhBTN != null)
            {
                BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.Find(tableLoi.id_BTN);
                btn.NoiSinh = NoiSinhBTN;
                db.Entry(btn).State = System.Data.Entity.EntityState.Modified;
            }
            if (NoiSinhHB != null)
            {
                HOCBA hocba = db.HOCBAs.Find(tableLoi.id_HB);
                hocba.NoiSinh = NoiSinhHB;
                db.Entry(hocba).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChinhSuaLoiQueQuan(List<string> listKey)
        {
            LoiModel chitietLoi = new LoiModel();
            int idLoi = Int32.Parse(listKey[0]);
            string QueQuanCMT = listKey[1];
            string QueQuanGKS = listKey[2];
            TABLE_LOI tableLoi = db.TABLE_LOI.Find(idLoi);
            string typeLoi = tableLoi.TypeLOI;

            if (QueQuanCMT != null)
            {
                CMT cmt = db.CMTs.Find(tableLoi.So_CMT);
                cmt.QueQuan = QueQuanCMT;
                db.Entry(cmt).State = System.Data.Entity.EntityState.Modified;
            }
            if (QueQuanGKS != null)
            {
                GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(tableLoi.id_GKS);
                gks.QueQuan = QueQuanGKS;
                db.Entry(gks).State = System.Data.Entity.EntityState.Modified;
            }        
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);

        }

         [HttpPost]
        public JsonResult ChinhSuaLoiGioiTinh(List<string> listKey)
        {
            LoiModel chitietLoi = new LoiModel();
            int idLoi = Int32.Parse(listKey[0]);
            string GioiTinhCMT = listKey[1];
            string GioiTinhGKS = listKey[2];
            string GioiTinhBTN = listKey[3];
            string GioiTinhHB = listKey[4];
            TABLE_LOI tableLoi = db.TABLE_LOI.Find(idLoi);
            string typeLoi = tableLoi.TypeLOI;
            if (GioiTinhCMT != null)
            {
                CMT cmt = db.CMTs.Find(tableLoi.So_CMT);
                cmt.GioiTinh = GioiTinhCMT;
                db.Entry(cmt).State = System.Data.Entity.EntityState.Modified;
            }
            if (GioiTinhGKS != null)
            {
                GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(tableLoi.id_GKS);
                gks.GioiTinh = GioiTinhGKS;
                db.Entry(gks).State = System.Data.Entity.EntityState.Modified;
            }
            if (GioiTinhBTN != null)
            {
                BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.Find(tableLoi.id_BTN);
                btn.GioiTinh = GioiTinhBTN;
                db.Entry(btn).State = System.Data.Entity.EntityState.Modified;
            }
            if (GioiTinhHB != null)
            {
                HOCBA hocba = db.HOCBAs.Find(tableLoi.id_HB);
                hocba.GioiTinh = GioiTinhHB;
                db.Entry(hocba).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
         [HttpPost]
        public JsonResult ChinhSuaLoiDanToc(List<string> listKey)
         {
             LoiModel chitietLoi = new LoiModel();
             int idLoi = Int32.Parse(listKey[0]);
             string DanTocGKS = listKey[1];
             string DanTocBTN = listKey[2];
             string DanTocHB = listKey[3];
             TABLE_LOI tableLoi = db.TABLE_LOI.Find(idLoi);
             string typeLoi = tableLoi.TypeLOI;
             if (DanTocGKS != null)
             {
                 GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(tableLoi.id_GKS);
                 gks.DanToc = DanTocGKS;
                 db.Entry(gks).State = System.Data.Entity.EntityState.Modified;
             }
             if (DanTocBTN != null)
             {
                 BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.Find(tableLoi.id_BTN);
                 btn.DanToc = DanTocBTN;
                 db.Entry(btn).State = System.Data.Entity.EntityState.Modified;
             }
             if (DanTocHB != null)
             {
                 HOCBA hocba = db.HOCBAs.Find(tableLoi.id_HB);
                 hocba.DanToc = DanTocHB;
                 db.Entry(hocba).State = System.Data.Entity.EntityState.Modified;
             }
             db.SaveChanges();
             return Json(true, JsonRequestBehavior.AllowGet);
         }

        [HttpPost]
        public ActionResult SearchLoi(string keySearchLoi) //tìm kiếm theo tên và loại lỗi
         {
              List<HOCSINH> list = db.HOCSINHs.Where(n => n.TenHS.Contains(keySearchLoi)).ToList();
              List<TABLE_LOI> listTbleLoi =db.TABLE_LOI.Where(n=>n.TypeLOI.Contains(keySearchLoi)).ToList();
              foreach(var item in list)
              {
                  TABLE_LOI tb =db.TABLE_LOI.SingleOrDefault(n=>n.id_HS ==item.id);
                  if(tb!=null)
                      listTbleLoi.Add(tb);

              }
              return View(listTbleLoi);
         }
    }
}