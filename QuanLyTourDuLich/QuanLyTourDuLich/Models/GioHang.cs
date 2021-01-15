using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTourDuLich.Models
{
    public class GioHang
    {
        public List<CartItem> lst;
        public GioHang()
        {
            lst = new List<CartItem>();
        }
        public GioHang(List<CartItem> lstGH)
        {
            lst = lstGH;
        }
        public int SoMatHang()
        {
            int soLuongHang = lst.Count();
            return soLuongHang;
        }
        public int TongSLHang()
        {
            if (lst == null)
                return 0;
            int tongSoLuongHang = lst.Sum(t => t.iSoLuong);
            return tongSoLuongHang;
        }
        public double TongThanhTien()
        {
            if (lst == null)
                return 0;
            double tongThanhTien = lst.Sum(t => t.ThanhTien);
            return tongThanhTien;
        }
        public int Them(int iMaTour)
        {
            CartItem sanpham = lst.Find(t => t.iMaTour == iMaTour);
            if (sanpham == null)
            {
                CartItem tour = new CartItem(iMaTour);
                if (tour == null)
                    return -1;
                lst.Add(tour);
            }
            else
            {
                sanpham.iSoLuong++;
            }
            return -1;
        }
        public int Xoa(int iMaTour)
        {
            CartItem sanpham = lst.Find(t => t.iMaTour == iMaTour);
            lst.Remove(sanpham);
            return -1;
        }

    }
}