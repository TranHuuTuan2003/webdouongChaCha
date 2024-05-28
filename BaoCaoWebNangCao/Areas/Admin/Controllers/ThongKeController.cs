using BaoCaoWebNangCao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoCaoWebNangCao.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            return View();
        }

       /* public ActionResult tk()
        {
            WEBNANGCAOEntities db = new WEBNANGCAOEntities();
            var last7Days = DateTime.Now.AddDays(-8);
            var labelList = db.THONGTINTAIKHOAN.Where(item => item.Ngaythamgia > last7Days && item.Ngaythamgia < DateTime.Now)
                .Select(item => DbFunctions.TruncateTime(item.Ngaythamgia).ToString()).Distinct().ToArray();
            var dataList = db.THONGTINTAIKHOAN.Where(item => item.Ngaythamgia > last7Days && item.Ngaythamgia < DateTime.Now)
                .GroupBy(item => DbFunctions.TruncateTime(item.Ngaythamgia))
                .Select(item => item.Count()).ToArray();
            List<float> doanhthuhangnam;
            for (int i = 2022; i <= 2024; i++)
            {
                var dt = db.GIOHANGs.Where(item => item.NGAYLAP.HasValue && item.NGAYLAP.)

            }
            List<string> backgroundColors = new List<string>();
            Random rand = new Random();
            for (int i = 0; i < labelList.Count(); i++)
            {
                int red = rand.Next(256);
                int green = rand.Next(256);
                int blue = rand.Next(256);
                string colors = $"rgba({red}, {green}, {blue}, 1)";
                backgroundColors.Add(colors);
            }
            var data = new
            {
                type = "bar",
                labels = labelList,
                label = "Số tài khoản được tạo trong 7 ngày trước",
                data = dataList,
                backgroundColor = backgroundColors.ToArray()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }*/
    }
}