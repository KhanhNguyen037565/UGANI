using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ugani_Restaurant.Models;

namespace Ugani_Restaurant.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employment")]
    public class MONANsController : Controller
    {
        private UGANI_1Entities db = new UGANI_1Entities();
        [AllowAnonymous]
        public ActionResult LoadMONANsById(int CatId)
        {
            List<MONAN> mONANs = db.MONANs.Where(m => m.MALOAIMON == CatId).ToList();
            return PartialView("Index", mONANs);
        }

        // GET: Admin/MONANs/Details/5
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

        // GET: Admin/MONANs/Create
        public ActionResult Create()
        {
            ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON");
            return View();
        }

        // POST: Admin/MONANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "MAMONAN,MALOAIMON,TENMONAN,HINHANH,DONGIA,DVT")] MONAN mONAN, HttpPostedFileBase HINHANH)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HINHANH.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(HINHANH.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/foodimages"), _FileName);
                        HINHANH.SaveAs(_path);
                        mONAN.HINHANH = _FileName;
                    }
                    db.MONANs.Add(mONAN);
                    db.SaveChanges();
                    return RedirectToAction("Index", "SystemManagement", new { area = "Admin" });
                }
                catch
                {
                    ViewBag.Message = "Không thành công!!";
                }
            }

            ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON", mONAN.MALOAIMON);
            return View(mONAN);
        }


        // GET: Admin/MONANs/Edit/5
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

        // POST: Admin/MONANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "MAMONAN,MALOAIMON,TENMONAN,HINHANH,DONGIA,DVT")] MONAN mONAN, HttpPostedFileBase HINHANH, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HINHANH != null)
                    {
                        string _FileName = Path.GetFileName(HINHANH.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/foodimages"), _FileName);
                        HINHANH.SaveAs(_path);
                        mONAN.HINHANH = _FileName;

                        //get path of old image deleting it
                        _path = Path.Combine(Server.MapPath("~/Content/foodimages"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                        {
                            System.IO.File.Delete(_path);
                        }
                    }
                    else
                        mONAN.HINHANH = form["oldimage"];
                    db.Entry(mONAN).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "SystemManagement", new { area = "Admin" });
                }
                catch
                {
                    ViewBag.Message = "Không thành công!!";
                }
                return RedirectToAction("Index", "SystemManagement", new { area = "Admin" });
            }
            ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON", mONAN.MALOAIMON);
            return View(mONAN);
        }
        //public ActionResult Edit([Bind(Include = "MAMONAN,MALOAIMON,TENMONAN,HINHANH,DONGIA,DVT")] MONAN mONAN)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(mONAN).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MALOAIMON = new SelectList(db.LOAIMONs, "MALOAIMON", "TENLOAIMON", mONAN.MALOAIMON);
        //    return View(mONAN);
        //}


        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Lấy thông tin về vai trò có id tương ứng và trả về một PartialView chứa form Edit
            MONAN mONAN = db.MONANs.Find(id);// Lấy thông tin về vai trò từ id

            return PartialView("Delete", mONAN);
        }

        public ActionResult DeleteSubmit(int CatId)
        {
            MONAN mONAN = db.MONANs.Find(CatId);
            db.MONANs.Remove(mONAN);
            db.SaveChanges();
            return RedirectToAction("Index", "SystemManagement", new { area = "Admin" });
        }

        // GET: Admin/MONANs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MONAN mONAN = db.MONANs.Find(id);
        //    if (mONAN == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mONAN);
        //}

        // POST: Admin/MONANs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MONAN mONAN = db.MONANs.Find(id);
        //    db.MONANs.Remove(mONAN);
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
