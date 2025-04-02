using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.PropertyGridInternal;

namespace ThiTracNghiem.Class
{
    public class CGiangVien
    {
        /** 
         * 
        MaGiangVien VARCHAR(10) PRIMARY KEY,
        HoTen NVARCHAR(50),
        GioiTinh NVARCHAR(10),
        NgaySinh DATE,
        QueQuan NVARCHAR(50),
        MatKhau VARCHAR(50),
        MaKhoa VARCHAR(10)y
        **/
        
        public string MaGiangVien {  get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string MatKhau { get; set; }
        public string MaKhoa { get; set; }


    }
}
