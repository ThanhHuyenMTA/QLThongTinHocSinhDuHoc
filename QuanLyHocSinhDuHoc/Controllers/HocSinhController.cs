using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinhDuHoc.Models.Entities;
using System.Data.Entity;
using System.IO;
using QuanLyHocSinhDuHoc.CommonXuLy;
using System.Data.SqlClient;

namespace QuanLyHocSinhDuHoc.Controllers
{
    public class HocSinhController : Controller
    {
        // GET: HocSinh
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        public ActionResult Index(int? page)
        {
            Session["chuyenTab"] = null;
            Xuly xuly = new Xuly();
            List<string> listNam = new List<string>();
            List<HOCSINH> lisths = db.HOCSINHs.OrderByDescending(n=>n.timeStart).ToList();
            foreach (var item in lisths)
            {
                if (!xuly.checkTrungTimeStart(item.timeStart, listNam)) //chưa tồn tại trong list thì thêm vào list
                    listNam.Add(item.timeStart);
            }
            ViewBag.listNam = listNam;
            int count = db.HOCSINHs.ToList().Count;
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
            var list = db.Database.SqlQuery<HOCSINH>("exec PhanTrang @LineStart,@soBanGhi ", idParam1, idParam2).ToList<HOCSINH>();

            return View(list);
        }
        //xử lý phân trang ve cuoi cung     
        public JsonResult XulyPhanTrangVeCuoicung()
        {
            int trangcuoi = (int)Session["chiasotrang"];
            return Json(trangcuoi, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Themmoi()
        {
           return View();
        }
        [HttpPost]
        public JsonResult Themmoi(HOCSINH hocsinh)
        {
            if(ModelState.IsValid)
            {
                DateTime today =DateTime.Now;              
                hocsinh.timeStart = today.ToString("yyyy");
                db.HOCSINHs.Add(hocsinh);
                db.SaveChanges();
                int id_HS = hocsinh.id;
                Session["id_HS"] = id_HS;
                return Json(id_HS, JsonRequestBehavior.AllowGet);
            }
            return Json("Thêm thất bại!", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpLoadImage()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                //lưu tên file
                var fileName = Path.GetFileName(fileImg.FileName);
                //lưu đường dẫn
                var path = Path.Combine(Server.MapPath("~/Content/images/profile"), fileName);
                // file is uploaded
                var type = fileImg.ContentType;
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    if (type == "image/jpeg" || type == "image/jpg" || type == "image/png")
                        fileImg.SaveAs(path);
                }
                //dua ra hoc sinh can upload anh
                HOCSINH hs = db.HOCSINHs.ToList().Last();
                hs.anh = fileName;
                db.Entry(hs).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //end
                return Json(fileName, JsonRequestBehavior.AllowGet);

            }
            return Json("Khong", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ThemmoiChung()
        {

            return View();
        }
        public ActionResult SuaHocsinh(int id)
        {
            HOCSINH hocsinh = db.HOCSINHs.Find(id);
            return View(hocsinh);
        }
        [HttpPost]
        public ActionResult SuaHocsinh(HOCSINH hocsinh)
        {
            if (ModelState.IsValid)
            {
                HOCSINH hocsinhupdate = db.HOCSINHs.Find(hocsinh.id);
                hocsinhupdate.TenHS = hocsinh.TenHS;
                hocsinhupdate.sdt = hocsinh.sdt;
                hocsinhupdate.email = hocsinh.email;
                db.Entry(hocsinhupdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["chuyenTab"] = null;
                return RedirectToAction("DetailChung/"+hocsinhupdate.id);
            }
            return View(hocsinh);
        }

        public ActionResult DetailHocsinh(int id)
        {
            HOCSINH hocsinh = db.HOCSINHs.Find(id);
            return View(hocsinh);
        }
        public ActionResult DetailChung(int id)
        {
            Session["id_hsDetail"] = id;
            HOCSINH hocsinh = db.HOCSINHs.Find(id);
            return View(hocsinh);
        }

        //GET: sOLUONG Load san pham
        public JsonResult SearchNam(string Namloc)
        {
            List<HOCSINH> list = db.HOCSINHs.Where(n => n.timeStart ==Namloc).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchKeyName(string keySearch)
        {
            List<HOCSINH> list = db.HOCSINHs.Where(n => n.TenHS.Contains(keySearch)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}