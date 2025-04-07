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
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ThiTracNghiem
{
    public partial class fSinhVienMain : Form
    {
        string strConn = "Server=DINHDUCGIANG;Database=UTT_ThiTracNghiem;Integrated Security=True;";
        string maSinhVien = "";
        string maKhoa = "";
        public fSinhVienMain(string username)
        {
            maSinhVien = username;
            InitializeComponent();

            Load_ThongTinSinhVien(maSinhVien);
            LoadCB_MonHoc(maSinhVien);
            string maMonHoc = tcdcbMonHoc.SelectedValue.ToString();
            LoadCB_DeThi(maSinhVien, maMonHoc);
            string maDeThi = tcdcbDeThi.SelectedValue.ToString();
            LoadData_BangDiem(maDeThi);
            LoadData_DeThi(maMonHoc);
        }

        private void fSinhVienMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Load_ThongTinSinhVien(string maSinhVien)
        {
            
            SqlConnection conn = new SqlConnection(strConn);
            string query = "select SINHVIEN.MaSinhVien, SINHVIEN.HoTen, SINHVIEN.GioiTinh, SINHVIEN.NgaySinh, SINHVIEN.QueQuan, LOP.TenLop, KHOA.TenKhoa, KHOA.MaKhoa from SINHVIEN join LOP on SINHVIEN.MaLop = LOP.MaLop join KHOA on LOP.MaKhoa = KHOA.MaKhoa where SINHVIEN.MaSinhVien = @MaSinhVien";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string masinhvien = reader["MaSinhVien"].ToString();
                        string hoTen = reader["HoTen"].ToString();
                        string gioiTinh = reader["GioiTinh"].ToString();
                        string ngaySinh = Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");
                        string queQuan = reader["QueQuan"].ToString();
                        string tenLop = reader["TenLop"].ToString();
                        string tenKhoa = reader["Tenkhoa"].ToString();
                        maKhoa = reader["MaKhoa"].ToString();

                        tcdtxtMaSinhVien.Text = masinhvien;
                        tcdtxtHoTen.Text = hoTen;
                        tcdtxtGioiTinh.Text = gioiTinh;
                        tcdtxtNgaySinh.Text = ngaySinh;
                        tcdtxtQueQuan.Text = queQuan;
                        tcdtxtLop.Text = tenLop;
                        tcdtxtTenKhoa.Text = tenKhoa;

                        btktxtMaSinhVien.Text = masinhvien;
                        btktxtHoTen.Text = hoTen;
                        btktxtGioiTinh.Text = gioiTinh;
                        btktxtNgaySinh.Text = ngaySinh;
                        btktxtQueQuan.Text = queQuan;  
                        btktxtLop.Text = tenLop;
                        btktxtKhoa.Text= tenKhoa;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void LoadCB_MonHoc(string maSinhVien)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                
                try
                {
                    conn.Open();
                    string query = "select MONHOC.TenMonHoc, MONHOC.MaMonHoc from MONHOC  join KHOA on KHOA.MaKhoa = MONHOC.MaKhoa  join LOP on LOP.MaKhoa = KHOA.MaKhoa join SINHVIEN on SINHVIEN.MaLop = LOP.MaLop where SINHVIEN.MaSinhVien = @MaSinhVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    tcdcbMonHoc.DataSource = dt.Copy();
                    tcdcbMonHoc.DisplayMember = "TenMonHoc";
                    tcdcbMonHoc.ValueMember = "MaMonHoc";     
                    
                    btkcbMonHoc.DataSource = dt.Copy();
                    btkcbMonHoc.DisplayMember = "TenMonHoc";
                    btkcbMonHoc.ValueMember = "MaMonHoc";

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
        private void LoadCB_DeThi(string maSinhVien, string maMonHoc)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {

                try
                {
                    conn.Open();
                    string query = "select DETHI.MaDeThi, DETHI.TenDeThi from DETHI  join MONHOC on MONHOC.MaMonHoc = DETHI.MaMonHoc  join LOP on LOP.MaLop = DETHI.MaLop join SINHVIEN on SINHVIEN.MaLop = Lop.MaLop where DETHI.MaMonHoc = @MaMonHoc and SINHVIEN.MaSinhVien = @MaSinhVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    tcdcbDeThi.DataSource = dt.Copy();
                    tcdcbDeThi.DisplayMember = "TenDeThi";
                    tcdcbDeThi.ValueMember = "MaDeThi";

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
        private void LoadData_BangDiem(string maDeThi)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "select BANGDIEM.MaBangDiem, DETHI.TenDeThi, BANGDIEM.Diem, DETHI.ThoiGianThi, DETHI.SoLuongCauHoi, DETHI.ThoiGianBatDau, DETHI.ThoiGianKetThuc from BANGDIEM join DETHI on DETHI.MaDeThi = BANGDIEM.MaDeThi where BANGDIEM.MaDeThi = @MaDeThi";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDeThi", maDeThi);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataBangDiem.DataSource = dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void LoadData_DeThi(string maMonHoc)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "select DETHI.MaDeThi, DETHI.TenDeThi, DETHI.ThoiGianThi, DETHI.ThoiGianBatDau, DETHI.ThoiGianKetThuc, DETHI.SoLuongCauHoi from DETHI join MONHOC on MONHOC.MaMonHoc = DETHI.MaMonHoc where MONHOC.MaMonHoc = @MaMonHoc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataDeThi.DataSource = dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void tcdcbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maMonHoc = tcdcbMonHoc.SelectedValue.ToString(); 
            LoadCB_DeThi(maSinhVien, maMonHoc);
        }

        private void tcdcbDeThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maDeThi = tcdcbDeThi.SelectedValue.ToString();
            LoadData_BangDiem(maDeThi);
        }

        private void btkcbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maMonHoc = btkcbMonHoc.SelectedValue.ToString();
            LoadData_DeThi(maMonHoc);
        }

        private void dataDeThi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataDeThi.Rows[e.RowIndex];
                btktxtMaDeThi.Text = row.Cells["MaDeThi"].Value.ToString();               
            }
        }

        private void bktbtnLamBaiThi_Click(object sender, EventArgs e)
        {
            string maDeThi = btktxtMaDeThi.Text;
            string maSinhVien = btktxtMaSinhVien.Text;
            string maMonHoc = btkcbMonHoc.SelectedValue.ToString();

            if (string.IsNullOrEmpty(maDeThi)) {
                MessageBox.Show("Vui lòng chọn một đề thi để làm bài!");
                return;
            }

            exam xam = new exam(maDeThi, maSinhVien, maMonHoc, maKhoa);
            xam.Show();
        }
    }
}
