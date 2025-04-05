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
using DocumentFormat.OpenXml.Office.Word;
using ThiTracNghiem.Class;
using ThiTracNghiem.Library;

namespace ThiTracNghiem
{
    public partial class login: Form
    {
        string strConn = "Server=DINHDUCGIANG;Database=UTT_ThiTracNghiem;Integrated Security=True;";
        public login()
        {
            InitializeComponent();
            logincbVaiTro.SelectedIndex = 0;
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
            //Lấy thông tin 
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
            if (logincbVaiTro.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn vai trò!");
                return;
            }

            //Login
            using(SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string queryGiaoVien = "Select count(*) from GIANGVIEN where MaGiangVien = @MaGiangVien and MatKhau = @MatKhau";
                    string querySinhVien = "Select count(*) from SINHVIEN where MaSinhVien = @MaSinhVien and MatKhau = @MatKhau";
                    string query = "";

                    if (logincbVaiTro.SelectedItem == "Sinh Viên")
                    {
                        query = querySinhVien;
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaSinhVien", username);
                        cmd.Parameters.AddWithValue("@MatKhau", password);

                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0) 
                        {
                            fSinhVienMain sv = new fSinhVienMain(username);
                            sv.Show();
                            this.Hide();
                        } else
                        {
                            MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
                        }
                    }
                    else
                    {
                        if (logincbVaiTro.SelectedItem == "Giảng Viên")
                        {
                            query = queryGiaoVien;
                            SqlCommand cmd1 = new SqlCommand(query, conn);
                            cmd1.Parameters.AddWithValue("@MaGiangVien", username);
                            cmd1.Parameters.AddWithValue("@MatKhau", password);

                            int count = (int)cmd1.ExecuteScalar();
                            if (count > 0)
                            {
                                fMain fmain = new fMain(username);
                                fmain.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
                            }
                        }
                    }
                    Console.WriteLine(query);             
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message); 
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
