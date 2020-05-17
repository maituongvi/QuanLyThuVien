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
    public partial class QLTra : Form
    {
        public QLTra()
        {
            InitializeComponent();
        }

        Reader r = new Reader();
        BorrowBook b = new BorrowBook();
        BorrowingDetails b1 = new BorrowingDetails();
        Book book = new Book();


        private void pbSearch_Click(object sender, EventArgs e)
        {
            lvSach.Items.Clear();
            if(checkNhapMaDocGia() == 1)
            {
                MessageBox.Show("Mã độc giả không đúng. Vui lòng nhập lại");
                txtTimKiem.Text ="";
            }
            else
            {
                DocGia p = r.timKiemDocGiaTheoMa(Int32.Parse(txtTimKiem.Text));
                lbMa.Text = p.MaDocGia.ToString();
                lbTen.Text = p.HoTenDocGia;
                lbNgaySinh.Text = p.NgaySinh.ToString();
                lbDiaChi.Text = p.DiaChi;
                lbEmail.Text = p.Email;
                lbNgayDK.Text = p.NgayLapThe.ToString();
                lbNgayHetHan.Text = p.NgayHetHan.ToString();

                PhieuMuonSach phieu = b.TimPhieuMuonSachCuaDocGia(p.MaDocGia);
                lbMaPhieu.Text = phieu.MaPhieuMuon.ToString();

                //MessageBox.Show(phieu.MaPhieuMuon.ToString());

                List<int> list = b1.dsMaSachDocGiaMuon(phieu.MaPhieuMuon);
                
                if(list.Count == 0)
                {
                    MessageBox.Show("Độc giả hiện đang không mượn cuốn sách nào!!!");
                    lbMaPhieu.Text = "";
                }
                else
                {
                    foreach (int i in list)
                    {
                        Sach sach = book.timSachTheoMa(i);
                        ListViewItem lvi = new ListViewItem(sach.MaSach.ToString());

                        lvi.SubItems.Add(sach.TenSach);


                        lvi.SubItems.Add(phieu.NgayMuon.ToString());

                        lvSach.Items.Add(lvi);
                    }
                }
         
                

            }

            
            
        }

        private void QLTra_Load(object sender, EventArgs e)
        {
            lvSach.FullRowSelect = true; //cho phép chọn 1 dòng
            lvSach.View = View.Details; //cho phép hiển thị thông tin chi tiết dạng bảng


            lvSach.View = View.Details;
            //so cot cua LV
            lvSach.Columns.Add("Mã sách", 100);
            lvSach.Columns.Add("Tên sách", 200);
            lvSach.Columns.Add("Ngày mượn", 100);
            
        }

        private int checkNhapMaDocGia()
        {
            int ma;
            string ma1 = txtTimKiem.Text;
            ma = Int32.Parse(ma1);
            IEnumerable<DocGia> list = r.layDSDocGia();
            if (ma < 0 || ma > list.Count())
            {
                return 1;
            }
            else return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvSach.SelectedIndices.Count>0)
            {
                b1.CapNhatTraSach(lbMaSach.Text, Int32.Parse(lbMaPhieu.Text));
                lvSach.Items.RemoveAt(lvSach.SelectedIndices[0]);
                MessageBox.Show("Trả sách thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách cần trả");
            }

        }

        private void lvSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSach.SelectedIndices.Count > 0)
            {
                lbMaSach.Text = lvSach.SelectedItems[0].SubItems[0].Text;
                lbTenSach.Text = lvSach.SelectedItems[0].SubItems[1].Text;
                lbNgayMuon.Text = lvSach.SelectedItems[0].SubItems[2].Text;
            }
        }

       

        private void btnMuon_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "" ||lbMa.Text=="")
            {
                MessageBox.Show("Vui lòng nhập mã độc giả và trả sách trước khi mượn!!!");
            }
            else
            {
                if(lvSach.Items.Count > 0)
                {
                    MessageBox.Show("Vui lòng trả sách trước khi mượn. ");
                }
                else
                {

                    QLMuon fm = new QLMuon();
                    fm.maDG = lbMa.Text;
                    fm.tenDG = lbTen.Text;
                    fm.email = lbEmail.Text;
                    fm.Region = this.Region;
                    fm.Show();
                    this.Hide();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Trangchu fm = new Trangchu();
            fm.Region = this.Region;
            fm.Show();
            this.Hide();
        }
    }
}
