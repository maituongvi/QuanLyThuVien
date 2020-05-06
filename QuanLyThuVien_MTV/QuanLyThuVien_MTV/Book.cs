using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyThuVien_MTV
{
    class Book
    {
        SqlConnection sqlConn;
        string cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
        QuanLyThuVienDatabaseDataContext qltvDB;

        public Book()
        {
            sqlConn = new SqlConnection(cnStr);
            qltvDB = new QuanLyThuVienDatabaseDataContext(sqlConn);
        }

        public IEnumerable<Sach> layDSSach()
        {
            IEnumerable<Sach> sach = qltvDB.GetTable<Sach>();
            return sach;
        }

        public void ThemSach(string tenSach, string tg, int namXB, string NXB, double donGia)
        {
            Sach s = new Sach(); // object Sach trong QuanLyThuVienDatabase

            s.TenSach = tenSach;
            s.TacGia = tg;
            s.NamXuatBan = namXB;
            s.NhaXuatBan = NXB;
            s.TriGia = donGia;
            s.NgayNhap = DateTime.Now;

            qltvDB.Saches.InsertOnSubmit(s);
            qltvDB.SubmitChanges();

        }

        public void XoaSach(string id)
        {
            Sach s = qltvDB.Saches.FirstOrDefault(p => p.MaSach.Equals(id));
            qltvDB.Saches.DeleteOnSubmit(s);
            qltvDB.SubmitChanges();
        }

        

        public void CapNhatSach(string id,string tenSach, string tg, int namXB, string NXB, double donGia)
        {
            Sach s = qltvDB.Saches.FirstOrDefault(p => p.MaSach.Equals(id));
            s.TenSach = tenSach;
            s.TacGia = tg;
            s.NamXuatBan = namXB;
            s.NhaXuatBan = NXB;
            s.TriGia = donGia;

            qltvDB.SubmitChanges();

        }

        public IEnumerable<Sach> timKiemSach(string kw)
        {
            IEnumerable<Sach> kqSearch = qltvDB.GetTable<Sach>().Where(p => p.TenSach.Contains(kw) || 
                                                                       p.TacGia.Contains(kw) || p.NhaXuatBan.Contains(kw));
            return kqSearch;
        }
    }
}
