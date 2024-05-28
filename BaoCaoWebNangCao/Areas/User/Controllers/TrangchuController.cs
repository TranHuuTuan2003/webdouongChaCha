using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoCaoWebNangCao.Models;

namespace BaoCaoWebNangCao.Areas.User.Controllers
{
    public class TrangchuController : Controller
    {
        // GET: User/Trangchu
        Models.WEBNANGCAOEntities db = new Models.WEBNANGCAOEntities();
        public ActionResult Index()
        {
            List<Models.SANPHAM> data = db.SANPHAMs.Take(16).ToList();
            ViewBag.HotProducts = data;
            return View();
        }
        public ActionResult Details(int id)
        {
            SANPHAM sanpham = db.SANPHAMs.Where(row => row.ID == id).FirstOrDefault();
            return View(sanpham);
        }
    }
}