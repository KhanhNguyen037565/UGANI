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
    //[Authorize(Roles = "Admin")]
    public class LOAIKHONGGIANsController : Controller
    {
        private UGANI_1Entities db = new UGANI_1Entities();

        // GET: Admin/LOAIKHONGGIANs
        public ActionResult Index()
        {
            return View(db.LOAIKHONGGIANs.ToList());
        }


        // GET: Admin/LOAIKHONGGIANs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LOAIKHONGGIANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MALOAIKHONGGIAN,TENLOAIKHONGGIAN,IMG,MOTA,DONGIA,DVT")] LOAIKHONGGIAN lOAIKHONGGIAN, HttpPostedFileBase IMG)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (IMG.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(IMG.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/khonggian"), _FileName);
                        IMG.SaveAs(_path);
                        lOAIKHONGGIAN.IMG = _FileName;
                    }
                    db.LOAIKHONGGIANs.Add(lOAIKHONGGIAN);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }
            //if (ModelState.IsValid)
            //{
            //    db.LOAIKHONGGIANs.Add(lOAIKHONGGIAN);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(lOAIKHONGGIAN);
        }

        // GET: Admin/LOAIKHONGGIANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIKHONGGIAN lOAIKHONGGIAN = db.LOAIKHONGGIANs.Find(id);
            if (lOAIKHONGGIAN == null)
            {
                return HttpNotFound();
            }
            return View(lOAIKHONGGIAN);
        }

        // POST: Admin/LOAIKHONGGIANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MALOAIKHONGGIAN,TENLOAIKHONGGIAN,IMG,MOTA,DONGIA,DVT")] LOAIKHONGGIAN lOAIKHONGGIAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAIKHONGGIAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAIKHONGGIAN);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Lấy thông tin về vai trò có id tương ứng và trả về một PartialView chứa form Edit
            LOAIKHONGGIAN lOAIKHONGGIAN = db.LOAIKHONGGIANs.Find(id);// Lấy thông tin về vai trò từ id

            return PartialView("Delete", lOAIKHONGGIAN);
        }

        public ActionResult DeleteSubmit(int CatId)
        {
            LOAIKHONGGIAN lOAIKHONGGIAN = db.LOAIKHONGGIANs.Find(CatId);
            db.LOAIKHONGGIANs.Remove(lOAIKHONGGIAN);
            db.SaveChanges();
            return RedirectToAction("Index");   
        }


        // GET: Admin/LOAIKHONGGIANs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LOAIKHONGGIAN lOAIKHONGGIAN = db.LOAIKHONGGIANs.Find(id);
        //    if (lOAIKHONGGIAN == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lOAIKHONGGIAN);
        //}

        // POST: Admin/LOAIKHONGGIANs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LOAIKHONGGIAN lOAIKHONGGIAN = db.LOAIKHONGGIANs.Find(id);
        //    db.LOAIKHONGGIANs.Remove(lOAIKHONGGIAN);
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
