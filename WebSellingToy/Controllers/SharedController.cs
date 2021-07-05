using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;

namespace WebSellingToy.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();

        private List<LoaiHangHoa> LayDSLoai()
        {
            return data.LoaiHangHoas.ToList();
        }

        private List<SanPham> LayDSSP()
        {
            return data.SanPhams.ToList();
        }
        //public ActionResult PartialView()
        //{
        //    ViewBag.Message = "Hello this is Partial View";
        //    return View();
        //}

        public ActionResult _Layout()
        {


            //ViewBag.Message = "Welcome to my demo!";
            ViewData["Loais"] = LayDSLoai();
            
            return View();
        }

        

        public ActionResult RenderDSLoai()
        {
            var loai = LayDSLoai();

            return PartialView(loai);
        }
    }
}