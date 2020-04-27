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
    public partial class Trangchu : Form
    {
        public Trangchu()
        {
            InitializeComponent();
        }

        private void btnExit(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            QLNhanVien temp1 = new QLNhanVien();
            temp1.Region = this.Region;
            temp1.Show();
            this.Hide();
        }

        private void btnQLDocGia_Click(object sender, EventArgs e)
        {
            QLDocGia form = new QLDocGia();
            form.Region = this.Region;
            form.Show();
            this.Hide();

        }

        private void btnQLSach_Click(object sender, EventArgs e)
        {
            QLSach form1 = new QLSach();
            form1.Region = this.Region;
            form1.Show();
            this.Hide();
        }
    }
}
