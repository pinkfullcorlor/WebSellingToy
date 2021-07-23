using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSellingToy.Models
{
    public class ThongKeDoanhSo
    {
        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();

        public int iMaHoaDon { get; set; }
        public DateTime dtNgayLap { get; set; }
        public int iMaSanPham { get; set; }
        public int iSoLuongBan { get; set; }
        public double dTongTien { get; set; }

        //public ThongKeDoanhSo(int MaHoaDon)
        //{
        //    iMaHoaDon = MaHoaDon;
        //    DSBanHang dsBanHang = data.DSBanHangs.Single(n => n.MaHoaDon == MaHoaDon);

        //    iMaSanPham = dsBanHang.MaSanPham;
        //    iSoLuongBan = dsBanHang.SLBan;
        //    dTongTien = dsBanHang.TongTien;



        //}
    }
}