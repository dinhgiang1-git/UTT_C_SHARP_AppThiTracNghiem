using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class thongTinTaiKhoan : Form
    {
        string maSV_GV = "";
        public thongTinTaiKhoan(string id)
        {
            InitializeComponent();
            maSV_GV = id;
        }
    }
}
