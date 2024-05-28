using System.Web.Mvc;
using BaoCaoWebNangCao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;



namespace BaoCaoWebNangCao.Areas.Admin.Controllers
{
    public class DonDatHangController : Controller
    {
        private WEBNANGCAOEntities db = new WEBNANGCAOEntities();
        // GET: Admin/DonDatHang
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
                int totalCount = db.GIOHANGs.Where(row => row.TAIKHOAN.TENDANGNHAP.Contains(search)).Count();

                // Calculate total pages
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure page is within bounds
                page = Math.Max(1, Math.Min(totalPages, page));

                // Calculate the number of records to skip
                int skip = (page - 1) * pageSize;

                // Query records for the current page
                List<GIOHANG> dondathang = db.GIOHANGs
      .Where(row => row.TAIKHOAN.TENDANGNHAP.Contains(search))
      .OrderBy(row => row.ID)
      .Skip(skip)
      .Take(pageSize)
      .ToList();

                // Pass pagination data to the view
                ViewBag.Search = search;
                ViewBag.Page = page;
                ViewBag.TotalPages = totalPages;

                return View(dondathang);
            }
        }


        // GET: Admin/SANPHAMs/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Truy vấn chi tiết giỏ hàng dựa trên id
            List<Models.CHITIETGIOHANG> data = db.CHITIETGIOHANGs.Where(ct => ct.IDGIOHANG == id).ToList();
            List<Models.GIOHANG> data2 = db.GIOHANGs.Where(ct => ct.ID == id).ToList();
            List<Models.CHITIETGIOHANG> data3 = db.CHITIETGIOHANGs.Where(ct => ct.IDGIOHANG == id).ToList();
            List<Models.GIOHANG> da = db.GIOHANGs.Where(ct => ct.ID == id).ToList();

            // Kiểm tra xem có dữ liệu chi tiết giỏ hàng không
            if (data == null || data.Count == 0)
            {
                return HttpNotFound();
            }

            // Gửi dữ liệu chi tiết giỏ hàng tới view
            ViewBag.CT = data;
            ViewBag.CT2 = data2;
            ViewBag.CT3 = data3;
            ViewBag.CT4 = da;

            return View();


        }
    }
}