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
    public partial class fMain : Form
    {
        public string _MaGiangVien;
        string strConn = "Server=DINHDUCGIANG;Database=UTT_ThiTracNghiem;Integrated Security=True;";
        public fMain(string MaGiangVien)
        {
            InitializeComponent();
            _MaGiangVien = MaGiangVien;
            LoadComboBox_Khoa();

            string maKhoa = qllcbKhoa.SelectedValue.ToString();
            string maKhoa_MH = qlmhcbKhoa.SelectedValue.ToString();
            string maKhoa_SV = qlsvcbKhoa.SelectedValue.ToString();

            LoadComboBox_Lop(maKhoa_SV);

            string maLop = "";

            Infomation_tcd();

            LoadData_Khoa();
            LoadData_MonHoc(maKhoa_MH);
            LoadData_Lop(maKhoa);
            LoadData_SinhVien(maLop);

            ResetComboBox();
        }

        private void ResetComboBox()
        {

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
                        string makhoa = reader["MaKhoa"].ToString();

                        tcdtxtHoTen.Text = hoten;
                        tcdtxtMaGiangVien.Text = magiangvien;
                        tcdtxtMaKhoa.Text = makhoa;
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
        private void LoadData_Khoa()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select MaKhoa, TenKhoa from KHOA";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    dataKhoa.DataSource = dt;
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
        private void LoadData_MonHoc(string maKhoa)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select MaMonHoc, TenMonHoc from MonHoc where MaKhoa = @MaKhoa";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    dataMonHoc.DataSource = dt;
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
        private void LoadData_Lop(string maKhoa)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select MaLop, TenLop from LOP where MaKhoa = @MaKhoa";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataLop.DataSource = dt;
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
        private void LoadData_SinhVien(string maLop)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select MaSinhVien, Malop, HoTen, GioiTinh, NgaySinh, QueQuan from SINHVIEN where MaLop = @MaLop";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataSinhVien.DataSource = dt;
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
        private void LoadComboBox_Khoa()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select MaKhoa, TenKhoa from KHOA";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    qlmhcbKhoa.DataSource = dt.Copy();
                    qlmhcbKhoa.DisplayMember = "TenKhoa";
                    qlmhcbKhoa.ValueMember = "MaKhoa";

                    qllcbKhoa.DataSource = dt.Copy();
                    qllcbKhoa.DisplayMember = "TenKhoa";
                    qllcbKhoa.ValueMember = "Makhoa";

                    qlsvcbKhoa.DataSource = dt.Copy();
                    qlsvcbKhoa.DisplayMember = "TenKhoa";
                    qlsvcbKhoa.ValueMember = "MaKhoa";
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
        private void LoadComboBox_Lop(string maKhoa)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select Malop, TenLop from LOP where MaKhoa = @MaKhoa";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    qlsvcbLop.DataSource = dt;
                    qlsvcbLop.DisplayMember = "TenLop";
                    qlsvcbLop.ValueMember = "MaLop";

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

        //Quản lí Khoa
        private bool checkDuplicateMakhoa(string strMakhoa)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select count(*) from Khoa where MaKhoa = @MaKhoa";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Makhoa", strMakhoa);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("DataBase Error: " + ex.Message);
                } finally
                {
                    conn.Close();
                }
            }
        }
        private void dataKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataKhoa.Rows[e.RowIndex];

                qlktxtMaKhoa.Text = row.Cells["MaKhoa"].Value.ToString();
                qlktxtTenKhoa.Text = row.Cells["TenKhoa"].Value.ToString();
            }
        }
        private void qlkbtnThemKhoa_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string Tenkhoa = qlktxtTenKhoa.Text.Trim();
            string Makhoa = qlktxtMaKhoa.Text.Trim().ToUpper();

            //Validate
            if (string.IsNullOrEmpty(Tenkhoa) || string.IsNullOrEmpty(Makhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Mã Khoa và Tên Khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkDuplicateMakhoa(Makhoa))
            {
                MessageBox.Show("Mã khoa đã tồn tại. Vui lòng nhập mã khác!");
                return;
            }

            //Thêm
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Insert into KHOA (MaKhoa, TenKhoa) values (@MaKhoa, @TenKhoa)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKhoa", Makhoa);
                    cmd.Parameters.AddWithValue("@TenKhoa", Tenkhoa);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm Khoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData_Khoa();
                        qlktxtMaKhoa.Clear();
                        qlktxtTenKhoa.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm khoa");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }


        }
        private void qlkbtnXoaKhoa_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maKhoa = qlktxtMaKhoa.Text.Trim();

            //Validate
            if (string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng chọn một Khoa để xoá!");
                return;
            }
            //Xoá
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Delete from KHOA where MaKhoa = @MaKhoa";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm thành công!");
                        LoadData_Khoa();
                        qlktxtMaKhoa.Clear();
                        qlktxtTenKhoa.Clear();
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
        private void qlkbtnSuaKhoa_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maKhoa = qlktxtMaKhoa.Text.Trim();
            string tenKhoa = qlktxtTenKhoa.Text.Trim();

            //Validate
            if (string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng điền Mã Khoa!");
                return;
            }
            if (string.IsNullOrEmpty(tenKhoa))
            {
                MessageBox.Show("Vui lòng điền Tên Khoa!");
                return;
            }

            //Sửa
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Update KHOA set TenKhoa = @TenKhoa where MaKhoa = @MaKhoa";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenKhoa", tenKhoa);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thành công!");
                        LoadData_Khoa();
                        qlktxtTenKhoa.Clear();
                        qlktxtMaKhoa.Clear();
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

        //Quản lí Môn Học
        private bool checkDuplicateMaMonHoc(string strMaMonHoc)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select count(*) from MONHOC where MaMonHoc = @MaMonHoc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaMonHoc", strMaMonHoc);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("DataBase Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void dataMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataMonHoc.Rows[e.RowIndex];
                qlmhtxtMaMonHoc.Text = row.Cells["MaMonHoc"].Value.ToString();
                qlmhtxtTenMonHoc.Text = row.Cells["TenMonHoc"].Value.ToString();
            }
        }
        private void qlmhcbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (qlmhcbKhoa.SelectedIndex == -1)
                return;

            string slectedKhoa = qlmhcbKhoa.SelectedValue.ToString();
            LoadData_MonHoc(slectedKhoa);
        }
        private void qlmhbtnThemMonHoc_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string Mamonhoc = qlmhtxtMaMonHoc.Text.Trim().ToUpper();
            string Tenmonhoc = qlmhtxtTenMonHoc.Text.Trim();
            string Makhoa = qlmhcbKhoa.SelectedValue.ToString();

            //Validate
            if (string.IsNullOrEmpty(Mamonhoc))
            {
                MessageBox.Show("Mã môn học không được để trống!");
                return;
            }
            if (string.IsNullOrEmpty(Tenmonhoc))
            {
                MessageBox.Show("Tên môn học không được để trống!");
                return;
            }
            if (string.IsNullOrEmpty(Makhoa))
            {
                MessageBox.Show("Vui lòng chọn khoa");
                return;
            }
            if (checkDuplicateMaMonHoc(Mamonhoc))
            {
                MessageBox.Show("Mã môn học đã bị trùng. Vui lòng nhập mã khác!");
                return;
            }

            //Thêm
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Insert into MONHOC (MaMonHoc, TenMonHoc, MaKhoa) values (@MaMonHoc, @TenMonHoc, @MaKhoa)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaMonHoc", Mamonhoc);
                    cmd.Parameters.AddWithValue("@TenMonHoc", Tenmonhoc);
                    cmd.Parameters.AddWithValue("@MaKhoa", Makhoa);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm môn học thành công!");
                        LoadData_MonHoc(Makhoa);
                        qlmhtxtTenMonHoc.Clear();
                        qlmhtxtMaMonHoc.Clear();
                    } else
                    {
                        MessageBox.Show("Lỗi khi thêm môn học!");
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
        private void qlmhbtnSuaMonHoc_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maMonHoc = qlmhtxtMaMonHoc.Text.Trim();
            string tenMonHoc = qlmhtxtTenMonHoc.Text.Trim();
            string maKhoa = qlmhcbKhoa.SelectedValue.ToString();

            //Validate
            if (string.IsNullOrEmpty(maMonHoc))
            {
                MessageBox.Show("Vui lòng điền mã môn học!");
                return;
            }
            if (string.IsNullOrEmpty(tenMonHoc))
            {
                MessageBox.Show("Vui lòng điền tên môn học!");
                return;
            }

            //Sửa
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Update MONHOC set TenMonHoc = @TenMonHoc where MaMonHoc = @MaMonHoc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenMonHoc", tenMonHoc);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa môn học thành công!");
                        LoadData_MonHoc(maKhoa);
                        qlmhtxtMaMonHoc.Clear();
                        qlmhtxtTenMonHoc.Clear();
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
        private void qlmhbtnXoaMonHoc_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maMonHoc = qlmhtxtMaMonHoc.Text.Trim();
            string maKhoa = qlmhcbKhoa.SelectedValue.ToString();

            //Validate
            if (string.IsNullOrEmpty(maMonHoc))
            {
                MessageBox.Show("Vui lòng chọn một môn học để xoá!");
                return;
            }

            //Xoá
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Delete from MONHOC where MaMonHoc = @MaMonHoc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xoá thành công!");
                        LoadData_MonHoc(maKhoa);
                        qlmhtxtTenMonHoc.Clear();
                        qlmhtxtMaMonHoc.Clear();

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

        //Quản lí lớp
        private bool checkDuplicateMaLop(string strMaLop)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select count(*) from LOP where MaLop = @MaLop";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLop", strMaLop);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("DataBase Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void qllcbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (qllcbKhoa.SelectedIndex == -1)
            {
                return;
            }
            string maKhoa = qllcbKhoa.SelectedValue.ToString();
            LoadData_Lop(maKhoa);
        }
        private void dataLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0 )
            {
                DataGridViewRow row = dataLop.Rows[e.RowIndex];
                qlltxtMaLop.Text = row.Cells["MaLop"].Value.ToString();
                qlltxtTenLop.Text = row.Cells["TenLop"].Value.ToString();
            }
        }
        private void qllbtnThemLop_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maLop = qlltxtMaLop.Text.Trim().ToUpper();
            string tenLop = qlltxtTenLop.Text.Trim();
            string maKhoa = qllcbKhoa.SelectedValue.ToString();

            //Validate
            if (string.IsNullOrEmpty(maLop))
            {
                MessageBox.Show("Vui lòng nhập mã lớp");
                return;
            }
            if (string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Vui lòng nhập tên lớp");
                return;
            }
            if (string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng chọn khoa");
                return;
            }
            if (checkDuplicateMaLop(maLop)) {
                MessageBox.Show("Mã lớp đã bị trùng. Vui lòng nhập lại!");
                return;
            }
        
            //Thêm
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Insert into LOP (MaLop, TenLop, MaKhoa) values (@MaLop, @TenLop, @MaKhoa)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@TenLop", tenLop);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0 )
                    {
                        MessageBox.Show("Thêm lớp thành công!");
                        LoadData_Lop(maKhoa);
                        Console.WriteLine(maKhoa);
                        qlltxtTenLop.Clear();
                        qlltxtMaLop.Clear();
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
        private void qllbtnSuaLop_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maLop = qlltxtMaLop.Text.Trim();
            string tenLop = qlltxtTenLop.Text.Trim();
            string maKhoa = qllcbKhoa.SelectedValue.ToString();

            //Validate
            if (string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Vui lòng điền tên lớp!");
                return;
            }

            //Sửa
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Update LOP set TenLop = @TenLop, MaKhoa = @MaKhoa where MaLop = @MaLop";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenLop", tenLop);
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@MaLop", maLop);

                    int rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected > 0 )
                    {
                        MessageBox.Show("Sửa thành công!");
                        LoadData_Lop(maKhoa);
                        qlltxtMaLop.Clear();
                        qlltxtTenLop.Clear();
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
        private void qllbtnXoaLop_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maLop = qlltxtMaLop.Text.Trim();
            string maKhoa = qllcbKhoa.SelectedValue.ToString();

            //Validate
            if (string.IsNullOrEmpty(maLop))
            {
                MessageBox.Show("Vui lòng chọn một lớp để xoá!");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có muốn xoá Lớp này?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) 
            {
                return;
            }

            //Xoá
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Delete from LOP where MaLop = @MaLop";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0) 
                    {
                        MessageBox.Show("Xoá thành công");
                        LoadData_Lop(maKhoa);
                        qlltxtMaLop.Clear();
                        qlltxtTenLop.Clear();
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

        //Quản lí sinh viên
        private void qlsvcbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(qlsvcbKhoa.SelectedIndex == -1)
            {
                return;
            }
            string maKhoa = qlsvcbKhoa.SelectedValue.ToString();
            LoadComboBox_Lop(maKhoa);
        }
        private void qlsvcbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(qlsvcbLop.SelectedIndex == -1)
            {
                return;
            }
            string maLop = qlsvcbLop.SelectedValue.ToString();
            LoadData_SinhVien(maLop);           
        }
        private bool checkDuplicateMaSV(string strMaSV)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Select count(*) from SINHVIEN where MaSinhVien = @MaSinhVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSinhVien", strMaSV);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("DataBase Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void dataSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataSinhVien.Rows[e.RowIndex];
                qlsvtxtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                qlsvtxtMaSV.Text = row.Cells["MaSinhVien"].Value.ToString();
                qlsvcbGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                if (row.Cells["NgaySinh"].Value != null) 
                {
                    DateTime ngaySinh;
                    if (DateTime.TryParse(row.Cells["NgaySinh"].Value.ToString(), out ngaySinh))
                    {
                        qlsvdateNgaySinh.Value = ngaySinh;
                    }
                }
                qlsvtxtQueQuan.Text = row.Cells["QueQuan"].Value.ToString();

                //string maLop = row.Cells["MaLop"].Value.ToString();
                //foreach (DataRowView item in qlsvcbLop.Items)
                //{
                //    if (item["MaLop"].ToString() == maLop)
                //    {
                //        qlsvcbLop.SelectedItem = item;
                //        break;
                //    }
                //}
            }
        }
        private void qlsvbtnThemSV_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string hoTen = qlsvtxtHoTen.Text.Trim();
            string maSV = qlsvtxtMaSV.Text.Trim().ToUpper();
            string gioiTinh = qlsvcbGioiTinh.SelectedItem.ToString();
            string ngaySinh = qlsvdateNgaySinh.Value.ToString("yyyy-MM-dd");
            string queQuan = qlsvtxtQueQuan.Text.Trim();
            string maLop = qlsvcbLop.SelectedValue.ToString();

            //Validate
            if(string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng điền họ tên!");
                return;
            }
            if (string.IsNullOrEmpty(maSV)) 
            {
                MessageBox.Show("Vui lòng điền Mã Sinh Viên");
                return;
            }
            if (string.IsNullOrEmpty(gioiTinh)) 
            {
                MessageBox.Show("Vui lòng chọn giới tính");
                return;
            }
            if(string.IsNullOrEmpty(queQuan))
            {
                MessageBox.Show("Vui lòng điền quê quán!");
                return;
            }
            if (checkDuplicateMaSV(maSV))
            {
                MessageBox.Show("Mã sinh viên đã bị trùng. Vui lòng nhập mã khác!");
                return;
            }

            //Thêm
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Insert into SINHVIEN (MaSinhVien, HoTen, GioiTinh, NgaySinh, QueQuan, MaLop, MatKhau) values (@MaSinhVien, @HoTen, @GioiTinh, @NgaySinh, @QueQuan, @MaLop, @MatKhau)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSV);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@QueQuan", queQuan);
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@MatKhau", 1);

                    int rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected > 0) 
                    {
                        MessageBox.Show("Thêm sinh viên thành công!");
                        LoadData_SinhVien(maLop);
                        qlsvtxtHoTen.Clear();
                        qlsvtxtMaSV.Clear();
                        qlsvcbGioiTinh.SelectedIndex = 0;
                        qlsvtxtQueQuan.Clear();                        
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
        private void qlsvbtnSuaSV_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string hoTen = qlsvtxtHoTen.Text.Trim();
            string maSV = qlsvtxtMaSV.Text.Trim().ToUpper();
            string gioiTinh = qlsvcbGioiTinh.SelectedItem.ToString();
            string ngaySinh = qlsvdateNgaySinh.Value.ToString("yyyy-MM-dd");
            string queQuan = qlsvtxtQueQuan.Text.Trim();
            string maLop = qlsvcbLop.SelectedValue.ToString();

            //Validate
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng điền họ tên!");
                return;
            }
            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng điền Mã Sinh Viên");
                return;
            }
            if (string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính");
                return;
            }
            if (string.IsNullOrEmpty(queQuan))
            {
                MessageBox.Show("Vui lòng điền quê quán!");
                return;
            }      

            //Sửa
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Update SINHVIEN set HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, QueQuan = @QueQuan, MaLop = @MaLop where MaSinhVien = @MaSinhVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@QueQuan", queQuan);
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSV);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0) 
                    {
                        MessageBox.Show("Sửa thành công sinh viên " + hoTen);
                        LoadData_SinhVien(maLop);
                        qlsvtxtHoTen.Clear();
                        qlsvtxtMaSV.Clear();
                        qlsvcbGioiTinh.SelectedIndex = 0;
                        qlsvtxtQueQuan.Clear();
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
        private void qlsvbtnXoaSV_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu
            string maLop = qlsvcbLop.SelectedValue.ToString();
            string maSinhVien = qlsvtxtMaSV.Text.Trim();
            string hoTen = qlsvtxtHoTen.Text.Trim();

            //Validate
            if (string.IsNullOrEmpty(maSinhVien))
            {
                MessageBox.Show("Vui lòng chọn một Sinh Viên để xoá!");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có muốn xoá " +hoTen+" ?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            //Xoá
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "Delete SINHVIEN where MaSinhVien = @MaSinhVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSinhVien", maSinhVien);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0) 
                    {
                        MessageBox.Show("Xoá sinh viên " + hoTen + "thành công!");
                        LoadData_SinhVien(maLop);
                        qlsvtxtHoTen.Clear();
                        qlsvtxtMaSV.Clear();
                        qlsvcbGioiTinh.SelectedIndex = 0;
                        qlsvtxtQueQuan.Clear();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
    }
}
