using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class thongTinTaiKhoan : Form
    {
        string strConn = ConfigurationManager.ConnectionStrings["UTTConnection"].ConnectionString;
        string maSV_GV = "";
        string g_role = "";
        public thongTinTaiKhoan(string id, string role)
        {
            InitializeComponent();
            maSV_GV = id;
            g_role = role;                    
            
            if(role == "giangvien")
            {
            Load_ThongTinGV();
            }
            else if (role == "sinhvien")
            {
                LoadThongTinSV();
            }
        }

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

        private void Load_ThongTinGV()
        {
            using (SqlConnection conn = new SqlConnection(strConn)) 
            {
                try
                {
                    conn.Open();
                    string query = "select GIANGVIEN.MaGiangVien, GIANGVIEN.HoTen, GIANGVIEN.GioiTinh, GIANGVIEN.NgaySinh, GIANGVIEN.QueQuan from GIANGVIEN where GIANGVIEN.MaGiangVien = @MaGiangVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGiangVien", maSV_GV);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string magiangvien = reader["MaGiangVien"].ToString();
                            string hoten = reader["HoTen"].ToString();
                            string gioitinh = reader["GioiTinh"].ToString();
                            string ngaysinh = reader["NgaySinh"].ToString();
                            string quequan = reader["QueQuan"].ToString();

                            tttktxtId.Text = magiangvien;
                            tttktxtHoTen.Text = hoten;
                            tttktxtGioiTinh.Text = gioitinh;
                            tttktxtNgaySinh.Text = ngaysinh;
                            tttktxtQueQuan.Text = quequan;

                        }
                    }
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
        private void LoadThongTinSV()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "select MaSinhVien ,HoTen ,GioiTinh ,NgaySinh ,QueQuan from SINHVIEN where MaSinhVien = @MaSinhVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSV_GV);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string magiangvien = reader["MaSinhVien"].ToString();
                            string hoten = reader["HoTen"].ToString();
                            string gioitinh = reader["GioiTinh"].ToString();
                            string ngaysinh = reader["NgaySinh"].ToString();
                            string quequan = reader["QueQuan"].ToString();

                            tttktxtId.Text = magiangvien;
                            tttktxtHoTen.Text = hoten;
                            tttktxtGioiTinh.Text = gioitinh;
                            tttktxtNgaySinh.Text = ngaysinh;
                            tttktxtQueQuan.Text = quequan;

                        }
                    }
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
