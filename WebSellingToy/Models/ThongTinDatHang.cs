using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSellingToy.Models
{
    public class ThongTinDatHang
    {
        public string sTenKhachHang { get; set; }
        public string sSoDienThoai { get; set; }
        public string sDiaChi { get; set; }
        public string sDiaChiNhanHang { get; set; }

        public ThongTinDatHang() {
            sTenKhachHang = "Trong";
            sSoDienThoai = "Trong";
            sDiaChi = "Trong";
            sDiaChiNhanHang = "Trong";
        }
    }
}