using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSellingToy.Models
{
    public class GioHang
    {
        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();

        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien {
            get { return iSoLuong * dDonGia; }
        }

        public GioHang(int MaSP)
        {
            iMaSP = MaSP;
            SanPham sp = data.SanPhams.Single(n => n.MaSanPham == iMaSP);
            
            sTenSP = sp.TenSanPham;
            sHinhAnh = sp.HinhAnh;
            dDonGia = double.Parse(sp.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}