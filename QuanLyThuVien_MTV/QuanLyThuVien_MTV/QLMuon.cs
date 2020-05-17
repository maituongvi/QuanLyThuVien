using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien_MTV
{
    public partial class QLMuon : Form
    {

        public string maDG { get; set; }
        public string tenDG { get; set; }
        public string email { get; set; }
        public QLMuon()
        {
            InitializeComponent();
        }

        private void QLMuon_Load(object sender, EventArgs e)
        {
            txtMaDG.Text = maDG;
            txtTenDG.Text = tenDG;
            txtEmailDG.Text = email;


            lvSach.FullRowSelect = true; //cho phép chọn 1 dòng
            lvSach.View = View.Details; //cho phép hiển thị thông tin chi tiết dạng bảng


            lvSach.View = View.Details;
            //so cot cua LV
            lvSach.Columns.Add("MaSach", 0);
            lvSach.Columns.Add("Tên sách", 200);
            lvSach.Columns.Add("Tác giả", 100);
            lvSach.Columns.Add("Năm xuất bản", 100);
            lvSach.Columns.Add("Nhà xuất bản", 100);
            lvSach.Columns.Add("Trị giá", 100);

            hienthiSach();
        }

        Book sach = new Book();
        public void hienthiSach()
        {
            foreach (Sach e in sach.layDSSach())
            {
                ListViewItem lvi = new ListViewItem(e.MaSach.ToString());

                lvi.SubItems.Add(e.TenSach.ToString()); //Lay cac thuoc tinh: NS
                lvi.SubItems.Add(e.TacGia.ToString()); //Lay cac thuoc tinh: NS

                lvi.SubItems.Add(e.NamXuatBan.ToString()); //DC

                lvi.SubItems.Add(e.NhaXuatBan.ToString()); //DT
                lvi.SubItems.Add(e.TriGia.ToString());

                lvSach.Items.Add(lvi);
            }
        }


        private void pbSearch_Click(object sender, EventArgs e)
        {
            lvSach.Items.Clear();
            foreach (Sach p in sach.timKiemSach(txtSearch.Text))
            {
                ListViewItem lvi = new ListViewItem(p.MaSach.ToString());

                lvi.SubItems.Add(p.TenSach.ToString()); //Lay cac thuoc tinh: NS
                lvi.SubItems.Add(p.TacGia.ToString()); //Lay cac thuoc tinh: NS

                lvi.SubItems.Add(p.NamXuatBan.ToString()); //DC

                lvi.SubItems.Add(p.NhaXuatBan.ToString()); //DT
                lvi.SubItems.Add(p.TriGia.ToString());

                lvSach.Items.Add(lvi);


            }
        }

        private void lvSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSach.SelectedItems.Count > 0)
            {
                txtTenSach.Text = lvSach.SelectedItems[0].SubItems[1].Text;
                txtTacGia.Text = lvSach.SelectedItems[0].SubItems[2].Text;
                txtNamXuatBan.Text = lvSach.SelectedItems[0].SubItems[3].Text;
                txtNXB.Text = lvSach.SelectedItems[0].SubItems[4].Text;
                txtDonGia.Text = lvSach.SelectedItems[0].SubItems[5].Text;
            }
        }

        //Tạo mới đối tượng phiếu mượn sách
        BorrowBook br = new BorrowBook();
        BorrowingDetails brDt = new BorrowingDetails();
        private void btnMuon_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaDG.Text))
            {
                 MessageBox.Show("Vui lòng chọn thông tin độc giả ");
            }
            else
            {
                if (lvSach.SelectedItems.Count >0)
                {
                    PhieuMuonSach p = br.ThemPhieuMuonSach(txtMaDG.Text);
                    brDt.ThemChiTietPhieuMuonSach(lvSach.SelectedItems[0].SubItems[0].Text, p.MaPhieuMuon);
                    MessageBox.Show("Mượn thành công sách " + lvSach.SelectedItems[0].SubItems[1].Text);

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sách cần mượn");
                }
            }
        }

        

        private void btnBack_Click(object sender, EventArgs e)
        {
            QLTra temp1 = new QLTra();
            temp1.Region = this.Region;
            temp1.Show();
            this.Hide();
        }
    }
}
