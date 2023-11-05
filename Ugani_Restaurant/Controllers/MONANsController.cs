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
    public class MONANsController : Controller
    {
        private UGANI_1Entities db = new UGANI_1Entities();

        // GET: MONANs
        public ActionResult Index()
        {
            var mONANs = db.MONANs.Include(m => m.LOAIMON);
            return View(mONANs.ToList());
        }

        // GET: MONANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONAN mONAN = db.MONANs.Find(id);
            if (mONAN == null)
            {
                return HttpNotFound();
            }
            return View(mONAN);
        }

        // GET: MONANs/Create
        public ActionResult Create()
        {
            ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON");
            return View();
        }

        // POST: MONANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAMONAN,MALOAIMON,TENMONAN,HINHANH,DONGIA,DVT")] MONAN mONAN)
        {
            if (ModelState.IsValid)
            {
                db.MONANs.Add(mONAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON", mONAN.MALOAIMON);
            return View(mONAN);
        }

        // GET: MONANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONAN mONAN = db.MONANs.Find(id);
            if (mONAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON", mONAN.MALOAIMON);
            return View(mONAN);
        }

        // POST: MONANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAMONAN,MALOAIMON,TENMONAN,HINHANH,DONGIA,DVT")] MONAN mONAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mONAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON", mONAN.MALOAIMON);
            return View(mONAN);
        }

        // GET: MONANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONAN mONAN = db.MONANs.Find(id);
            if (mONAN == null)
            {
                return HttpNotFound();
            }
            return View(mONAN);
        }

        // POST: MONANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MONAN mONAN = db.MONANs.Find(id);
            db.MONANs.Remove(mONAN);
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
