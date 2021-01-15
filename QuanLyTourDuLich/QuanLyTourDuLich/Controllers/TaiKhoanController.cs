using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.Controllers
{
    public class TaiKhoanController : Controller
    {
        //
        // GET: /TaiKhoan/
        QuanLyTourDataContext data = new QuanLyTourDataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection c)
        {
            TaiKhoan tk = new TaiKhoan();
            KhachHang kh = new KhachHang();
            var hoten = c["Hoten"];
            string tendn = c["TenDN"];
            string password = c["Password"];
            var gioitinh = c["GioiTinh"];
            var cmnd = c["Sochungminh"];
            var diachi = c["Diachi"];
            var phone = c["Dienthoai"];
            if (string.IsNullOrEmpty(hoten))
                ViewData["Error1"] = "Họ tên không được bỏ trống";
            if (string.IsNullOrEmpty(tendn))
                ViewData["Error2"] = "Tên đăng nhập không được bỏ trống";
            if (string.IsNullOrEmpty(password))
                ViewData["Error3"] = "Password không được bỏ trống";
            if (string.IsNullOrEmpty(gioitinh))
                ViewData["Error4"] = "gioi tinh khong dk bo trong";
            if (string.IsNullOrEmpty(cmnd))
                ViewBag["Error5"] = "cmnd khong dk bo trong";
            if (string.IsNullOrEmpty(diachi))
                ViewBag["Error6"] = "dia chi khong dk bo trong";
            if (string.IsNullOrEmpty(phone))
                ViewBag["Error7"] = "so dien thoai khong duoc bo trong";
            else
            {
                kh.TenKhachHang = hoten;
                tk.TenDangNhap = tendn;
                tk.MatKhau = password;
                kh.GioiTinh = gioitinh;
                kh.CMND = cmnd;
                kh.DiaChi = diachi;
                kh.SDT = phone;
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                KhachHang khNew = data.KhachHangs.Where(t => t.TenKhachHang.Contains(hoten)).FirstOrDefault();
                tk.MaSo = khNew.MaSoKhachHang;
                data.TaiKhoans.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XacNhanDangNhap(FormCollection col)
        {
            TaiKhoan tk = data.TaiKhoans.FirstOrDefault(t => t.TenDangNhap == col["txtTen"] && t.MatKhau == col["txtMatKhau"]);
            if (tk != null)
            {
                Session["tk"] = tk;
                return RedirectToAction("TrangChu", "Home");
            }
            return View();
        }

    }
}
