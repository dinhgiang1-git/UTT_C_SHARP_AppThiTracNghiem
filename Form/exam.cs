﻿using System;
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
using DocumentFormat.OpenXml.Math;

namespace ThiTracNghiem
{
    public partial class exam : Form
    {
        string strConn = ConfigurationManager.ConnectionStrings["UTTConnection"].ConnectionString;
        string g_maSinhVien = "";
        string g_maDeThi = "";
        string g_maMonHoc = "";
        string g_maKhoa = "";

        private Timer examTimer;
        private TimeSpan remainingTime;

        List<string> danhSachMaCauHoi = new List<string>();
        int currentQuestionIndex = 0;
        Dictionary<string, string> dapAnChon = new Dictionary<string, string>();
        Dictionary<string, string> dapAnDung = new Dictionary<string, string>();
        public exam(string maDeThi, string username, string maMonHoc, string maKhoa)
        {
            InitializeComponent();
            g_maSinhVien = username;
            g_maDeThi = maDeThi;
            g_maMonHoc = maMonHoc;
            g_maKhoa = maKhoa;

            LoadExam_ThongTin(g_maSinhVien, g_maDeThi);
            LoadExam_DanhSachCauHoi(g_maDeThi);
            LoadDapAnDung(g_maDeThi);
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

        private void LoadExam_ThongTin(string maSinhVien, string maDeThi)
        {
            SqlConnection conn = new SqlConnection(strConn);
            string query = @"SELECT SINHVIEN.MaSinhVien, SINHVIEN.HoTen, DETHI.MaDeThi, COUNT(CAUHOI.MaCauHoi) AS SoLuongCauHoi, DETHI.ThoiGianThi, DETHI.TenDeThi
            FROM SINHVIEN
            JOIN DETHI ON DETHI.MaLop = SINHVIEN.MaLop
            JOIN CAUHOI ON CAUHOI.MaDeThi = DETHI.MaDeThi
            WHERE SINHVIEN.MaSinhVien = @MaSinhVien AND DETHI.MaDeThi = @MaDeThi
            GROUP BY 
                SINHVIEN.MaSinhVien, 
                SINHVIEN.HoTen, 
                DETHI.MaDeThi, 
                DETHI.ThoiGianThi, 
                DETHI.TenDeThi";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                cmd.Parameters.AddWithValue("@MaDeThi", maDeThi);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string masinhvien = reader["MaSinhVien"].ToString();
                        string hoten = reader["HoTen"].ToString();
                        string madethi = reader["MaDeThi"].ToString();
                        string soluongcauhoi = reader["SoLuongCauHoi"].ToString();
                        string thoigianthi = reader["ThoiGianThi"].ToString();
                        string tendethi = reader["TenDeThi"].ToString();

                        int soPhut = int.Parse(thoigianthi);
                        remainingTime = TimeSpan.FromMinutes(soPhut);
                        examtxtThoiGianConLai.Text = remainingTime.ToString(@"mm\:ss");

                        examTimer = new Timer();
                        examTimer.Interval = 1000; // 1 giây
                        examTimer.Tick += ExamTimer_Tick;
                        examTimer.Start();

                        examtxtMaSinhVien.Text = masinhvien;
                        examtxtHoTen.Text = hoten;
                        examtxtMaDeThi.Text = madethi;
                        examtxtSoLuongCauHoi.Text = soluongcauhoi;
                        examtxtThoiGianLamBai.Text = thoigianthi;
                        examtxtBaiKiemTra.Text = tendethi;
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
        private void ExamTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                examtxtThoiGianConLai.Text = remainingTime.ToString(@"mm\:ss");
            }
            else
            {
                examTimer.Stop();
                MessageBox.Show("Hết giờ làm bài!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // TODO: Gọi hàm nộp bài hoặc đóng form
                this.Close(); // hoặc gọi SubmitExam();
            }
        }
        private void LoadDapAnDung(string maDeThi)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaCauHoi, DapAnDung FROM CAUHOI WHERE MaDeThi = @MaDeThi";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDeThi", maDeThi);

                    SqlDataReader reader = cmd.ExecuteReader();
                    dapAnDung.Clear();

                    while (reader.Read())
                    {
                        string maCauHoi = reader["MaCauHoi"].ToString();
                        string dapAn = reader["DapAnDung"].ToString();
                        dapAnDung[maCauHoi] = dapAn;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải đáp án đúng: " + ex.Message);
                }
            }
        }
        private void LoadExam_DanhSachCauHoi(string maDeThi)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaCauHoi FROM CAUHOI WHERE MaDeThi = @MaDeThi";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDeThi", maDeThi);
                    SqlDataReader reader = cmd.ExecuteReader();

                    examlvDanhSachCauHoi.Items.Clear(); // Xóa dữ liệu cũ nếu có
                    int index = 1;
                    danhSachMaCauHoi.Clear(); // reset danh sách
                    while (reader.Read())
                    {
                        string maCauHoi = reader["MaCauHoi"].ToString();
                        string display = "Câu " + index;

                        ListViewItem item = new ListViewItem(display);
                        item.Tag = maCauHoi; // Gắn giá trị MaCauHoi vào tag

                        examlvDanhSachCauHoi.Items.Add(item);
                        danhSachMaCauHoi.Add(maCauHoi); // thêm vào danh sách
                        index++;
                    }

                    // Hiển thị câu hỏi đầu tiên khi vừa load
                    if (danhSachMaCauHoi.Count > 0)
                    {
                        currentQuestionIndex = 0;
                        LoadExam_ChiTietCauHoi(danhSachMaCauHoi[currentQuestionIndex]);
                        examlvDanhSachCauHoi.Items[0].Selected = true;
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

        private void LoadExam_ChiTietCauHoi(string maCauHoi)
        {
            SqlConnection conn = new SqlConnection(strConn);

            try
            {
                conn.Open();
                string query = @"select CAUHOI.NoiDungCauHoi, CAUHOI.DapAnA, CAUHOI.DapAnB, CAUHOI.DapAnC, CAUHOI.DapAnD from CAUHOI where CAUHOI.MaCauHoi = @MaCauHoi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaCauHoi", maCauHoi);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string noidungcauhoi = reader["NoiDungCauHoi"].ToString();
                        string dapanA = reader["DapAnA"].ToString();
                        string dapanB = reader["DapAnB"].ToString();
                        string dapanC = reader["DapAnC"].ToString();
                        string dapanD = reader["DapAnD"].ToString();

                        examrichtxtNoiDungCauHoi.Text = noidungcauhoi;
                        examradioA.Text = dapanA;
                        examradioB.Text = dapanB;
                        examradioC.Text = dapanC;
                        examradioD.Text = dapanD;

                        // Bỏ chọn tất cả RadioButton
                        examradioA.Checked = false;
                        examradioB.Checked = false;
                        examradioC.Checked = false;
                        examradioD.Checked = false;

                        if (dapAnChon.ContainsKey(maCauHoi))
                        {
                            string daChon = dapAnChon[maCauHoi];
                            switch (daChon)
                            {
                                case "A": examradioA.Checked = true; break;
                                case "B": examradioB.Checked = true; break;
                                case "C": examradioC.Checked = true; break;
                                case "D": examradioD.Checked = true; break;
                            }
                        }
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

        private void examlvDanhSachCauHoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (examlvDanhSachCauHoi.SelectedItems.Count > 0)
            {
                LuuDapAnSinhVien();
                //ListViewItem selectedItem = examlvDanhSachCauHoi.SelectedItems[0];
                //string maCauHoi = selectedItem.Tag.ToString();
                currentQuestionIndex = examlvDanhSachCauHoi.SelectedIndices[0];

                LoadExam_ChiTietCauHoi(danhSachMaCauHoi[currentQuestionIndex]);
            }
        }
        private void ChuyenCauHoi(int index)
        {
            if (index >= 0 && index < danhSachMaCauHoi.Count)
            {
                currentQuestionIndex = index;
                LoadExam_ChiTietCauHoi(danhSachMaCauHoi[currentQuestionIndex]);


                foreach (ListViewItem item in examlvDanhSachCauHoi.Items)
                {
                    item.Selected = false;
                }

                examlvDanhSachCauHoi.Items[currentQuestionIndex].Selected = true;
                examlvDanhSachCauHoi.Select();
            }
        }
        private void LuuDapAnSinhVien()
        {
            string maCauHoi = danhSachMaCauHoi[currentQuestionIndex];
            string dapAn = "";

            if (examradioA.Checked) dapAn = "A";
            else if (examradioB.Checked) dapAn = "B";
            else if (examradioC.Checked) dapAn = "C";
            else if (examradioD.Checked) dapAn = "D";
            
            if (dapAn != "")
            {
                dapAnChon[maCauHoi] = dapAn;
                examlvDanhSachCauHoi.Items[currentQuestionIndex].BackColor = Color.LightGreen;
            }
        }

        private void exambtnCauDau_Click(object sender, EventArgs e)
        {
            LuuDapAnSinhVien();
            ChuyenCauHoi(0);
        }

        private void exambtnCauTruoc_Click(object sender, EventArgs e)
        {
            LuuDapAnSinhVien();
            ChuyenCauHoi(currentQuestionIndex - 1);
        }

        private void exambtnCauSau_Click(object sender, EventArgs e)
        {
            LuuDapAnSinhVien();
            ChuyenCauHoi(currentQuestionIndex + 1);
        }

        private void exambtnCauCuoi_Click(object sender, EventArgs e)
        {
            LuuDapAnSinhVien();
            ChuyenCauHoi(danhSachMaCauHoi.Count - 1);
        }

        private void exambtnXoaDanhDau_Click(object sender, EventArgs e)
        {
            examradioA.Checked = false;
            examradioB.Checked = false;
            examradioC.Checked = false;
            examradioD.Checked = false;

            // Xoá đáp án đã chọn nếu có
            string maCauHoi = danhSachMaCauHoi[currentQuestionIndex];
            if (dapAnChon.ContainsKey(maCauHoi))
            {
                dapAnChon.Remove(maCauHoi);
            }

            // Đặt lại màu mặc định cho ListView item
            examlvDanhSachCauHoi.Items[currentQuestionIndex].BackColor = SystemColors.Window;
            examlvDanhSachCauHoi.Items[currentQuestionIndex].ForeColor = SystemColors.ControlText;
        }
        private void TinhDiemVaKetQua()
        {
            int soCauDung = 0;
            int tongSoCau = danhSachMaCauHoi.Count;

            foreach (string maCauHoi in danhSachMaCauHoi)
            {
                if (dapAnChon.ContainsKey(maCauHoi))
                {
                    string dapAnSV = dapAnChon[maCauHoi];
                    string dapAnDungHeThong = dapAnDung[maCauHoi];

                    if (dapAnSV == dapAnDungHeThong)
                    {

                        soCauDung++;
                    }
                }
            }

            float diemMoiCau = 10f / tongSoCau;
            float diemTong = soCauDung * diemMoiCau;

            // Làm tròn 2 chữ số sau dấu phẩy
            diemTong = (float)Math.Round(diemTong, 2);

            MessageBox.Show($"Bạn đã trả lời đúng {soCauDung}/{tongSoCau} câu.\nĐiểm của bạn là: {diemTong}",
                            "Kết quả bài kiểm tra",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            LuuBangDiemSQL(diemTong);
        }

        private void LuuBangDiemSQL(float diemTong)
        {
            //Lấy dữ liệu
            string maDethi = examtxtMaDeThi.Text;
            string maMonHoc = g_maMonHoc;
            string maSinhVien = g_maSinhVien;
            string maKhoa = g_maKhoa;

            //Validate

            //Thêm

            SqlConnection conn = new SqlConnection(strConn);

            try
            {
                conn.Open();
                string query = "Insert into BANGDIEM(Diem, MaDeThi, MaMonHoc, MaSinhVien, MaKhoa) values (@Diem, @MaDeThi, @MaMonHoc, @MaSinhVien, @MaKhoa)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Diem", diemTong);
                cmd.Parameters.AddWithValue("@MaDeThi", maDethi);
                cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                int rowsAffected = cmd.ExecuteNonQuery();
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

        private void exambtnNopBai_Click(object sender, EventArgs e)
        {
            LuuDapAnSinhVien();

            if (!examcheckboxHoanThanhBaiKiemTra.Checked)
            {
                MessageBox.Show("Bạn chưa xác nhận hoàn thành bài kiểm tra.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đếm số câu chưa làm
            int soCauChuaLam = 0;
            foreach (var maCauHoi in danhSachMaCauHoi)
            {
                if (!dapAnChon.ContainsKey(maCauHoi))
                {
                    soCauChuaLam++;
                }
            }

            // Nếu còn câu chưa làm, hỏi lại xác nhận
            if (soCauChuaLam > 0)
            {
                DialogResult result = MessageBox.Show(
                    $"Bạn còn {soCauChuaLam} câu hỏi chưa làm. Bạn có chắc chắn muốn nộp bài không?",
                    "Xác nhận nộp bài",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                {
                    return;
                }
            }
            TinhDiemVaKetQua();
        }
    }
}