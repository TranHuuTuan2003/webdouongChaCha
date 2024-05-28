using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoCaoWebNangCao.Areas.User.Controllers
{
    public class SanphamController : Controller
    {
        Models.WEBNANGCAOEntities db = new Models.WEBNANGCAOEntities();
        // GET: User/Sanpham
        public ActionResult Index()
        {
            List<Models.SANPHAM> data = db.SANPHAMs.Take(20).ToList();
            ViewBag.HotProducts = data;
            return View();
        }
    }
}