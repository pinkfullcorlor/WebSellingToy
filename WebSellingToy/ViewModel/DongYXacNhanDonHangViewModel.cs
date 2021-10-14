using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSellingToy.Models;

namespace WebSellingToy.ViewModel
{
    public class DongYXacNhanDonHangViewModel
    {
        public ThongTinDatHang viewmodelThongTinDatHang { get; set; }
        public IEnumerable<DSBanHang> viewmodelDSBanHang { get; set; }
        public IEnumerable<ThongTinSanPhamDatHang> viewmodelThongTinSP { get; set; }
    }
}