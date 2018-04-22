using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyHocSinhDuHoc.CommonXuLy
{
    public class Paging
    {
        public int currentPage { set; get; }
        public int nextPage1 { set; get; }
        public int nextPage2 { set; get; }
        public int prevPage1 { set; get; }
        public int prevPage2 { set; get; }

        public bool hasNext1 { set; get; }
        public bool hasNext2 { set; get; }
        public bool hasPrev1 { set; get; }
        public bool hasPrev2 { set; get; }

        public Paging(int curPage, int totalPage)
        {
            //totalPage : tổng số trang
            //currentPage : trang hiện tại
            this.currentPage = curPage; //giatri trung tam
            this.hasNext1 = (curPage + 1 < totalPage) ? true : false;
            this.hasNext2 = (curPage + 2 <= totalPage) ? true : false;

            this.hasPrev1 = (curPage - 2 > 1) ? true : false; //Lùi 1
            this.hasPrev2 = (curPage - 2 > 1) ? true : false;  //lùi toàn bộ

            this.nextPage1 = curPage + 1;
            this.nextPage2 = curPage + 2;
            this.prevPage1 = curPage - 1;
            this.prevPage2 = curPage - 2;

        }
    }
}