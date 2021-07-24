using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;

namespace WebSellingToy.Controllers
{
    public class DanhMucDoChoiController : Controller
    {
        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();
        // GET: DanhMucDoChoi
        private List<SanPham> DMSP()
        {
            return data.SanPhams.ToList();
        }
     
        public ActionResult MoHinh()
        {
            var sanpham = from s in data.SanPhams
                          where s.MaLoai == "MT"
                          select s;

            return View(sanpham);

        }
        public ActionResult Figure()
        {
            var sanpham = from s in data.SanPhams
                          where s.MaLoai == "FI"
                          select s;

            return View(sanpham);

        }
        public ActionResult Puzzel()
        {
            var sanpham = from s in data.SanPhams
                          where s.MaLoai == "PUZ"
                          select s;

            return View(sanpham);

        }
        public ActionResult TECH()
        {
            var sanpham = from s in data.SanPhams
                          where s.MaLoai == "TECH"
                          select s;

            return View(sanpham);

        }
    }
}