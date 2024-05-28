using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaoCaoWebNangCao.Models
{
    public class CartModel
    {
        public int IDSANPHAM { get; set; }
        public Models.SANPHAM SANPHAMDetail {get;set;}
        public int SOLUONG { get; set; }
        public double TONGTIEN { get; set; }

        public double TONGGIOHANG { get; set; }

    }
}