using BaoCaoWebNangCao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoCaoWebNangCao.Areas.User.Controllers
{
    public class CartController : Controller
    {
        // GET: User/Cart
        Models.WEBNANGCAOEntities db = new Models.WEBNANGCAOEntities();
        public ActionResult Index()
        {
            if (Session["TENDANGNHAP2"] == null)
            {
                return RedirectToAction("/Login", "Login", new { area = "Admin" });
            }
            else
            {
                return View();
            }

        }

        public ActionResult AddToCart(int id)
        {
            List<Models.CartModel> lstCart = null;
            if (Session["Cart"] == null)
            {
                lstCart = new List<Models.CartModel>();
            }
            else
            {
                lstCart = (List<Models.CartModel>)Session["Cart"];
            }
            Models.CartModel obj = lstCart.FirstOrDefault(m => m.IDSANPHAM == id);
            if (obj == null)
            {
                Models.SANPHAM crrSANPHAM = db.SANPHAMs.First(m => m.ID == id);
                obj = new Models.CartModel
                {
                    IDSANPHAM = id,
                    SANPHAMDetail = crrSANPHAM,
                    SOLUONG = 1,
                    TONGTIEN = (double)(1 * crrSANPHAM.GIA),
                };
                lstCart.Add(obj);
            }
            else
            {
                obj.SOLUONG = obj.SOLUONG + 1;
                obj.TONGTIEN =  (double)(obj.SOLUONG * obj.SANPHAMDetail.GIA);

            }
            obj.TONGGIOHANG = lstCart.Sum(m => m.TONGTIEN);

           
            Session["Cart"]= lstCart;
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var sessionCart = (List<Models.CartModel>)Session["Cart"];
            sessionCart.RemoveAll(m => m.IDSANPHAM == id);
            Session["Cart"] = sessionCart;
            return RedirectToAction("Index");

        }

        public ActionResult updateTotal(int id, string stringInputValue)
        {

            List<CartModel> lstCart = (List<Models.CartModel>)Session["Cart"];
            Models.CartModel obj = lstCart.FirstOrDefault(m => m.IDSANPHAM == id);

            obj.SOLUONG = int.Parse(stringInputValue);
            obj.TONGTIEN =  (double)(obj.SOLUONG * obj.SANPHAMDetail.GIA);


            Session["Cart"]= lstCart;
            return PartialView("_Lst_Cart");
        }








    }
}