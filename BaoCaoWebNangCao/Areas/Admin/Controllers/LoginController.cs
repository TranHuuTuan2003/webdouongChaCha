using BaoCaoWebNangCao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BaoCaoWebNangCao.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        WEBNANGCAOEntities db = new WEBNANGCAOEntities();
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(string TENDANGNHAP, string MATKHAU, string LOAITAIKHOAN)
        {
            mapTaikhoan map = new mapTaikhoan();
            var user = map.Timkiem(TENDANGNHAP, MATKHAU, LOAITAIKHOAN);

            if (user != null)
            {
                Session["TENDANGNHAP"] = TENDANGNHAP;
                var TENNGUOIDUNG = (from tk in db.TAIKHOANs
                                    where tk.TENDANGNHAP== TENDANGNHAP
                                    select tk.TENNGUOIDUNG).FirstOrDefault();
                var HINHANH = (from tk in db.TAIKHOANs
                                    where tk.TENDANGNHAP== TENDANGNHAP
                                    select tk.HINHANH).FirstOrDefault();
               
                Session["TENNGUOIDUNG"] = TENNGUOIDUNG;
                Session["HINHANH"] = HINHANH;
               
                return RedirectToAction("/Index", "SANPHAMs", new { area = "Admin" });
            }
            ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng";

            var user2 = map.Timkiem2(TENDANGNHAP, MATKHAU, LOAITAIKHOAN);
            Session["a"] = TENDANGNHAP;
            if (user2 != null)
            {
                Session["TENDANGNHAP2"] = TENDANGNHAP;
                Session["id"] = user2.ID;

                return RedirectToAction("/Index", "Trangchu", new { area = "User" });
            }
            ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }


        public ActionResult Logout()
        {
            Session["TENDANGNHAP"] = null;
            Session["TENDANGNHAP2"] = null;
            return Redirect("https://localhost:44302/Admin/Login/Login");
        }
    }
}