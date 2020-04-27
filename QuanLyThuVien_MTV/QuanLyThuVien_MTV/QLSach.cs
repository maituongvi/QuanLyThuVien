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
    public partial class QLSach : Form
    {
        public QLSach()
        {
            InitializeComponent();
        }

        private bool checkNhapDuLieu()
        {
            if (String.IsNullOrEmpty(txtTenSach.Text))
                return true;
            else if (String.IsNullOrEmpty(txtTacGia.Text))
                return true;
            else if (String.IsNullOrEmpty(txtNamXuatBan.Text))
                return true;
            else return false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!checkNhapDuLieu())
            {
              
                int nam = Int32.Parse(txtNamXuatBan.Text);
                sach.ThemSach(txtTenSach.Text, txtTacGia.Text, nam, txtNXB.Text, Int32.Parse(txtDonGia.Text));
                lvSach.Items.Clear();
                hienthiSach();
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin ");

        }

        private void QLSach_Load(object sender, EventArgs e)
        {
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lvSach.SelectedIndices.Count > 0)
            {
                int nam = Int32.Parse(txtNamXuatBan.Text);
                sach.CapNhatSach(lvSach.SelectedItems[0].SubItems[0].Text,txtTenSach.Text, txtTacGia.Text, nam, txtNXB.Text, Int32.Parse(txtDonGia.Text));
                lvSach.Items.Clear();
                hienthiSach();
            }
            else MessageBox.Show("Vui lòng chọn sách để cập nhật ");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvSach.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc xóa  không?", "Xóa bằng cấp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //XoaNhanVien(lsvNhanVien.SelectedItems[0].SubItems[0].Text); 
                    sach.XoaSach(lvSach.SelectedItems[0].SubItems[0].Text);
                    lvSach.Items.RemoveAt(lvSach.SelectedIndices[0]);
                    setNull();

                }
            }
            else
                MessageBox.Show("Bạn phải chọn mẩu tin cần xóa");
        }

        void setNull()
        {
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtNamXuatBan.Text = "";
            txtNXB.Text = "";
            txtDonGia.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Trangchu temp1 = new Trangchu();
            temp1.Region = this.Region;
            temp1.Show();
            this.Hide();
        }

        private void pbSearch_Click(object sender, EventArgs e)
        {
            lvSach.Items.Clear();
            foreach (Sach p in sach.timKiemSach(txtTimKiem.Text))
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
    }
}
