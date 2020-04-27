using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyThuVien_MTV
{
    class NhanVien1
    {
        SqlConnection sqlConn;
        string cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
        QuanLyThuVienDatabaseDataContext qltvDB;

        public NhanVien1()
        {
            sqlConn = new SqlConnection(cnStr);
            qltvDB = new QuanLyThuVienDatabaseDataContext(sqlConn);
        }

        public IEnumerable<NhanVien> layDSNhanVien()
        {
            IEnumerable<NhanVien> emp = qltvDB.GetTable<NhanVien>();
            return emp;
        }

        public void ThemNV( string ten, string ns, string dc, string sdt, int bc)
        {
            NhanVien nv = new NhanVien();
 
            nv.HoTenNhanVien = ten;
            nv.NgaySinh =DateTime.Parse( ns);
            nv.DiaChi = dc;
            nv.DienThoai = sdt;
            nv.MaBangCap = bc;

            qltvDB.NhanViens.InsertOnSubmit(nv);
            qltvDB.SubmitChanges();

        }

        public void XoaNV(string id)
        {
            NhanVien nv = qltvDB.NhanViens.FirstOrDefault(s => s.MaNhanVien.Equals(id));
            qltvDB.NhanViens.DeleteOnSubmit(nv);
            qltvDB.SubmitChanges();
        }

        public NhanVien timNhanVienTheoMa(string id)
        {
            NhanVien nv = qltvDB.NhanViens.FirstOrDefault(s => s.MaNhanVien.Equals(id));
            return nv;
        }

        public void CapNhatNV(string id, string ten, string ns, string dc, string sdt, int bc)
        {
            NhanVien nv = qltvDB.NhanViens.FirstOrDefault(s => s.MaNhanVien.Equals(id));
            nv.HoTenNhanVien = ten;
            nv.NgaySinh = DateTime.Parse(ns);
            nv.DiaChi = dc;
            nv.DienThoai = sdt;
            nv.MaBangCap = bc;

         
            qltvDB.SubmitChanges();

        }


    }
}
