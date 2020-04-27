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
    public partial class QLNhanVien : Form
    {
        public QLNhanVien()
        {
            InitializeComponent();
        }

        private void QLNhanVien_Load(object sender, EventArgs e)
        {
            lvNV.FullRowSelect = true; //cho phép chọn 1 dòng
            lvNV.View = View.Details; //cho phép hiển thị thông tin chi tiết dạng bảng


            lvNV.View = View.Details;
            //so cot cua LV
            lvNV.Columns.Add("MaNV", 0);
            lvNV.Columns.Add("Họ và tên", 200);
            lvNV.Columns.Add("Ngày sinh:", 300);
            lvNV.Columns.Add("Địa chỉ", 200);
            lvNV.Columns.Add("Điện thoại", 100);
            lvNV.Columns.Add("Bằng Câp", 100);
            HienThiNV();
            LoadCPBangCap();
        }

        NhanVien1 nv = new NhanVien1();

        // Load list view Nhan Vien
        void HienThiNV()
        {
            

            foreach (NhanVien e in nv.layDSNhanVien())
            {
                ListViewItem lvi = new ListViewItem(e.MaNhanVien.ToString());

                lvi.SubItems.Add(e.HoTenNhanVien); //Lay cac thuoc tinh: NS
                lvi.SubItems.Add(e.NgaySinh.ToString()); //Lay cac thuoc tinh: NS

                lvi.SubItems.Add(e.DiaChi.ToString()); //DC

                lvi.SubItems.Add(e.DienThoai.ToString()); //DT
                lvi.SubItems.Add(e.MaBangCap.ToString());

                lvNV.Items.Add(lvi);
            } 
           
        }

        Degree bc = new Degree();

        public void LoadCPBangCap()
        {

            cbBangCap.DataSource = bc.layDSBangCap();
            cbBangCap.DisplayMember = "TenBangCap";
            cbBangCap.ValueMember = "MaBangCap";

        }

        private void lvNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvNV.SelectedItems.Count > 0)
            {
                txtTen.Text = lvNV.SelectedItems[0].SubItems[1].Text;
                dpNgaySinh.Text = lvNV.SelectedItems[0].SubItems[2].Text;
                txtDiaChi.Text = lvNV.SelectedItems[0].SubItems[3].Text;
                txtSdt.Text = lvNV.SelectedItems[0].SubItems[4].Text;
                cbBangCap.Text = lvNV.SelectedItems[0].SubItems[5].Text;
            }
        }

     

        private bool checkNhapDuLieu()
        {
            if (String.IsNullOrEmpty(txtTen.Text))
                return true;
            else if (String.IsNullOrEmpty(txtDiaChi.Text))
                return true;
            else if (String.IsNullOrEmpty(txtSdt.Text))
                return true;
            else return false;

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Trangchu temp1 = new Trangchu();
            temp1.Region = this.Region;
            temp1.Show();
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!checkNhapDuLieu() )
            {
                nv.ThemNV(txtTen.Text, dpNgaySinh.Value.ToShortDateString(),
                    txtDiaChi.Text, txtSdt.Text, cbBangCap.SelectedIndex +1);
                lvNV.Items.Clear();
                HienThiNV();
            }
            else MessageBox.Show("Vui lòng nhập đủ thông tin ");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvNV.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc xóa  không?", "Xóa bằng cấp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    //XoaNhanVien(lsvNhanVien.SelectedItems[0].SubItems[0].Text); 
                    nv.XoaNV(lvNV.SelectedItems[0].SubItems[0].Text);
                    lvNV.Items.RemoveAt(lvNV.SelectedIndices[0]);
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
            txtSdt.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvNV.SelectedIndices.Count > 0)
            {
             
                nv.CapNhatNV(lvNV.SelectedItems[0].SubItems[0].Text, txtTen.Text, dpNgaySinh.Value.ToShortDateString(),
                    txtDiaChi.Text, txtSdt.Text, cbBangCap.SelectedIndex +1);
                lvNV.Items.Clear();
                HienThiNV();
            }
            else
                MessageBox.Show("Cần chọn mẫu tin muốn sửa", "Sửa nhân viên");

        }
    }
}
