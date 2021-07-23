using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;

namespace WebSellingToy.Controllers
{
    public class GioHangController : Controller
    {
        dbSQLTiemDoChoiDataContext data = new dbSQLTiemDoChoiDataContext();

        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.iMaSP == iMaSP);
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "NguoiMuaHang");
            }
            else if (sanpham == null)
            {
                sanpham = new GioHang(iMaSP);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
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

        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }

        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        public ActionResult XoaGioHang(int iMaSanPham)
        {
            List<GioHang> gioHangs = LayGioHang();

            GioHang gh = gioHangs.SingleOrDefault(n => n.iMaSP == iMaSanPham);

            if (gh != null)
            {
                gioHangs.RemoveAll(n => n.iMaSP == iMaSanPham);
                return RedirectToAction("GioHang");
            }
            if (gioHangs.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(int iMaSanPham, FormCollection collection)
        {
            List<GioHang> gioHangs = LayGioHang();
            GioHang gh = gioHangs.SingleOrDefault(n => n.iMaSP == iMaSanPham);
            if (gh == null)
            {
                return RedirectToAction("GioHang");
            }
            else if (gh != null)
            {
                gh.iSoLuong = int.Parse(collection["txtSoLuong"]);
            }

            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> gioHangs = LayGioHang();
            gioHangs.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<GioHang> gioHangs = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(gioHangs);

        }

        public ActionResult DatHang(FormCollection collection)
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            HoaDonBanHang hdBanHang = new HoaDonBanHang();
            CTHoaDon ctHoaDon = new CTHoaDon();

            KhachHang kh = Session["Taikhoan"] as KhachHang;
            List<GioHang> gioHangs = LayGioHang();

            var NgayLapHD = string.Format("{0:MM/dd/yyyy}", collection["NgayLapHD"]);
            var DiaChiNhanHang = collection["DiaChiNhanHang"];

            hdBanHang.TinhTrangThanhToan = false;
            hdBanHang.DiaChiNhanHang = DiaChiNhanHang;
            hdBanHang.NgayLap = DateTime.Now;

            data.HoaDonBanHangs.InsertOnSubmit(hdBanHang);
            data.SubmitChanges();

            foreach (var item in gioHangs)
            {
                DSBanHang dsBanHang = new DSBanHang();
                dsBanHang.MaHoaDon = hdBanHang.MaHoaDon;
                dsBanHang.MaSanPham = item.iMaSP;
                dsBanHang.SLBan = item.iSoLuong;
                dsBanHang.TongTien = int.Parse(item.dDonGia.ToString());

                data.DSBanHangs.InsertOnSubmit(dsBanHang);


            }
            ctHoaDon.MaHoaDon = hdBanHang.MaHoaDon;
            ctHoaDon.MaKhachHang = kh.MaKhachHang;
            data.CTHoaDons.InsertOnSubmit(ctHoaDon);

            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");

        }

        public ActionResult XacNhanDonHang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}