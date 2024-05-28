using BaoCaoWebNangCao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BaoCaoWebNangCao.Areas.Admin.Controllers
{
    public class CongThucController : Controller
    {
        // GET: Admin/CongThuc
        
        public ActionResult Index(string search="")
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            /*List<CONGTHUC> congthuc= db.CONGTHUCs.ToList();*/
            List<CONGTHUC> congthuc = db.CONGTHUCs.Where(row => row.TENCONGTHUC.Contains(search)).ToList();
            return View(congthuc);
        }
        public ActionResult Detail(int id)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            CONGTHUC congthuc = db.CONGTHUCs.Where(row=> row.ID == id).FirstOrDefault();
            return View(congthuc);
        }
        public ActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create (CONGTHUC c, HttpPostedFileBase hinhanh)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            if (hinhanh != null &&  hinhanh.ContentLength > 0)
            {
                //lưu file ảnh
                string rootFolder = Server.MapPath("/Data/Image/");
                string pathImage = rootFolder + hinhanh.FileName;
                hinhanh.SaveAs(pathImage);
                //Lưu thuộc tính url
                c.HINHANH= "/Data/Image/"+hinhanh.FileName;

            }
            db.CONGTHUCs.Add(c);
            db.SaveChanges();
            return RedirectToAction("/Index");

        }
        public ActionResult Edit(int id)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            CONGTHUC congthuc=db.CONGTHUCs.Where(row=>row.ID==id).FirstOrDefault();
            return View(congthuc);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CONGTHUC c,HttpPostedFileBase hinhanh)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            CONGTHUC congthuc = db.CONGTHUCs.Where(row => row.ID == c.ID).FirstOrDefault();
            /*Cập nhật*/
            if (hinhanh != null && hinhanh.ContentLength > 0)
            {
                //lưu file ảnh
                string rootFolder = Server.MapPath("/Data/Image/");
                string pathImage = rootFolder + hinhanh.FileName;
                hinhanh.SaveAs(pathImage);
                //Lưu thuộc tính url
                c.HINHANH = "/Data/Image/" + hinhanh.FileName;
                congthuc.HINHANH = c.HINHANH;
            }
            congthuc.IDDANHMUC = c.IDDANHMUC;
            congthuc.TENCONGTHUC=c.TENCONGTHUC;
            congthuc.NGAYDANGTAI = c.NGAYDANGTAI;
            congthuc.NGUYENLIEU = c.NGUYENLIEU;
            congthuc.NOIDUNG = c.NOIDUNG;
            congthuc.NOIDUNGPHU=c.NOIDUNGPHU;
            db.SaveChanges();
            return RedirectToAction("/Index");
        }
        public ActionResult Delete(int id)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            CONGTHUC congthuc = db.CONGTHUCs.Where(row => row.ID == id).FirstOrDefault();
            return View(congthuc);
        }
        [HttpPost]
        public ActionResult Delete(int id, CONGTHUC c)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            CONGTHUC congthuc = db.CONGTHUCs.Where(row => row.ID == id).FirstOrDefault();
            db.CONGTHUCs.Remove(congthuc);
            db.SaveChanges();
            return RedirectToAction("/Index");
        }
    }
}