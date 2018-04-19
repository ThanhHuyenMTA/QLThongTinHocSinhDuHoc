using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.CommonXuLy
{
    
    public class Xuly
    {
        dbXulyTThsEntities db = new dbXulyTThsEntities();
        public string ReturnHoten(int id_loi)
        {
            TABLE_LOI tb_loi = db.TABLE_LOI.Find(id_loi);
            if (tb_loi.id_HS > 0)
            {
                HOCSINH hs = db.HOCSINHs.Find(tb_loi.id_HS);
                return hs.TenHS;
            }
            else
            {
                if (tb_loi.So_CMT != null)
                {
                    CMT cmt = db.CMTs.Find(tb_loi.So_CMT);
                    return cmt.HoTen;
                }
                else
                {
                    if (tb_loi.id_GKS > 0)
                    {
                        GIAYKHAISINH gks = db.GIAYKHAISINHs.Find(tb_loi.id_GKS);
                        return gks.HoTen;
                    }
                    else
                    {
                        if (tb_loi.id_BTN > 0)
                        {
                            BANGTOTNGHIEP btn = db.BANGTOTNGHIEPs.Find(tb_loi.id_BTN);
                            return btn.HoTen;
                        }
                        else
                        {
                            if (tb_loi.id_HB > 0)
                            {
                                HOCBA hb = db.HOCBAs.Find(tb_loi.id_HB);
                                return hb.HoTen;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}