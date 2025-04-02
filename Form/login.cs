using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiTracNghiem.Class;
using ThiTracNghiem.Library;

namespace ThiTracNghiem
{
    public partial class login: Form
    {
        public login()
        {
            InitializeComponent();
        }
        //Đổi màu nền back ground
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Rectangle rc = ClientRectangle;
            if (rc.IsEmpty)
                return;
            if (rc.Width == 0 || rc.Height == 0)
                return;
            using (LinearGradientBrush brush = new LinearGradientBrush(rc, Color.White, Color.FromArgb(196, 232, 250), 90F))
            {
                e.Graphics.FillRectangle(brush, rc);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //Lấy thông tin từ giao diện
            string username = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;

            CGiangVien gv = new CGiangVien()
            {
                MaGiangVien = txtTaiKhoan.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
            };

            //Validate
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Kiểm tra thông tin
            if(LGiangVien.IsExitsAccount(gv)) {
                fMain main = new fMain(username);
                main.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Tài khoản mật khẩu không đúng");
            }
        }
    }
}
