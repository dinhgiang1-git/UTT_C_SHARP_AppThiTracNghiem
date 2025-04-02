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
            LoadData_Khoa();
            LoadData_MonHoc("");
            LoadComboBox();
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
        private void LoadComboBox()
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

                    qlmhcbKhoa.DataSource = dt;
                    qlmhcbKhoa.DisplayMember = "TenKhoa";
                    qlmhcbKhoa.ValueMember = "MaKhoa";
                }
                catch (Exception ex)
                {
                    throw new Exception ("Error: "+ ex.Message);
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
                }finally
                {
                    conn.Close();
                }
            }
        }
        private void dataKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
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
            if(checkDuplicateMakhoa(Makhoa))
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
            if(string.IsNullOrEmpty(maKhoa))
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
            if(string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng điền Mã Khoa!");
                return;
            }
            if(string.IsNullOrEmpty (tenKhoa))
            {
                MessageBox.Show("Vui lòng điền Tên Khoa!");
                return;
            }

            //Sửa
            using(SqlConnection conn = new SqlConnection(strConn))
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
                    throw new Exception("Error: "+ ex.Message);
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
            if(e.RowIndex >= 0)
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
            if(string.IsNullOrEmpty(maMonHoc))
            {
                MessageBox.Show("Vui lòng điền mã môn học!");
                return;
            }
            if(string.IsNullOrEmpty(tenMonHoc))
            {
                MessageBox.Show("Vui lòng điền tên môn học!");
                return;
            }

            //Sửa
            using(SqlConnection conn = new SqlConnection(strConn))
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
            if(string.IsNullOrEmpty(maMonHoc))
            {
                MessageBox.Show("Vui lòng chọn một môn học để xoá!");
                return;
            }

            //Xoá
            using(SqlConnection conn = new SqlConnection(strConn))
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
    }
}
