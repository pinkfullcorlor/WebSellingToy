using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;

namespace WebSellingToy.Controllers
{
    public class MuaHangController : Controller
    {

        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();
        // GET: MuaHang

        private List<SanPham> LayDSSP()
        {
            return data.SanPhams.ToList();
        }
        public ActionResult MuaHang()
        {
            var sanpham = LayDSSP();
            return View(sanpham);
        }

        public ActionResult ChiTietSanPham(int id)
        {
            var sanpham = from s in data.SanPhams
                          where s.MaSanPham == id
                          select s;

            return View(sanpham.Single());

        }

        

        

    }
}