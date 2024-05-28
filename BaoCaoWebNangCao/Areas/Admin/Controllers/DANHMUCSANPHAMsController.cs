using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BaoCaoWebNangCao.Models;

namespace BaoCaoWebNangCao.Areas.Admin.Controllers
{
    public class DANHMUCSANPHAMsController : Controller
    {
        private WEBNANGCAOEntities db = new WEBNANGCAOEntities();

        
        public ActionResult Index(string search = "", int page = 1, int pageSize = 6)
        {
            if (Session["TENDANGNHAP"] == null)
            {
                return RedirectToAction("/Login", "Login", new { area = "Admin" });
            }
            else
            {
                WEBNANGCAOEntities db = new WEBNANGCAOEntities();

                // Query the total count of records matching the search criteria
                int totalCount = db.DANHMUCSANPHAMs.Count(row => row.TENDANHMUC.Contains(search));

                // Calculate total pages
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure page is within bounds
                page = Math.Max(1, Math.Min(totalPages, page));

                // Calculate the number of records to skip
                int skip = (page - 1) * pageSize;

                // Query records for the current page
                List<DANHMUCSANPHAM> danhmuc = db.DANHMUCSANPHAMs
                                            .Where(row => row.TENDANHMUC.Contains(search))
                                            .OrderBy(row => row.ID) // Adjust the ordering as needed
                .Skip(skip)
                                            .Take(pageSize)
                                            .ToList();

                // Pass pagination data to the view
                ViewBag.Search = search;
                ViewBag.Page = page;
                ViewBag.TotalPages = totalPages;

                return View(danhmuc);
            }
            
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCSANPHAM dANHMUCSANPHAM = db.DANHMUCSANPHAMs.Find(id);
            if (dANHMUCSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCSANPHAM);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TENDANHMUC")] DANHMUCSANPHAM dANHMUCSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.DANHMUCSANPHAMs.Add(dANHMUCSANPHAM);
                db.SaveChanges();
                return RedirectToAction("/Index");
            }

            return View(dANHMUCSANPHAM);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCSANPHAM dANHMUCSANPHAM = db.DANHMUCSANPHAMs.Find(id);
            if (dANHMUCSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCSANPHAM);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TENDANHMUC")] DANHMUCSANPHAM dANHMUCSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANHMUCSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            return View(dANHMUCSANPHAM);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCSANPHAM dANHMUCSANPHAM = db.DANHMUCSANPHAMs.Find(id);
            if (dANHMUCSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCSANPHAM);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DANHMUCSANPHAM dANHMUCSANPHAM = db.DANHMUCSANPHAMs.Find(id);
            db.DANHMUCSANPHAMs.Remove(dANHMUCSANPHAM);
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
