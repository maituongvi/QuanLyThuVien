using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyThuVien_MTV
{
    // Phiếu mượn sách
    class BorrowBook
    {
        SqlConnection sqlConn;
        string cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
        QuanLyThuVienDatabaseDataContext qltvDB;

        public BorrowBook()
        {
            sqlConn = new SqlConnection(cnStr);
            qltvDB = new QuanLyThuVienDatabaseDataContext(sqlConn);
        }

        public PhieuMuonSach ThemPhieuMuonSach(string maDocGia)
        {
            PhieuMuonSach p = new PhieuMuonSach();
            p.MaDocGia = Int32.Parse(maDocGia);
            p.NgayMuon = DateTime.Now;
            qltvDB.PhieuMuonSaches.InsertOnSubmit(p);
            qltvDB.SubmitChanges();
            return p;

        }
    }
}
