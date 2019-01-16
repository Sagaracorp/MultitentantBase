using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCBaseProject.Models;

namespace SCBaseProject.Areas.SysAdmin.Controllers
{
    public class SysUserCompaniesController : Controller
    {
        private SCBaseProjectDBEntities db = new SCBaseProjectDBEntities();

        // GET: SysAdmin/SysUserCompanies
        public ActionResult Index()
        {
            var sysUserCompanies = db.SysUserCompanies.Include(s => s.SysCompany).Include(s => s.SysUser);
            return View(sysUserCompanies.ToList());
        }

        // GET: SysAdmin/SysUserCompanies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUserCompany sysUserCompany = db.SysUserCompanies.Find(id);
            if (sysUserCompany == null)
            {
                return HttpNotFound();
            }
            return View(sysUserCompany);
        }

        // GET: SysAdmin/SysUserCompanies/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.SysCompanies, "ID", "Name");
            ViewBag.UserID = new SelectList(db.SysUsers, "ID", "Name");
            return View();
        }

        // POST: SysAdmin/SysUserCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,CompanyID,IsActive,CreatedBy,CreatedDate")] SysUserCompany sysUserCompany)
        {
            if (ModelState.IsValid)
            {
                db.SysUserCompanies.Add(sysUserCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.SysCompanies, "ID", "Name", sysUserCompany.CompanyID);
            ViewBag.UserID = new SelectList(db.SysUsers, "ID", "Name", sysUserCompany.UserID);
            return View(sysUserCompany);
        }

        // GET: SysAdmin/SysUserCompanies/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUserCompany sysUserCompany = db.SysUserCompanies.Find(id);
            if (sysUserCompany == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.SysCompanies, "ID", "Name", sysUserCompany.CompanyID);
            ViewBag.UserID = new SelectList(db.SysUsers, "ID", "Name", sysUserCompany.UserID);
            return View(sysUserCompany);
        }

        // POST: SysAdmin/SysUserCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,CompanyID,IsActive,CreatedBy,CreatedDate")] SysUserCompany sysUserCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysUserCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.SysCompanies, "ID", "Name", sysUserCompany.CompanyID);
            ViewBag.UserID = new SelectList(db.SysUsers, "ID", "Name", sysUserCompany.UserID);
            return View(sysUserCompany);
        }

        // GET: SysAdmin/SysUserCompanies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUserCompany sysUserCompany = db.SysUserCompanies.Find(id);
            if (sysUserCompany == null)
            {
                return HttpNotFound();
            }
            return View(sysUserCompany);
        }

        // POST: SysAdmin/SysUserCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SysUserCompany sysUserCompany = db.SysUserCompanies.Find(id);
            db.SysUserCompanies.Remove(sysUserCompany);
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
