using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ugani_Restaurant.Models;

namespace Ugani_Restaurant.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employment")]
    public class SystemManagementController : Controller
    {

        // GET: Admin/SystemManagement
        UGANI_1Entities db = new UGANI_1Entities();
        public ActionResult Index()
        {
            ViewBag.ListYears = getListYear();
            ViewBag.SumBills = db.HOADONs.ToList().Count;
            ViewBag.SumFoods = db.MONANs.ToList().Count;
            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.Online = HttpContext.Application["Online"].ToString();
            return View();
        }

        public Object getListYear()
        {
            var lsYear = db.ListYear().ToList();
            return lsYear;
        }

        public ActionResult GetResultReport(int year, int month)
        {
            var lsData = GetReportByYearMonth(year, month);
            List<SourceChart> a = new List<SourceChart>();
            for (int i = 0; i < lsData.Count; i++)
            {
                SourceChart b = new SourceChart();
                b.Ngay = lsData[i].NgayLapHD.Value.ToString("dd/MM/yyyy");
                b.TongTien = lsData[i].TongTien.Value;
                a.Add(b);
            }

            return Json(a, JsonRequestBehavior.AllowGet);
        }



        public List<Report_Result> GetReportByYearMonth(int year, int month)
        {
            using (db)
            {
                var lsData = db.Report(month, year).ToList();
                return lsData;
            }
        }


        public ActionResult RenderLOAIMONAN()
        {
            List<LOAIMON> lOAIMONs = db.LOAIMONs.ToList();
            return PartialView("LOAIMONAN_ls", lOAIMONs);
        }

        public ActionResult RenderLOAIKHONGGIAN()
        {
            List<LOAIKHONGGIAN> lOAIKHONGGIANs = db.LOAIKHONGGIANs.ToList();
            return PartialView("LOAIKHONGGIAN_ls", lOAIKHONGGIANs);
        }
        
    }
}