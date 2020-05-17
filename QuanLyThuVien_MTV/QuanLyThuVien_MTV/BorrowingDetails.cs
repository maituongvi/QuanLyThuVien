using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyThuVien_MTV
{
    // Chi tiết phiếu mượn sách (gồm mã sách và mã phiếu mượn sách và ngày trả)
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

        // trả sách

        public void CapNhatTraSach(string maSach, int maPhieu)
        {
            ChiTietPhieuMuon p1 = qltvDB.GetTable<ChiTietPhieuMuon>().
                Where(s => s.MaPhieuMuon.Equals(maPhieu)).Where(s => s.MaSach.Equals(maSach)).First();
            //ChiTietPhieuMuon p = qltvDB.ChiTietPhieuMuons.
            //    FirstOrDefault(s => s.MaPhieuMuon.Equals(maPhieu) 
            //|| s.MaSach.Equals(maSach));
            p1.ngayTra = DateTime.Now;
            qltvDB.SubmitChanges();

        }

        //lấy danh sách mã của những cuốn sách của khách mượn
        public List<int> dsMaSachDocGiaMuon(int maPhieu)
        {
            List<int> kq = new List<int>();
            IEnumerable<ChiTietPhieuMuon> p = qltvDB.GetTable<ChiTietPhieuMuon>().
                Where(s => s.MaPhieuMuon.Equals(maPhieu) ).Where(s=> s.ngayTra.Equals(null));
            foreach(ChiTietPhieuMuon i in p)
            {
                kq.Add(i.MaSach);
            }
            return kq;
        }

       

    }
}
