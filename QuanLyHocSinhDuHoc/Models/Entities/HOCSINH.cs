//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyHocSinhDuHoc.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOCSINH
    {
        public int id { get; set; }
        public string TenHS { get; set; }
        public string SoCMT { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public string anh { get; set; }
        public Nullable<int> id_GKS { get; set; }
        public Nullable<int> id_BTN { get; set; }
        public Nullable<int> id_HB { get; set; }
        public string timeStart { get; set; }
    }
}
