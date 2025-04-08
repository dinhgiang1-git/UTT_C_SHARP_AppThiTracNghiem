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
    public partial class doiMatKhau : Form
    {
        string strConn = ConfigurationManager.ConnectionStrings["UTTConnection"].ConnectionString;
        string g_id = "";
        public doiMatKhau(string id)
        {
            InitializeComponent();
            g_id = id;
        }

        private void change()
        {
            //Lấy dữ liệu
            string matKhauMoi = dmkMatKhauMoi.Text.Trim();
            string nhapLaiMatKhauMoi = dmkNhapLaiMatKhauMoi.Text.Trim();

            //Valide
            if(string.IsNullOrEmpty(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!");
                return;
            }
            if (string.IsNullOrEmpty(nhapLaiMatKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu mới!");
            }
            
            //Change
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Update GIANGVIEN set MatKhau = @MatKhau where MaGiangVien = @MaGiangVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MatKhau", nhapLaiMatKhauMoi);
                    cmd.Parameters.AddWithValue("@MaGiangVien", g_id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0) {
                        MessageBox.Show("Đổi mật khẩu thành công!");
                        dmkMatKhauMoi.Clear();
                        dmkNhapLaiMatKhauMoi.Clear();
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

        private void dmkbtnXacNhan_Click(object sender, EventArgs e)
        {
            change();
        }

        private void dmkbtnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
