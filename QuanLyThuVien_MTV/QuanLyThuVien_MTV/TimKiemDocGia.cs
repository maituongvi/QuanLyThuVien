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
    public partial class TimKiemDocGia : Form
    {
        public TimKiemDocGia()
        {
            InitializeComponent();
        }
        Reader dg = new Reader();
        void HienThiDG()
        {


            foreach (DocGia e in dg.layDSDocGia())
            {
                ListViewItem lvi = new ListViewItem(e.MaDocGia.ToString());

                lvi.SubItems.Add(e.HoTenDocGia); //Lay cac thuoc tinh: NS
                lvi.SubItems.Add(e.NgaySinh.ToString()); //Lay cac thuoc tinh: NS

                lvi.SubItems.Add(e.DiaChi.ToString()); //DC

                lvi.SubItems.Add(e.Email.ToString()); //DT
                lvi.SubItems.Add(e.NgayLapThe.ToString());
                lvi.SubItems.Add(e.NgayHetHan.ToString());

                lvDG.Items.Add(lvi);
            }

        }

        private void pbSearch_Click(object sender, EventArgs e)
        {
            lvDG.Items.Clear();
            foreach (DocGia p in dg.timKiemDocGia(txtTimKiem.Text))
            {
                ListViewItem lvi = new ListViewItem(p.MaDocGia.ToString());

                lvi.SubItems.Add(p.HoTenDocGia); //Lay cac thuoc tinh: NS
                lvi.SubItems.Add(p.NgaySinh.ToString()); //Lay cac thuoc tinh: NS

                lvi.SubItems.Add(p.DiaChi.ToString()); //DC

                lvi.SubItems.Add(p.Email.ToString()); //DT
                lvi.SubItems.Add(p.NgayLapThe.ToString());
                lvi.SubItems.Add(p.NgayHetHan.ToString());

                lvDG.Items.Add(lvi);
            }
        }

        private void TimKiemDocGia_Load(object sender, EventArgs e)
        {
            lvDG.FullRowSelect = true; //cho phép chọn 1 dòng
            lvDG.View = View.Details; //cho phép hiển thị thông tin chi tiết dạng bảng


            lvDG.View = View.Details;
            //so cot cua LV
            lvDG.Columns.Add("Mã", 50);
            lvDG.Columns.Add("Họ và tên", 200);
            lvDG.Columns.Add("Ngày sinh:", 95);
            
            HienThiDG();
        }

        private void lvDG_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lvDG.SelectedItems.Count > 0)
            {

                lbMa.Text = lvDG.SelectedItems[0].SubItems[0].Text;
                lbTen.Text = lvDG.SelectedItems[0].SubItems[1].Text;
                lbNgaySinh.Text = lvDG.SelectedItems[0].SubItems[2].Text;
                lbDiaChi.Text = lvDG.SelectedItems[0].SubItems[3].Text;
                lbEmail.Text = lvDG.SelectedItems[0].SubItems[4].Text;
                lbNgayDK.Text = lvDG.SelectedItems[0].SubItems[5].Text;
                lbNgayHetHan.Text = lvDG.SelectedItems[0].SubItems[6].Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(lvDG.SelectedItems.Count > 0)
            {
                QLMuon fm = new QLMuon();
                
                fm.maDG = lvDG.SelectedItems[0].SubItems[0].Text;
                fm.tenDG = lvDG.SelectedItems[0].SubItems[1].Text;
                fm.email = lvDG.SelectedItems[0].SubItems[4].Text;
                fm.Region = this.Region;
                fm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn độc giả");
            }



        }
    }
}
