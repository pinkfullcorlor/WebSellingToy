using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;

namespace WebSellingToy.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{

        //    return View();
        //}

        public HomeController()
        {
           

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();

        private List<LoaiHangHoa> LayDSLoai()
        {
            return data.LoaiHangHoas.ToList();
        }

        public ActionResult Index()
        {


            //ViewBag.Message = "Welcome to my demo!";
            ViewData["Loais"] = LayDSLoai();

            return View();
        }


        //dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();

        //private List<LoaiHangHoa> LayDSLoai()
        //{
        //    return data.LoaiHangHoas.ToList();
        //}

        //public ActionResult RenderDSLoai()
        //{
        //    var loai = LayDSLoai();

        //    return View(loai);
        //}











    }
}