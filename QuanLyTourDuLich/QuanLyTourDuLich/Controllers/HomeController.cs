using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        QuanLyTourDataContext data = new QuanLyTourDataContext();
        public ActionResult TrangChu()
        {
            List<TOUR> lstTrongNuoc = data.TOURs.Where(t => t.MaLoaiTour == 1).Take(6).ToList();
            List<TOUR> lstNgoaiNuoc = data.TOURs.Where(t => t.MaLoaiTour == 2).Take(6).ToList();
            ViewBag.lstTrongNuoc = lstTrongNuoc;
            ViewBag.lstNgoaiNuoc = lstNgoaiNuoc;
            return View();
        }
        public ActionResult DanhMucTourTrongNuoc()
        {
            List<TOUR> lstTrongNuoc = data.TOURs.Where(t => t.MaLoaiTour == 1).ToList();
            return PartialView(lstTrongNuoc);
        }
        public ActionResult DanhMucTourNgoaiNuoc()
        {
            List<TOUR> lstNgoaiNuoc = data.TOURs.Where(t => t.MaLoaiTour == 2).ToList();
            return PartialView(lstNgoaiNuoc);
        }
        public ActionResult LayOutSanPham()
        {
            GioHang gio = Session["gh"] as GioHang;
            return PartialView(gio);
        }
        public ActionResult LayOutUser()
        {
            TaiKhoan taikhoan = Session["tk"] as TaiKhoan;
            return PartialView(taikhoan);
        }
        public ActionResult LayOutLink()
        {
            TaiKhoan taikhoan = Session["tk"] as TaiKhoan;
            return PartialView(taikhoan);
        }
        

    }
}

