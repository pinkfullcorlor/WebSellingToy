using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;

namespace WebSellingToy.Controllers
{
    
    public class LoaiDoChoiController : Controller
    {
        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();
        private List<SanPham>DSSP()
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
    }
}