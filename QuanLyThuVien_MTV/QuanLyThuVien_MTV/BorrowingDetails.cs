using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyThuVien_MTV
{
    // Chi tiết phiếu mượn sách (gồm mã sách và mã phiếu mượn sách)
    class BorrowingDetails
    {
        SqlConnection sqlConn;
        string cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
        QuanLyThuVienDatabaseDataContext qltvDB;

        public BorrowingDetails()
        {
            sqlConn = new SqlConnection(cnStr);
            qltvDB = new QuanLyThuVienDatabaseDataContext(sqlConn);
        }

        public void ThemChiTietPhieuMuonSach(string maSach, int maPhieu)
        {
            ChiTietPhieuMuon p = new ChiTietPhieuMuon();
            p.MaSach = Int32.Parse(maSach);
            p.MaPhieuMuon = maPhieu;
            qltvDB.ChiTietPhieuMuons.InsertOnSubmit(p);
            qltvDB.SubmitChanges();
        }

    }
}
