﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbXulyTThsEntities : DbContext
    {
        public dbXulyTThsEntities()
            : base("name=dbXulyTThsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BANGTOTNGHIEP> BANGTOTNGHIEPs { get; set; }
        public virtual DbSet<BAOCAOTHONGKE> BAOCAOTHONGKEs { get; set; }
        public virtual DbSet<CMT> CMTs { get; set; }
        public virtual DbSet<CT_BCTK> CT_BCTK { get; set; }
        public virtual DbSet<CT_LOTRINH> CT_LOTRINH { get; set; }
        public virtual DbSet<GIAYCHUNGTHUC> GIAYCHUNGTHUCs { get; set; }
        public virtual DbSet<GIAYKHAISINH> GIAYKHAISINHs { get; set; }
        public virtual DbSet<HOCBA> HOCBAs { get; set; }
        public virtual DbSet<HOPDONG> HOPDONGs { get; set; }
        public virtual DbSet<HOSOH> HOSOHS { get; set; }
        public virtual DbSet<KIHOC> KIHOCs { get; set; }
        public virtual DbSet<LOTRINH> LOTRINHs { get; set; }
        public virtual DbSet<NAMHOC> NAMHOCs { get; set; }
        public virtual DbSet<NGUOIGIAMHO> NGUOIGIAMHOes { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<QUANTRI> QUANTRIs { get; set; }
        public virtual DbSet<QUYEN> QUYENs { get; set; }
        public virtual DbSet<TABLE_LOI> TABLE_LOI { get; set; }
        public virtual DbSet<HOCSINH> HOCSINHs { get; set; }
    }
}
