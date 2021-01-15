using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTourDuLich.Models
{
    public class CartItem
    {
        public int iMaTour { get; set; }
        public string sTenTour { get; set; }
        public string sHinhAnhTour { get; set; }
        public double dGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dGia; }
        }
        QuanLyTourDataContext data = new QuanLyTourDataContext();
        public CartItem(int MaTour)
        {
            TOUR tour = data.TOURs.Where(t => t.MaSoTour == MaTour).FirstOrDefault();
            if (tour != null)
            {
                iMaTour = MaTour;
                sTenTour = tour.TenTour;
                sHinhAnhTour = tour.HinhAnhTour;
                dGia = double.Parse(tour.Gia.ToString());
                iSoLuong = 1;
            }
        }
    }
}