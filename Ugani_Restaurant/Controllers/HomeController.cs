using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ugani_Restaurant.Models;

namespace Ugani_Restaurant.Controllers
{
    public class HomeController : Controller
    {
        private UGANI_1Entities db = new UGANI_1Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListBill()
        {
            string makh = User.Identity.GetUserId();
            var hOADONs = db.HOADONs.Include(h => h.AspNetUser).Where(h => h.MAKH == makh).OrderByDescending(h => h.NGAYLAPHD);
            return View(hOADONs.ToList());
        }

        // GET: HOADONs/Details/5
        public ActionResult DetailsBill(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.CHITIETDATBANs = db.CHITIETDATBANs.Where(m => m.MAHD == id).ToList().First();
            ViewBag.CHITIETDATMONs = db.CHITIETDATMONANs.Where(m => m.MAHD == id).ToList();
            return View(hOADON);
        }

        public ActionResult Booking()
        {
            return View(db.LOAIKHONGGIANs.ToList());
        }

        public ActionResult BookingFoods()
        {
            ViewBag.LOAIMON = db.LOAIMONs.ToList();
            return View(db.MONANs.ToList());
        }

        public ActionResult GetTableById(int id, DateTime date, DateTime startTime, DateTime endTime)
        {
            //List<BANAN> bANANs = db.BANANs.Where(m => m.MAKHONGGIAN == id).ToList();
            //So ban trong chi tiet dat ban co khong gian giong ( lay maban de xet toi time)
            // Khai báo danh sách kết quả dưới dạng List<Banan>
            DateTime ngayDat = date.Date;
            List<BANAN> a = db.BANANs.Where(m => m.MAKHONGGIAN == id).ToList();
            List<CHITIETDATBAN> b = db.CHITIETDATBANs.Where(m => m.NGAYDAT == ngayDat).ToList();
            // Chuyển ngày date.Date thành biến
            

            List<BANAN> ctbANANs = a
                .Where(x => b.Any(y => y.MABAN == x.MABAN && y.NGAYDAT != null && y.NGAYDAT == ngayDat))
                .ToList();

            //List<BANAN> ctbANANs = db.BANANs.Where(m => m.MAKHONGGIAN == id && db.CHITIETDATBANs.Any(n => n.MABAN == m.MABAN && n.NGAYDAT.Value.Date==date.Date)).Select(m => new BANAN { MABAN = m.MABAN }).ToList();

            //So ban co khong gian giong nhung chua duoc chon
            List<BANAN> bANANs = db.BANANs.Where(m => m.MAKHONGGIAN == id).ToList().Except(ctbANANs).ToList();
            List<BANAN> lsbANANs = new List<BANAN>();
            switch (b.Count())
            {
                case 0:
                    lsbANANs = db.BANANs
                        .Where(banan => b.Any(chitietdatban => chitietdatban.MABAN == banan.MABAN))
                        .ToList();
                    break;
                default:
                    foreach (var x in b)
                    {
                        if (startTime.TimeOfDay < x.GIODATBAN.Value.TimeOfDay && endTime.TimeOfDay < x.GIODATBAN.Value.TimeOfDay)
                        {
                            lsbANANs.Add(db.BANANs.Find(x.MABAN));
                        }
                        else if (startTime.TimeOfDay > x.GIODATBAN.Value.TimeOfDay && endTime.TimeOfDay > x.GIODATBAN.Value.TimeOfDay)
                        {
                            lsbANANs.Add(db.BANANs.Find(x.MABAN));
                        }
                    }
                    break;
            }
            return PartialView("_listTable", lsbANANs.Union(bANANs));
        }

        [HttpPost]
        public ActionResult SubmitBooking(DateTime date, DateTime startTime, DateTime endTime, string table, string note)
        {
            if (ModelState.IsValid)
            {
                string idKH = User.Identity.GetUserId();
                DateTime dateTime = DateTime.Now;
                HOADON hOADON = new HOADON();
                hOADON.MAKH = idKH;
                hOADON.TONGTIEN = 0;
                hOADON.TINHTRANG = "Đang chờ";                
                hOADON.NGAYLAPHD = dateTime;
                db.HOADONs.Add(hOADON);
                db.SaveChanges();

                int maHD = db.HOADONs.Where(m => m.MAKH == idKH).OrderByDescending(m => m.NGAYLAPHD).ToList().First().MAHD;

                CHITIETDATBAN cHITIETDATBAN = new CHITIETDATBAN();
                cHITIETDATBAN.MAHD = maHD;
                cHITIETDATBAN.NGAYDAT = date.Date;
                cHITIETDATBAN.GIODATBAN = startTime;
                cHITIETDATBAN.GIOTRABAN = endTime;
                cHITIETDATBAN.MABAN = table;
                cHITIETDATBAN.GHICHU = note;
                db.CHITIETDATBANs.Add(cHITIETDATBAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("About");
        }

        public ActionResult ShowBill()
        {
            string idKH = User.Identity.GetUserId();
            HOADON hOADON = db.HOADONs.Where(m => m.MAKH == idKH).OrderByDescending(m => m.NGAYLAPHD).ToList().First();
            List<CHITIETDATMONAN> cHITIETDATMONANs = db.CHITIETDATMONANs.Where(m => m.MAHD == hOADON.MAHD).ToList();
            CHITIETDATBAN cHITIETDATBAN = db.CHITIETDATBANs.Where(m => m.MAHD == hOADON.MAHD).ToList().First();
            ViewBag.CHITIETDATMONs = db.CHITIETDATMONANs.Where(m => m.MAHD == hOADON.MAHD).ToList();
            ViewBag.ChiTietDatMon = cHITIETDATMONANs;
            ViewBag.CHITIETDATBANs = cHITIETDATBAN;
            decimal tongTien = 0;
            foreach(var item in cHITIETDATMONANs)
            {
                tongTien = (decimal)(tongTien + (item.MONAN.DONGIA*item.SOLUONG));
            }
            hOADON.TONGTIEN = tongTien + cHITIETDATBAN.BANAN.LOAIKHONGGIAN.DONGIA;
            db.Entry(hOADON).State = EntityState.Modified;
            db.SaveChanges();
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        [HttpPost]
        public ActionResult SubmitBookingFoods(List<CHITIETDATMONAN> selectedItems)
        {
            if (ModelState.IsValid)
            {
                string idKH = User.Identity.GetUserId();
                int maHD = db.HOADONs.Where(m => m.MAKH == idKH).OrderByDescending(m => m.NGAYLAPHD).ToList().First().MAHD;
                foreach (var item in selectedItems)
                {
                    item.MAHD = maHD;
                }
                // Làm các thao tác xử lý dữ liệu ở đây, ví dụ: lưu danh sách CHITIETDATMONAN vào CSDL
                db.CHITIETDATMONANs.AddRange(selectedItems);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteBill(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return PartialView("DeleteBill", hOADON);
        }

        public ActionResult DeleteBillSubmit(int CatId)
        {
            if (CatId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(CatId);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            else
            {
                List<CHITIETDATBAN> cHITIETDATBANs = db.CHITIETDATBANs.Where(m => m.MAHD == CatId).ToList();
                if (cHITIETDATBANs.Count > 0)
                {
                    db.CHITIETDATBANs.RemoveRange(cHITIETDATBANs);
                }
                List<CHITIETDATMONAN> cHITIETDATMONANs = db.CHITIETDATMONANs.Where(m => m.MAHD == CatId).ToList();
                if (cHITIETDATMONANs.Count > 0)
                {
                    db.CHITIETDATMONANs.RemoveRange(cHITIETDATMONANs);
                }
                db.HOADONs.Remove(hOADON);
                db.SaveChanges();
                return RedirectToAction("ListBill");
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}