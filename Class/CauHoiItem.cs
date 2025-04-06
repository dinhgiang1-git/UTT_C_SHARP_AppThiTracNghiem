using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiTracNghiem
{
    internal class CauHoiItem
    {
        public string Display { get; set; }
        public string Value { get; set; }  // MaCauHoi
        public override string ToString()
        {
            return Display;
        }
    }
}
