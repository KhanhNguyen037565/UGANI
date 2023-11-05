using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ugani_Restaurant.Models;

namespace Ugani_Restaurant.Controllers
{
    public class CHITIETDATBANsController : Controller
    {
        private UGANI_1Entities db = new UGANI_1Entities();

        // GET: CHITIETDATBANs
        public ActionResult Index()
        {
            var cHITIETDATBANs = db.CHITIETDATBANs.Include(c => c.MAHD).Include(c => c.BANAN);
            return View(cHITIETDATBANs.ToList());
        }

        // GET: CHITIETDATBANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDATBAN cHITIETDATBAN = db.CHITIETDATBANs.Find(id);
            if (cHITIETDATBAN == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDATBAN);
        }

        // GET: CHITIETDATBANs/Create
        public ActionResult Create()
        {
            ViewBag.LOAIKHONGGIAN = db.LOAIKHONGGIANs.ToList();
            ViewBag.MAKH = new SelectList(db.AspNetUsers, "Id", "UserName");
            ViewBag.MABAN = new SelectList(db.BANANs, "MABAN", "MABAN");
            return View();
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

        // POST: CHITIETDATBANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "STT,MAKH,MABAN,NGAYDAT,GIODATBAN,GIOTRABAN,GHICHU")] CHITIETDATBAN cHITIETDATBAN)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CHITIETDATBANs.Add(cHITIETDATBAN);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.LOAIKHONGGIAN = db.LOAIKHONGGIANs.ToList();
        //    ViewBag.MAKH = new SelectList(db.AspNetUsers, "Id", "UserName", cHITIETDATBAN.MAKH);
        //    ViewBag.MABAN = new SelectList(db.BANANs, "MABAN", "MABAN", cHITIETDATBAN.MABAN);
        //    return View(cHITIETDATBAN);
        //}

        // GET: CHITIETDATBANs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CHITIETDATBAN cHITIETDATBAN = db.CHITIETDATBANs.Find(id);
        //    if (cHITIETDATBAN == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MAKH = new SelectList(db.AspNetUsers, "Id", "UserName", cHITIETDATBAN.MAKH);
        //    ViewBag.MABAN = new SelectList(db.BANANs, "MABAN", "MABAN", cHITIETDATBAN.MABAN);
        //    return View(cHITIETDATBAN);
        //}

        // POST: CHITIETDATBANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "STT,MAKH,MABAN,NGAYDAT,GIODATBAN,GIOTRABAN,GHICHU")] CHITIETDATBAN cHITIETDATBAN)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cHITIETDATBAN).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MAKH = new SelectList(db.AspNetUsers, "Id", "UserName", cHITIETDATBAN.MAKH);
        //    ViewBag.MABAN = new SelectList(db.BANANs, "MABAN", "MABAN", cHITIETDATBAN.MABAN);
        //    return View(cHITIETDATBAN);
        //}

        // GET: CHITIETDATBANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDATBAN cHITIETDATBAN = db.CHITIETDATBANs.Find(id);
            if (cHITIETDATBAN == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDATBAN);
        }

        // POST: CHITIETDATBANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHITIETDATBAN cHITIETDATBAN = db.CHITIETDATBANs.Find(id);
            db.CHITIETDATBANs.Remove(cHITIETDATBAN);
            db.SaveChanges();
            return RedirectToAction("Index");
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
