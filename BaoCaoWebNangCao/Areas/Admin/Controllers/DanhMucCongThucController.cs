using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoCaoWebNangCao.Models;

namespace BaoCaoWebNangCao.Areas.Admin.Controllers
{
    public class DanhMucCongThucController : Controller
    {
        // GET: Admin/DanhMucCongThuc
        
        public ActionResult Index(string search="")
        {
           
            if (Session["TENDANGNHAP"] == null)
            {
                return RedirectToAction("/Login", "Login", new { area = "Admin" });
            }
            else
            {
                WEBNANGCAOEntities db = new WEBNANGCAOEntities();
                List<DANHMUCCONGTHUC> danhmuc = db.DANHMUCCONGTHUCs.Where(row => row.TENDANHMUC.Contains(search)).ToList();
                ViewBag.Search = search;
                return View(danhmuc);
            }
        }
        public ActionResult Detail(int id)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            DANHMUCCONGTHUC danhmuc = db.DANHMUCCONGTHUCs.Where(row => row.ID == id).FirstOrDefault();

            return View(danhmuc);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DANHMUCCONGTHUC d)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            db.DANHMUCCONGTHUCs.Add(d);
            db.SaveChanges();
            return RedirectToAction("/Index");

        }
        public ActionResult Edit(int id)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            DANHMUCCONGTHUC danhmuc = db.DANHMUCCONGTHUCs.Where(row => row.ID == id).FirstOrDefault();
            return View(danhmuc);
        }
        [HttpPost]
        public ActionResult Edit(DANHMUCCONGTHUC d)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            DANHMUCCONGTHUC danhmuc = db.DANHMUCCONGTHUCs.Where(row => row.ID == d.ID).FirstOrDefault();
            /*Cập nhật*/
            danhmuc.IDTAIKHOAN = d.IDTAIKHOAN;
            danhmuc.TENDANHMUC = d.TENDANHMUC;
            db.SaveChanges();
            return RedirectToAction("/Index");
        }
        public ActionResult Delete(int id)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            DANHMUCCONGTHUC danhmuc = db.DANHMUCCONGTHUCs.Where(row => row.ID == id).FirstOrDefault();
            return View(danhmuc);
        }
        [HttpPost]
        public ActionResult Delete(int id, DANHMUCCONGTHUC d)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            DANHMUCCONGTHUC danhmuc = db.DANHMUCCONGTHUCs.Where(row => row.ID == id).FirstOrDefault();
            db.DANHMUCCONGTHUCs.Remove(danhmuc);
            db.SaveChanges();
            return RedirectToAction("/Index");
        }
    }
}