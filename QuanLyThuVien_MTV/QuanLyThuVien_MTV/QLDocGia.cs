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
    public partial class QLDocGia : Form
    {
        public QLDocGia()
        {
            InitializeComponent();
        }

        private bool checkNhapDuLieu()
        {
            if (String.IsNullOrEmpty(txtTen.Text))
                return true;
            else if (String.IsNullOrEmpty(txtDiaChi.Text))
                return true;
            else if (String.IsNullOrEmpty(txtEmail.Text))
                return true;
            else return false;

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!checkNhapDuLieu() )
            {
                dg.ThemDG(txtTen.Text, dpNgaySinh.Value.ToShortDateString(),
                    txtDiaChi.Text, txtEmail.Text);
                lvDG.Items.Clear();
                HienThiDG();
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin ");
        }

        private void QLDocGia_Load(object sender, EventArgs e)
        {
            lvDG.FullRowSelect = true; //cho phép chọn 1 dòng
            lvDG.View = View.Details; //cho phép hiển thị thông tin chi tiết dạng bảng


            lvDG.View = View.Details;
            //so cot cua LV
            lvDG.Columns.Add("Mã độc giả", 0);
            lvDG.Columns.Add("Họ và tên", 200);
            lvDG.Columns.Add("Ngày sinh:", 100);
            lvDG.Columns.Add("Địa chỉ", 200);
            lvDG.Columns.Add("Email", 100);
            lvDG.Columns.Add("Ngày lập thẻ", 100);
            lvDG.Columns.Add("Ngày hết hạn", 100);

            HienThiDG();
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

        private void lvDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDG.SelectedItems.Count > 0)
            {
                txtTen.Text = lvDG.SelectedItems[0].SubItems[1].Text;
                dpNgaySinh.Text = lvDG.SelectedItems[0].SubItems[2].Text;
                txtDiaChi.Text = lvDG.SelectedItems[0].SubItems[3].Text;
                txtEmail.Text = lvDG.SelectedItems[0].SubItems[4].Text;
                dpNgaySX.Text = lvDG.SelectedItems[0].SubItems[5].Text;
                dpHetHan.Text = lvDG.SelectedItems[0].SubItems[6].Text;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvDG.SelectedIndices.Count > 0)
            {

                dg.CapNhatDG(lvDG.SelectedItems[0].SubItems[0].Text, txtTen.Text, dpNgaySinh.Value.ToShortDateString(),
                    txtDiaChi.Text, txtEmail.Text);
                lvDG.Items.Clear();
                HienThiDG();
            }
            else
                MessageBox.Show("Cần chọn mẫu tin muốn sửa", "Sửa nhân viên");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvDG.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc xóa  không?", "Xóa bằng cấp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //XoaNhanVien(lsvNhanVien.SelectedItems[0].SubItems[0].Text); 
                    dg.XoaDG(lvDG.SelectedItems[0].SubItems[0].Text);
                    lvDG.Items.RemoveAt(lvDG.SelectedIndices[0]);
                    setNull();

                }
            }
            else
                MessageBox.Show("Bạn phải chọn mẩu tin cần xóa");
        }

        void setNull()
        {
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Trangchu temp1 = new Trangchu();
            temp1.Region = this.Region;
            temp1.Show();
            this.Hide();
        }


    }
}
