using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        public thongTinTaiKhoan(string id)
        {
            InitializeComponent();
            maSV_GV = id;
            Load_ThongTin();
        }

        private void Load_ThongTin()
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
    }
}
