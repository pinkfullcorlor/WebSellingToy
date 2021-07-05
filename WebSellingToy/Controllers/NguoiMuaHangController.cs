using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;

namespace WebSellingToy.Controllers
{
    public class NguoiMuaHangController : Controller
    {
        // GET: NguoiMuaHang
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        dbSQLTiemDoChoiDataContext db = new dbSQLTiemDoChoiDataContext();

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["TenKhachHang"];
            var tendn = collection["TenDangNhapKH"];
            var matkhau = collection["MatKhau"];
            var nhaplaimk = collection["NhapLaiMK"];
            var diachi = collection["DiaChi"];
            var ngaysinh = string.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            var sodienthoai = collection["SoDienThoai"];
            //if (String.IsNullOrEmpty(hoten))
            //{
            //    ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            //}
            //else if (String.IsNullOrEmpty(tendn))
            //{
            //    ViewData["Loi2"] = "Tên đăng nhập không được để trống";
            //}
            //else if (String.IsNullOrEmpty(matkhau))
            //{
            //    ViewData["Loi3"] = "Mật khẩu không được để trống";
            //}
            //else
            //{
            //    kh.TenKhachHang = hoten;
            //    kh.TenDangNhapKH = tendn;
            //    kh.MatKhau = matkhau;
            //    kh.DiaChi = diachi;
            //    kh.NgaySinh = DateTime.Parse(ngaysinh);
            //    kh.SoDienThoai = sodienthoai;
            //    db.KhachHangs.InsertOnSubmit(kh);
            //    db.SubmitChanges();
            //    return RedirectToAction("Home","Index");

            //}


            kh.TenKhachHang = hoten;
            kh.TenDangNhapKH = tendn;
            kh.MatKhau = matkhau;
            kh.DiaChi = diachi;
            kh.NgaySinh = DateTime.Parse(ngaysinh);
            kh.SoDienThoai = sodienthoai;
            db.KhachHangs.InsertOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Home", "Index");

            return this.DangKy();
        }

        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";

            }
            else
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TenDangNhapKH == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công!!!";
                    Session["Taikhoan"] = kh;
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";

            }
;
            return View();
        }


    }
}