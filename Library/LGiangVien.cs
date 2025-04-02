using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ThiTracNghiem.Class;
using System.Data.SqlClient;

namespace ThiTracNghiem.Library
{
    public class LGiangVien
    {
        public static bool IsExitsAccount(CGiangVien gv)
        {
            //Thực kiện kết nối cơ sở dữ liệu.
            string strConn = "Server=DINHDUCGIANG;Database=UTT_ThiTracNghiem;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(strConn);
            string querry = @"Select count(*) from GIANGVIEN where MaGiangVien = @MaGiangVien and MatKhau = @MatKhau";

            try
            {
                conn.Open();  
                SqlCommand cmd = new SqlCommand(querry, conn);
                cmd.Parameters.AddWithValue("@MaGiangVien", gv.MaGiangVien);
                cmd.Parameters.AddWithValue("@MatKhau", gv.MatKhau);

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
               throw new Exception("Database error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }            
    }
}
