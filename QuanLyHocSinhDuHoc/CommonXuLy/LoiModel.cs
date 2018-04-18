using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyHocSinhDuHoc.Models.Entities;

namespace QuanLyHocSinhDuHoc.CommonXuLy
{
    public class LoiModel
    {
        private HOCSINH hocsinh;

        public HOCSINH Hocsinh
        {
            get { return hocsinh; }
            set { hocsinh = value; }
        }
        private GIAYKHAISINH giaykhaisinh;

        public GIAYKHAISINH Giaykhaisinh
        {
            get { return giaykhaisinh; }
            set { giaykhaisinh = value; }
        }
        private BANGTOTNGHIEP bangtotnghiep;

        public BANGTOTNGHIEP Bangtotnghiep
        {
            get { return bangtotnghiep; }
            set { bangtotnghiep = value; }
        }
        private HOCBA hocba;

        public HOCBA Hocba
        {
            get { return hocba; }
            set { hocba = value; }
        }
        private CMT cmt;

        public CMT Cmt
        {
            get { return cmt; }
            set { cmt = value; }
        }
        public LoiModel() { }
        public LoiModel(HOCSINH hocsinh,CMT cmt,GIAYKHAISINH giaykhaisinh,BANGTOTNGHIEP bangtotnghiep, HOCBA hocba)
        {
            this.hocsinh = hocsinh;
            this.cmt = cmt;
            this.giaykhaisinh = giaykhaisinh;
            this.bangtotnghiep = bangtotnghiep;
            this.hocba = hocba;
        }
        public LoiModel(CMT cmt, GIAYKHAISINH giaykhaisinh, BANGTOTNGHIEP bangtotnghiep, HOCBA hocba)
        {
            this.cmt = cmt;
            this.giaykhaisinh = giaykhaisinh;
            this.bangtotnghiep = bangtotnghiep;
            this.hocba = hocba;
        }
        public LoiModel(GIAYKHAISINH giaykhaisinh, BANGTOTNGHIEP bangtotnghiep, HOCBA hocba)
        {
            this.giaykhaisinh = giaykhaisinh;
            this.bangtotnghiep = bangtotnghiep;
            this.hocba = hocba;
        }
        public LoiModel(CMT cmt, GIAYKHAISINH giaykhaisinh)
        {
            this.cmt = cmt;
            this.giaykhaisinh = giaykhaisinh;
        }
    }
}