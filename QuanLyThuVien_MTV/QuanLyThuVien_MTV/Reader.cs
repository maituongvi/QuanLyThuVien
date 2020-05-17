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
    class Reader
    {
        SqlConnection sqlConn;
        string cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
        QuanLyThuVienDatabaseDataContext qltvDB;

        public Reader()
        {
            sqlConn = new SqlConnection(cnStr);
            qltvDB = new QuanLyThuVienDatabaseDataContext(sqlConn);
        }

        public IEnumerable<DocGia> layDSDocGia()
        {
            IEnumerable<DocGia> r = qltvDB.GetTable<DocGia>();
            return r;
        }

        public void ThemDG(string ten, string ns, string dc, string email)
        {
            DocGia dg = new DocGia();

            dg.HoTenDocGia = ten;
            dg.NgaySinh = DateTime.Parse(ns);
            dg.DiaChi = dc;
            dg.Email = email;
            DateTime now = DateTime.Now;
            dg.NgayLapThe = now;
            dg.NgayHetHan = now.AddDays(730);
           

            qltvDB.DocGias.InsertOnSubmit(dg);
            qltvDB.SubmitChanges();

        }

        public void XoaDG(string id)
        {
            DocGia dg = qltvDB.DocGias.FirstOrDefault(s => s.MaDocGia.Equals(id));
            qltvDB.DocGias.DeleteOnSubmit(dg);
            qltvDB.SubmitChanges();
        }

        public void CapNhatDG(string id,string ten, string ns, string dc, string email)
        {
            DocGia dg = qltvDB.DocGias.FirstOrDefault(s => s.MaDocGia.Equals(id));
            dg.HoTenDocGia = ten;
            dg.NgaySinh = DateTime.Parse(ns);
            dg.DiaChi = dc;
            dg.Email = email;
      

            qltvDB.SubmitChanges();

        }

        public IEnumerable<DocGia> timKiemDocGia(string kw)
        {
            IEnumerable<DocGia> kqSearch = qltvDB.GetTable<DocGia>().Where(p => p.HoTenDocGia.Contains(kw) || p.Email.Contains(kw) ||p.DiaChi.Contains(kw));
            return kqSearch;
        }

        public DocGia timKiemDocGiaTheoMa(int ma)
        {
            DocGia p = qltvDB.DocGias.FirstOrDefault(s => s.MaDocGia.Equals(ma));
            return p;
        }
    }
}
