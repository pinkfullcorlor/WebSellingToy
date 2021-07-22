using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;
using WebSellingToy.ViewModel;

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

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }


        
        public ActionResult _UserLayout()
        {
            
            return View();
        }
        public ActionResult LogOut()
        {
            if (Session["Taikhoan"] != null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
    }
}