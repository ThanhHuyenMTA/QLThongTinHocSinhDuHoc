using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.CommonXuLy
{
    public class ModelQuyenNguoiDung
    {
        private NHANVIEN nhanvien;

        public NHANVIEN Nhanvien
        {
            get { return nhanvien; }
            set { nhanvien = value; }
        }
        private QUYEN quyen;

        public QUYEN Quyen
        {
            get { return quyen; }
            set { quyen = value; }
        }
        public ModelQuyenNguoiDung()
        {

        }
        public ModelQuyenNguoiDung(NHANVIEN nhanvien,QUYEN quyen)
        {
            this.nhanvien = nhanvien;
            this.quyen = quyen;
        }

    }
}