        using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BaoCaoWebNangCao.Models;

namespace BaoCaoWebNangCao.Areas.Admin.Controllers
{
    public class SANPHAMsController : Controller
    {
        private WEBNANGCAOEntities db = new WEBNANGCAOEntities();

        // GET: Admin/SANPHAMs
        public ActionResult Index(string search="", int page = 1, int pageSize = 5)
        {
            if (Session["TENDANGNHAP"] == null)
            {
                return RedirectToAction("/Login", "Login", new { area = "Admin" });
            }
            else
            {
                WEBNANGCAOEntities db = new WEBNANGCAOEntities();

                // Query the total count of records matching the search criteria
                int totalCount = db.SANPHAMs.Count(row => row.TENSANPHAM.Contains(search));

                // Calculate total pages
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure page is within bounds
                page = Math.Max(1, Math.Min(totalPages, page));

                // Calculate the number of records to skip
                int skip = (page - 1) * pageSize;

                // Query records for the current page
                List<SANPHAM> sp = db.SANPHAMs
                                            .Where(row => row.TENSANPHAM.Contains(search))
                                            .OrderBy(row => row.ID) // Adjust the ordering as needed
                .Skip(skip)
                                            .Take(pageSize)
                                            .ToList();

                // Pass pagination data to the view
                ViewBag.Search = search;
                ViewBag.Page = page;
                ViewBag.TotalPages = totalPages;

                return View(sp);

                /*var sANPHAMs = db.SANPHAMs.Include(s => s.DANHMUCSANPHAM);
                return View(sANPHAMs.ToList());*/
            }

            
        }

      

        // GET: Admin/SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.IDDANHMUC = new SelectList(db.DANHMUCSANPHAMs, "ID", "TENDANHMUC");
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SANPHAM sANPHAM,HttpPostedFileBase hinhanh)
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            if(sANPHAM.TENSANPHAM==null)
            {
                ModelState.AddModelError("", "Thiếu thông tin tên sản phẩm");
                return View();
            }
            if(hinhanh != null && hinhanh.ContentLength > 0 )
            {
                string rootFolder = Server.MapPath("/Data/");
                string pathImage = rootFolder + hinhanh.FileName;
                hinhanh.SaveAs(pathImage);
                sANPHAM.ANH = "/Data/" + hinhanh.FileName;
            }    
           
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("/Index");
            
        }

        // GET: Admin/SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDANHMUC = new SelectList(db.DANHMUCSANPHAMs, "ID", "TENDANHMUC", sANPHAM.IDDANHMUC);
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SANPHAM sANPHAM, HttpPostedFileBase hinhanh)
        {
            SANPHAM sanpham = db.SANPHAMs.Where(row => row.ID == sANPHAM.ID).FirstOrDefault();

            if (hinhanh!=null && hinhanh.ContentLength > 0)
            {
                string rootFolder = Server.MapPath("/Data/");
                string pathImage = rootFolder + hinhanh.FileName;
                hinhanh.SaveAs(pathImage);
                sANPHAM.ANH = "/Data/" + hinhanh.FileName;
                sanpham.ANH = sANPHAM.ANH;
              
            }
            sanpham.IDDANHMUC = sANPHAM.IDDANHMUC;
            sanpham.TENSANPHAM = sANPHAM.TENSANPHAM;
            sanpham.NGAYSANXUAT = sANPHAM.NGAYSANXUAT;
            sanpham.DONVITINH = sANPHAM.DONVITINH;
            sanpham.SOLUONG= sANPHAM.SOLUONG;
            sanpham.GIA = sANPHAM.GIA;
            sanpham.CHITIET = sANPHAM.CHITIET;
            sanpham.CHITIET2 = sANPHAM.CHITIET2;
           
            db.SaveChanges();
            return RedirectToAction("/Index");
           
        }

        // GET: Admin/SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
