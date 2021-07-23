using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSellingToy.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace WebSellingToy.Controllers
{
    public class AdminController : Controller
    {
        dbSQLTiemDoChoiDataContext db = new dbSQLTiemDoChoiDataContext();
        // GET: Admin

        public ActionResult Index()
        {
            if (Session["TaikhoanAdmin"] == null)
            {

                return RedirectToAction("DangNhapAdmin", "Admin");
            }

            return View();
        }

        [HttpGet]
        public ActionResult DangNhapAdmin()
        {
            if (Session["TaikhoanAdmin"] != null)
            {

                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        [HttpPost]
        public ActionResult DangNhapAdmin(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];

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
                NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNhanVien == tendn && n.MKTruyCap == matkhau /*&& n.MaChucVu == "ADMIN"*/);
                if (nv != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công!!!";
                    Session["TaikhoanAdmin"] = nv;
                    return RedirectToAction("Index", "Admin");

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";

            }
            return View();
        }


        public ActionResult DangXuatAdmin()
        {
            if (Session["TaikhoanAdmin"] != null)
            {
                Session.Remove("TaikhoanAdmin");
                return RedirectToAction("DangNhapAdmin", "Admin");
            }

            return View();
        }

        public ActionResult QuanLySanPham(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {

                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            return View(db.SanPhams.ToList().OrderBy(n => n.MaSanPham).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoiSanPham()
        {
            if (Session["TaikhoanAdmin"] == null)
            {

                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangHoas.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSanPham(SanPham sp, HttpPostedFileBase fileupload)
        {
            ViewBag.MaLoai = new SelectList(db.LoaiHangHoas.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");

            if (fileupload == null)
            {
                ViewBag.ThongBaoTonTaiHinhAnh = "Vui lòng chọn đường dẫn cho ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);

                    var path = Path.Combine(Server.MapPath("~/HinhAnhSanPham"), filename);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBaoTonTaiHinhAnh = "Hình ảnh đã tồn tại hoặc đã trùng tên!";
                    }
                    else
                    {
                        //Lưu hình ảnh vào server
                        fileupload.SaveAs(path);
                    }
                    sp.HinhAnh = filename;

                    db.SanPhams.InsertOnSubmit(sp);
                    db.SubmitChanges();



                }
                return RedirectToAction("QuanLySanPham");

            }
        }

        [HttpGet]
        public ActionResult Xoa1SanPham(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == id);
            ViewBag.MaSanPham = sp.MaSanPham;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }


        [HttpPost, ActionName("Xoa1SanPham")]
        public ActionResult XacNhanXoaSP(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == id);
            ViewBag.MaSanPham = sp.MaSanPham;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("QuanLySanPham");
        }

        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {

            if (Session["TaikhoanAdmin"] == null)
            {

                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == id);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangHoas.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", sp.MaLoai);
            return View(sp);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(int id, SanPham sp, HttpPostedFileBase fileupload)
        {


            ViewBag.MaLoai = new SelectList(db.LoaiHangHoas.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");

            if (fileupload == null)
            {
                ViewBag.ThongBaoTonTaiHinhAnh = "Vui lòng chọn đường dẫn cho ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);

                    var path = Path.Combine(Server.MapPath("~/HinhAnhSanPham"), filename);

                    if (filename == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBaoTonTaiHinhAnh = "Hình ảnh đã tồn tại hoặc đã trùng tên!";
                    }
                    else
                    {
                        //Lưu hình ảnh vào server
                        fileupload.SaveAs(path);
                    }
                    sp.HinhAnh = filename;

                    SanPham sp1 = db.SanPhams.SingleOrDefault(n => n.MaSanPham == id);
                    sp1.HinhAnh = sp.HinhAnh;
                    //sp1.TenSanPham = sp.TenSanPham;
                    //sp1.MaLoai = sp.MaLoai;
                    //sp1.XuatXu = sp.XuatXu;
                    //sp1.DonViTinh = sp.DonViTinh;
                    //sp1.SLTon = sp.SLTon;

                    //sp1.HinhAnh = sp.HinhAnh;

                    //db.SubmitChanges();

                    //////UpdateModel(sp,"SanPham");
                    ////db.SanPhams.InsertOnSubmit(sp);

                    ////db.SubmitChanges();
                    ///

                    UpdateModel(sp1);

                    db.SubmitChanges();



                }
                return RedirectToAction("QuanLySanPham", "Admin");

            }

            //UpdateModel(sp);
            //db.SubmitChanges();

            //return RedirectToAction("QuanLySanPham", "Admin");

        }

        //[HttpPost, ActionName("SuaSanPham")]
        //public ActionResult CapNhatSP(int id)
        //{
        //    SanPham sanPham = db.SanPhams.Where(n => n.MaSanPham == id).SingleOrDefault();
        //    UpdateModel(sanPham);
        //    db.SubmitChanges();
        //    return RedirectToAction("QuanLySanPham", "Admin");
        //}

        public ActionResult QuanLyAdmin(int? page)
        {
            NhanVien snhanvien = Session["TaikhoanAdmin"] as NhanVien;
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            else if (snhanvien.MaChucVu != "ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            return View(db.NhanViens.ToList().OrderBy(n => n.MaNhanVien).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoiAdmin()
        {
            NhanVien snhanvien = Session["TaikhoanAdmin"] as NhanVien;
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            else if (snhanvien.MaChucVu != "ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.MaChucVu = new SelectList(db.QuyenTruyCaps.ToList().OrderBy(n => n.MaChucVu), "MaChucVu", "TenChucVu");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiAdmin(NhanVien nv)
        {
            NhanVien nhapnv = nv;
            ViewBag.MaChucVu = new SelectList(db.QuyenTruyCaps.ToList().OrderBy(n => n.MaChucVu), "MaChucVu", "TenChucVu");
            NhanVien nhanvientest = db.NhanViens.SingleOrDefault(n => n.MaNhanVien == nv.MaNhanVien);
            if (nhanvientest != null)
            {
                ViewData["LoiMaNhanVien"] = "Mã nhân viên đã tồn tại, vui lòng nhập mã khác!!";
                return View();
            }
            else if (nhanvientest == null)
            {

                db.NhanViens.InsertOnSubmit(nhapnv);
                db.SubmitChanges();
                return RedirectToAction("QuanLyAdmin");

            }


            //db.NhanViens.InsertOnSubmit(nv);
            //db.SubmitChanges();
            //return RedirectToAction("QuanLyAdmin");

            return View();
        }

        [HttpGet]
        public ActionResult EditAdmin(string id)
        {
            NhanVien snhanvien = Session["TaikhoanAdmin"] as NhanVien;
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            else if (snhanvien.MaChucVu != "ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }
            NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNhanVien == id);

            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChucVu = new SelectList(db.QuyenTruyCaps.ToList().OrderBy(n => n.MaChucVu), "MaChucVu", "TenChucVu", nv.MaChucVu);
            return View(nv);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditAdmin(NhanVien nv)
        {
            ViewBag.MaChucVu = new SelectList(db.QuyenTruyCaps.ToList().OrderBy(n => n.MaChucVu), "MaChucVu", "TenChucVu");

            NhanVien nvtest = db.NhanViens.FirstOrDefault(n => n.MaNhanVien == nv.MaNhanVien);

            UpdateModel(nvtest);
            db.SubmitChanges();

            return RedirectToAction("QuanLyAdmin");
        }

        [HttpGet]
        public ActionResult Xoa1NhanVien(string id)
        {
            NhanVien snhanvien = Session["TaikhoanAdmin"] as NhanVien;
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            else if (snhanvien.MaChucVu != "ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }
            NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNhanVien == id);
            ViewBag.MaNhanVien = nv.MaNhanVien;
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nv);
        }


        [HttpPost, ActionName("Xoa1NhanVien")]
        public ActionResult XacNhanXoaNhanVien(string id)
        {
            NhanVien snhanvien = Session["TaikhoanAdmin"] as NhanVien;
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            else if (snhanvien.MaChucVu != "ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }
            NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNhanVien == id);
            ViewBag.MaNhanVien = nv.MaNhanVien;
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhanViens.DeleteOnSubmit(nv);
            db.SubmitChanges();
            return RedirectToAction("QuanLyAdmin");
        }

        public ActionResult ThongKeDoanhSo()
        {
            var model = new NgayThongKe();
            return View(model);
        }

        [HttpPost]
        public ActionResult ThongKeDoanhSo(NgayThongKe ngaytk)
        {

            if (Session["TaikhoanAdmin"] == null)
            {

                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            Session["ngaybd"] = ngaytk.dtTuNgay;
            Session["ngaykt"] = ngaytk.dtDenNgay;


            return RedirectToAction("BangThongKeDS", "Admin");
        }


        public List<ThongKeDoanhSo> lstThongKeDoanhSo(DateTime tungay, DateTime denngay)
        {
            var model = from a in db.HoaDonBanHangs
                        join b in db.DSBanHangs
                        on a.MaHoaDon equals b.MaHoaDon
                        where a.NgayLap >= tungay
                        where a.NgayLap <= denngay
                        select new ThongKeDoanhSo()
                        {
                            iMaHoaDon = a.MaHoaDon,
                            dtNgayLap = a.NgayLap,
                            iMaSanPham = b.MaSanPham,
                            iSoLuongBan = b.SLBan,
                            dTongTien = b.TongTien,
                        };
            return model.OrderByDescending(x => x.iMaHoaDon).ToList();


        }


        public double TongDoanhThu(List<ThongKeDoanhSo> lst)
        {
            double tongdoanhthu = 0;
            //List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                tongdoanhthu = lst.Sum(n => n.dTongTien);
            }
            return tongdoanhthu;
        }
        //DateTime tungay, denngay;
        //NgayThongKe ngay = new NgayThongKe();
        public ActionResult BangThongKeDS(NgayThongKe ngaytk)
        {

            ViewBag.TuNgay = ngaytk.dtTuNgay;
            ViewBag.DenNgay = ngaytk.dtDenNgay;


            DateTime ngay1 = Convert.ToDateTime(Session["ngaybd"].ToString());
            DateTime ngay2 = Convert.ToDateTime(Session["ngaykt"].ToString());

            ViewBag.TongDoanhThu = TongDoanhThu(lstThongKeDoanhSo(ngay1, ngay2));

            return View(lstThongKeDoanhSo(ngay1, ngay2));
            //return View();
        }


        public ActionResult DoiMatKhauAdmin(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            //NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNhanVien == id);


            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DoiMatKhauAdmin(DoiMatKhau matkhau, string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            NhanVien nv = db.NhanViens.SingleOrDefault(n => n.MaNhanVien == id);
            if (matkhau.MKCu != nv.MKTruyCap)
            {
                ViewData["Loi"] = "Nhập sai mật khẩu cũ!!!";
            }
            else if (matkhau.MKMoi != matkhau.NhapLaiMK)
            {
                ViewData["Loi"] = "Nhập lại mật khẩu bị sai!!!";
            }
            else
            {
                nv.MKTruyCap = matkhau.NhapLaiMK;
                UpdateModel(nv);
                db.SubmitChanges();
                return RedirectToAction("DangNhapAdmin", "Admin");

            }

            return View();

            
        }



    }
}