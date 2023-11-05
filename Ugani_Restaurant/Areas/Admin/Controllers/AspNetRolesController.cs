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
    //[Authorize(Roles = "Admin")]
    public class AspNetRolesController : Controller
    {

        private UGANI_1Entities db = new UGANI_1Entities();

        // GET: Admin/AspNetRoles
        public ActionResult Index()
        {
            return View(db.AspNetRoles.ToList());
        }

        // GET: Admin/AspNetRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AspNetRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                db.AspNetRoles.Add(aspNetRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetRole);
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            // Lấy thông tin về vai trò có id tương ứng và trả về một PartialView chứa form Edit
            AspNetRole role = db.AspNetRoles.Find(id);// Lấy thông tin về vai trò từ id

            return PartialView("Edit", role);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            // Lấy thông tin về vai trò có id tương ứng và trả về một PartialView chứa form Edit
            AspNetRole role = db.AspNetRoles.Find(id);// Lấy thông tin về vai trò từ id

            return PartialView("Delete", role);
        }

        public ActionResult DeleteSubmit(string CatId)
        {
            AspNetRole aspNetRole = db.AspNetRoles.Find(CatId);
            db.AspNetRoles.Remove(aspNetRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Admin/AspNetRoles/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetRole aspNetRole = db.AspNetRoles.Find(id);
        //    if (aspNetRole == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetRole);
        //}

        // POST: Admin/AspNetRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name")] AspNetRole aspNetRole)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(aspNetRole).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(aspNetRole);
        //}

        //public ActionResult DeleteModal(string CatId)
        //{
        //    AspNetRole roles = db.AspNetRoles.Where(m => m.Id == CatId).FirstOrDefault();
        //    return PartialView("ModalDelete", roles);
        //}

        // GET: Admin/AspNetRoles/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetRole aspNetRole = db.AspNetRoles.Find(id);
        //    if (aspNetRole == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetRole);
        //}

        //// POST: Admin/AspNetRoles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    AspNetRole aspNetRole = db.AspNetRoles.Find(id);
        //    db.AspNetRoles.Remove(aspNetRole);
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
