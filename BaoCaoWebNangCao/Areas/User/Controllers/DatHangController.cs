using BaoCaoWebNangCao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoCaoWebNangCao.Areas.User.Controllers
{
    public class DatHangController : Controller
    {

        // GET: User/DatHang
        WEBNANGCAOEntities db = new WEBNANGCAOEntities();
        public ActionResult Index()
        {
            if (Session["TENDANGNHAP2"] == null)
            {
                return RedirectToAction("/Login", "Login", new { area = "Admin" });
            }
            else
            {
                var lstCart = ( List<CartModel> ) Session["Cart"];
                GIOHANG obj = new GIOHANG();
                obj.IDTAIKHOAN = int.Parse(Session["id"].ToString());
                obj.NGAYLAP = DateTime.Now;
                db.GIOHANGs.Add(obj);
                db.SaveChanges();

                int intOrderID = obj.ID;
                List<CHITIETGIOHANG> lstCHITIETGIOHANG = new List<CHITIETGIOHANG>();

                
                foreach (var item in lstCart)
                {
                    CHITIETGIOHANG obj2 = new CHITIETGIOHANG();
                    obj2.IDGIOHANG = intOrderID;
                    obj2.IDSANPHAM = item.IDSANPHAM;
                    obj2.SOLUONG = item.SOLUONG;
                    lstCHITIETGIOHANG.Add(obj2);
                }
                db.CHITIETGIOHANGs.AddRange(lstCHITIETGIOHANG);
                db.SaveChanges();
               
            }
            return View();

        }
    }
}