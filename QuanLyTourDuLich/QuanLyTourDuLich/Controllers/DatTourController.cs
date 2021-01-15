using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.Controllers
{
    public class DatTourController : Controller
    {
        //
        // GET: /DatTour/
        QuanLyTourDataContext data = new QuanLyTourDataContext();
        public ActionResult ThemTour(int MaTour)
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh == null)
                gh = new GioHang();
            int kq = gh.Them(MaTour);
            Session["gh"] = gh;
            return RedirectToAction("TrangChu", "Home");
        }
        public ActionResult XemGioHang()
        {
            GioHang gh = (GioHang)Session["gh"];
            return View(gh);
        }
        [HttpGet]
        public ActionResult XacNhanDatTour()
        {
            TaiKhoan taikhoan = (TaiKhoan)Session["tk"];
            if (taikhoan == null)
                return RedirectToAction("DangNhap", "TaiKhoan");
            return View(taikhoan);
        }
        [HttpPost]
        public ActionResult TaoDonDatTour()
        {
            Session["gh"] = null;
            return View();
        }

        public ActionResult XoaTour(int MaTour)
        {
            GioHang gh = (GioHang)Session["gh"];
            int kq = gh.Xoa(MaTour);
            Session["gh"] = gh;
            return RedirectToAction("XemGioHang", "DatTour");
        }

        public JsonResult GetbyID(int ID)
        {
            GioHang gh=(GioHang)Session["gh"];
            CartItem item = gh.lst.Find(t => t.iMaTour == ID);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        
    }
}
