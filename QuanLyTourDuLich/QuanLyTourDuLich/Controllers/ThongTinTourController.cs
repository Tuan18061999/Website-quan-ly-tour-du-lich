using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.Controllers
{
    public class ThongTinTourController : Controller
    {
        //
        // GET: /ThongTinTour/
        QuanLyTourDataContext data = new QuanLyTourDataContext();
        public ActionResult ChiTietTour(int id)
        {
            TOUR tour = data.TOURs.Where(t=>t.MaSoTour==id).FirstOrDefault();
            List<ChiTietPhuongTien> lstPhuongTien = data.ChiTietPhuongTiens.Where(t => t.MaSoTour == id).ToList();
            ViewBag.lstPhuongTien = lstPhuongTien;
            List<DiaDiemThamQuan> lstThamQuan = data.DiaDiemThamQuans.Where(t => t.MaSoTour == id).ToList();
            ViewBag.lstThamQuan = lstThamQuan;
            List<ChiTietDichVu> lstDichVu = data.ChiTietDichVus.Where(t => t.MaSoTour == id).ToList();
            ViewBag.lstDichVu = lstDichVu;
            List<TOUR> lstTourCungLoai = data.TOURs.Where(t => t.MaLoaiTour == tour.MaLoaiTour).ToList();
            List<TOUR> lstTourCungLoaiRandom = new List<TOUR>();
            Random rd = new Random();
            for (int i = 0; i < 3; i++)
            {
                int num = rd.Next(0,lstTourCungLoai.Count());
                lstTourCungLoaiRandom.Add(lstTourCungLoai[num]);
            }
            ViewBag.lstTourCungLoaiRandom = lstTourCungLoaiRandom;
            List<DanhGia> lstTopDanhGia = data.DanhGias.OrderByDescending(t => t.LuotYeuThich).Take(3).ToList();
            ViewBag.lstTopDanhGia = lstTopDanhGia;
            return View(tour);
        }

    }
}
