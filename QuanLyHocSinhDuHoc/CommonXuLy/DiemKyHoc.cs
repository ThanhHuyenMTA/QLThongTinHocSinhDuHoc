using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyHocSinhDuHoc.Models.Entities;
namespace QuanLyHocSinhDuHoc.CommonXuLy
{
    public class DiemKyHoc
    {
        private NAMHOC namhoc;

        public NAMHOC Namhoc
        {
            get { return namhoc; }
            set { namhoc = value; }
        }

        private List<KIHOC> lkihoc;

        public List<KIHOC> Lkihoc
        {
            get { return lkihoc; }
            set { lkihoc = value; }
        }     
        public DiemKyHoc() { }
        public DiemKyHoc(NAMHOC nh, List<KIHOC> listKH)
        {
            this.Namhoc = nh;
            this.Lkihoc = listKH;
        }

    }
}