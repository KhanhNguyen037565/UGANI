using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ugani_Restaurant.Models;

namespace Ugani_Restaurant.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LOAIMONsController : Controller
    {
        private UGANI_1Entities db = new UGANI_1Entities();

        // GET: Admin/LOAIMONs
        public ActionResult Index()
        {
            return View(db.LOAIMONs.ToList());
        }


        // GET: Admin/LOAIMONs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LOAIMONs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MALOAIMON,TENLOAIMON")] LOAIMON lOAIMON)
        {
            if (ModelState.IsValid)
            {
                db.LOAIMONs.Add(lOAIMON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAIMON);
        }

        // GET: Admin/LOAIMONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMON lOAIMON = db.LOAIMONs.Find(id);
            if (lOAIMON == null)
            {
                return HttpNotFound();
            }
            return View(lOAIMON);
        }

        // POST: Admin/LOAIMONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MALOAIMON,TENLOAIMON")] LOAIMON lOAIMON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAIMON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAIMON);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Lấy thông tin về vai trò có id tương ứng và trả về một PartialView chứa form Edit
            LOAIMON lOAIMON = db.LOAIMONs.Find(id);// Lấy thông tin về vai trò từ id

            return PartialView("Delete", lOAIMON);
        }

        public ActionResult DeleteSubmit(int CatId)
        {
            LOAIMON lOAIMON = db.LOAIMONs.Find(CatId); ;
            db.LOAIMONs.Remove(lOAIMON);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/LOAIMONs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LOAIMON lOAIMON = db.LOAIMONs.Find(id);
        //    if (lOAIMON == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lOAIMON);
        //}

        // POST: Admin/LOAIMONs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LOAIMON lOAIMON = db.LOAIMONs.Find(id);
        //    db.LOAIMONs.Remove(lOAIMON);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
