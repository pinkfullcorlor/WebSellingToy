using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSellingToy.Models
{
    public class XemDSHangHoaController : Controller
    {
        // GET: XemDSHangHoa
        public ActionResult Index()
        {
            return View();
        }

        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();

        private List<SanPham> LaySanPham(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.NgaySanXuat).Take(count).ToList();
        }

        public ActionResult XemDSHangHoa()
        {
            var dsSanPham = LaySanPham(5);
            return View(dsSanPham);

        }

        [HttpGet]
        public ActionResult ThemSpMoi()
        {
            return View();
        }
    }
}