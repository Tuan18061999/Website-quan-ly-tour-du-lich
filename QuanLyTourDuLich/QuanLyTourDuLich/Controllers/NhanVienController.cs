using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTourDuLich.Models;

namespace QuanLyTourDuLich.Controllers
{
    public class NhanVienController : Controller
    {
        //
        // GET: /NhanVien/
        QuanLyTourDataContext data = new QuanLyTourDataContext();
        public ActionResult DanhSachNhanVien()
        {
            List<NhanVien> lstNhanVien = data.NhanViens.ToList();
            return View(lstNhanVien);
        }
            [HttpGet]
            public ActionResult Them()
            {
                ViewBag.lstChucVu = new SelectList(data.ChucVus.ToList(), "MaSoChucVu", "TenChucVu");
                return View();
            }
            [HttpPost]
            [ValidateInput(false)]
            public ActionResult Them(NhanVien nv)
            {
                //ViewBag.lstChucVu = new SelectList(data.ChucVus.ToList(), "MaSoChucVu", "TenChucVu");
                data.NhanViens.InsertOnSubmit(nv);
                data.SubmitChanges();
                return RedirectToAction("DanhSachNhanVien", "NhanVien");
            }
            public ActionResult Xoa(int id)
            {
                try
                {
                    int ma = id;
                    NhanVien nv  = data.NhanViens.Where(t => t.MaSoNhanVien == id).FirstOrDefault();
                    data.NhanViens.DeleteOnSubmit(nv);
                    data.SubmitChanges();
                    return RedirectToAction("DanhSachNhanVien", "NhanVien");
                }
                catch
                {
                    return View();
                }
            }
            public ActionResult Sua(int id)
            {
                ViewBag.lstChucVu = new SelectList(data.ChucVus.ToList(), "MaSoChucVu", "TenChucVu");
                NhanVien nv = data.NhanViens.Where(t => t.MaSoNhanVien == id).FirstOrDefault();
                ViewBag.ngaySinh = string.Format("{0:yyyy-MM-dd}", nv.NgaySinh);
                return View(nv);
            }
            [HttpPost]
            [ValidateInput(false)]
            public ActionResult Sua(NhanVien nv, FormCollection col)
            {
              
                NhanVien editnv = data.NhanViens.Where(t => t.MaSoNhanVien == nv.MaSoNhanVien).FirstOrDefault();
                
                editnv.MaSoNhanVien = nv.MaSoNhanVien;
                editnv.TenNhanVien = nv.TenNhanVien;
                editnv.NgaySinh = nv.NgaySinh;
                editnv.GioiTinh = nv.GioiTinh;
                editnv.DiaChi = nv.DiaChi;
                editnv.SoDienThoai = nv.SoDienThoai;
                editnv.MaSoChucVu = nv.MaSoChucVu;
                UpdateModel(editnv);
                data.SubmitChanges();
                return RedirectToAction("DanhSachNhanVien", "NhanVien");
            }
            }
            

        }
    

