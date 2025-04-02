using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class fMain: Form
    {
        public string _MaGiangVien;
        string strConn = "Server=DINHDUCGIANG;Database=UTT_ThiTracNghiem;Integrated Security=True;";
        public fMain(string MaGiangVien)
        {
            InitializeComponent();
            _MaGiangVien = MaGiangVien;
            Infomation_tcd();
        }
        private void Infomation_tcd()
        {
            tcdtxtMaGiangVien.Enabled = false;
            tcdtxtHoTen.Enabled = false;
            tcdtxtMaKhoa.Enabled = false;


          
            SqlConnection conn = new SqlConnection(strConn);
            string query = @"Select MaGiangVien, HoTen, MaKhoa from GIANGVIEN where MaGiangVien = @MaGiangVien";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGiangVien", _MaGiangVien);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string magiangvien = reader["MaGiangVien"].ToString();
                        string hoten = reader["HoTen"].ToString();
                        string makhoa= reader["MaKhoa"].ToString();

                        tcdtxtHoTen.Text = hoten;
                        tcdtxtMaGiangVien.Text = magiangvien;
                        tcdtxtMaKhoa.Text = makhoa;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database Error: "+ ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
