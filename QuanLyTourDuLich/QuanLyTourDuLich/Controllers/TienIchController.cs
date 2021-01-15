using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.Controllers
{
    public class TienIchController : Controller
    {
        //
        // GET: /TienIch/
        QuanLyTourDataContext data = new QuanLyTourDataContext();
        [HttpPost]
        public ActionResult TimKiem(FormCollection col)
        {
            string name = col["txtName"];
            List<TOUR> lstTour = data.TOURs.Where(t => t.TenTour.Contains(name)).ToList();
            return View(lstTour);
        }
        [HttpGet]
        public ActionResult TimKiemNangCao()
        {
            List<Tinh> lstTinh = data.Tinhs.ToList();
            ViewBag.lstTinh = new SelectList(lstTinh, "MaSoTinh", "TenTinh");
            return View();
        }
        [HttpPost]
        public ActionResult TimKiemNangCao(FormCollection col)
        {
            string ngayDi = string.Format("{0:dd/MM/yyyy}", col["txtNgayDi"]);
            string ngayVe = string.Format("{0:dd/MM/yyyy}", col["txtNgayVe"]);
            string name = col["txtName"];
            string tinh = col["txtTinh"];
            List<TOUR> lstTour1 = new List<TOUR>();
            List<TOUR> lstTour2 = new List<TOUR>();
            List<TOUR> lstTour3 = new List<TOUR>();
            List<TOUR> lstTour4 = new List<TOUR>();

            //Loc tour theo ten
            List<TOUR> lstTourTen = data.TOURs.Where(t => t.TenTour.Contains(name)).ToList();

            //Loc tour theo tinh
            //24 có nghĩa là chọn tất cả tỉnh=>Không cần lọc dữ liệu. (Đối chiếu theo SQL)
            if (tinh != "")
                lstTour1 = lstTourTen.Where(t => t.MaSoTinh == int.Parse(tinh)).ToList();
            else
                lstTour1 = lstTourTen;

            //Loc tour theo ngay
            if (ngayDi != "" && ngayVe=="")
                lstTour2 = lstTour1.Where(t => t.NgayBatDau > DateTime.Parse(ngayDi)).ToList();
            if (ngayVe != "" && ngayDi == "")
                lstTour2 = lstTour1.Where(t => t.NgayKetThuc < DateTime.Parse(ngayVe)).ToList();
            if (ngayVe != "" && ngayDi != "")
                lstTour2 = lstTour1.Where(t => t.NgayBatDau > DateTime.Parse(ngayDi) && t.NgayKetThuc < DateTime.Parse(ngayVe)).ToList();
            if (ngayVe == "" && ngayDi == "")
                lstTour2 = lstTour1;
            
            //Loc tour theo gia
            if (col["gia1"] == "1")
            {
                List<TOUR> lstGia = lstTour2.Where(t => t.Gia < 10000000).ToList();
                lstTour3.AddRange(lstGia);
            }
            if (col["gia2"] == "2")
            {
                List<TOUR> lstGia = lstTour2.Where(t => t.Gia > 11000000 && t.Gia < 20000000).ToList();
                lstTour3.AddRange(lstGia);
            }
            if (col["gia3"] == "3")
            {
                List<TOUR> lstGia = lstTour2.Where(t => t.Gia > 21000000 && t.Gia < 40000000).ToList();
                lstTour3.AddRange(lstGia);
            }
            if (col["gia4"] == "4")
            {
                List<TOUR> lstGia = lstTour2.Where(t => t.Gia > 41000000).ToList();
                lstTour3.AddRange(lstGia);
            }
            if (col["gia1"] != "1" && col["gia2"] != "2" && col["gia3"] != "3" && col["gia4"] != "4")
                lstTour3 = lstTour2;

            
            return View("TimKiem",lstTour3);
        }
        public ActionResult DanhMucTimKiem(List<TOUR> lstTour)
        {
            return PartialView(lstTour);
        }

        public ActionResult DanhSachTour()
        {
            List<Tinh> lstTinh = data.Tinhs.ToList();
            List<LoaiTour> lstLoaiTour = data.LoaiTours.ToList();
            ViewBag.lstTinh = new SelectList(lstTinh, "MaSoTinh", "TenTinh");
            ViewBag.lstLoaiTour = new SelectList(lstLoaiTour, "MaLoaiTour", "TenLoai");
            List<TOUR> lstTour = data.TOURs.ToList();
            return View(lstTour);
        }
        [HttpGet]
        public ActionResult Them()
        {
            ViewBag.lstLoaiTour = new SelectList(data.LoaiTours.ToList(), "MaLoaiTour", "TenLoai");
            ViewBag.lstTinh = new SelectList(data.Tinhs.ToList(), "MaSoTinh", "TenTinh");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(TOUR tour)
        {
            HttpPostedFileBase fileUpload = Request.Files["fileUpload"];
            ViewBag.lstLoaiTour = new SelectList(data.LoaiTours.ToList(), "MaLoaiTour", "TenLoai");
            ViewBag.lstTinh = new SelectList(data.Tinhs.ToList(), "MaSoTinh", "TenTinh");
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/AnhDoAn"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    
                    tour.HinhAnhTour = fileName;
                    data.TOURs.InsertOnSubmit(tour);
                    data.SubmitChanges();
                }
                return RedirectToAction("DanhSachTour", "TienIch");
            }
            
        }
        [HttpGet]
        public ActionResult Sua(int id)
        {
            ViewBag.lstLoaiTour = new SelectList(data.LoaiTours.ToList(), "MaLoaiTour", "TenLoai");
            ViewBag.lstTinh = new SelectList(data.Tinhs.ToList(), "MaSoTinh", "TenTinh");
            TOUR tour = data.TOURs.Where(t => t.MaSoTour == id).FirstOrDefault();
            ViewBag.ngayDi = string.Format("{0:yyyy-MM-dd}", tour.NgayBatDau);
            ViewBag.ngayVe = string.Format("{0:yyyy-MM-dd}", tour.NgayKetThuc);
            return View(tour);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(TOUR tour, FormCollection col)
        {
            HttpPostedFileBase fileUpload = Request.Files["fileUpload"];
            ViewBag.lstLoaiTour = new SelectList(data.LoaiTours.ToList(), "MaLoaiTour", "TenLoai");
            ViewBag.lstTinh = new SelectList(data.Tinhs.ToList(), "MaSoTinh", "TenTinh");
            TOUR editTour = data.TOURs.Where(t => t.MaSoTour == tour.MaSoTour).FirstOrDefault();
            if (col["chkAnh"] == "1")
            {
                if (fileUpload == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/AnhDoAn"), fileName);
                        if (System.IO.File.Exists(path))
                            ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);
                        }
                        editTour.HinhAnhTour = fileName;
                        
                    }
                    
                }
            }
            editTour.TenTour = tour.TenTour;
            editTour.NgayBatDau = tour.NgayBatDau;
            editTour.NgayKetThuc = tour.NgayKetThuc;
            editTour.MoTaTour = tour.MoTaTour;
            editTour.Gia = tour.Gia;
            editTour.MaLoaiTour = tour.MaLoaiTour;
            editTour.MaSoTinh = tour.MaSoTinh;
            UpdateModel(editTour);
            data.SubmitChanges();
            return RedirectToAction("DanhSachTour", "TienIch");
        }
        public ActionResult Xoa(int id)
        {
            try
            {
                TOUR tour = data.TOURs.Where(t => t.MaSoTour == id).FirstOrDefault();
                data.TOURs.DeleteOnSubmit(tour);
                data.SubmitChanges();
                return RedirectToAction("DanhSachTour", "TienIch");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult KeHoach(int id)
        {
            List<DiaDiemThamQuan> lstDiaDiem = data.DiaDiemThamQuans.Where(t => t.MaSoTour == id).ToList();
            return View(lstDiaDiem);
        }
        public ActionResult TourTheoTinh(int maTinh)
        {
            List<TOUR> lstTour = data.TOURs.Where(t => t.MaSoTinh == maTinh).ToList();
            return View("TimKiem", lstTour);
        }
        
        
    }
}
