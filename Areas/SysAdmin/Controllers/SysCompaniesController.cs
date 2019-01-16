using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCBaseProject.Models;
using System.IO;

namespace SCBaseProject.Areas.SysAdmin.Controllers
{
    public class SysCompaniesController : Controller
    {
        private SCBaseProjectDBEntities db = new SCBaseProjectDBEntities();

        // GET: SysAdmin/SysCompanies
        public ActionResult Index()
        {
            return View(db.SysCompanies.ToList());
        }

        // GET: SysAdmin/SysCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysCompany sysCompany = db.SysCompanies.Find(id);
            if (sysCompany == null)
            {
                return HttpNotFound();
            }
            return View(sysCompany);
        }

        // GET: SysAdmin/SysCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysAdmin/SysCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,Logo,Domain,ContactName,Email,Phone,Fax,BillingInfo,IsActive,IsDeleted,CreatedBy,CreatedDate")] SysCompany sysCompany)
        {
            if (ModelState.IsValid)
            {

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(path);

                        sysCompany.Logo = "/Images/" + fileName;
                    }
                }

                db.SysCompanies.Add(sysCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysCompany);
        }

        // GET: SysAdmin/SysCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysCompany sysCompany = db.SysCompanies.Find(id);
            if (sysCompany == null)
            {
                return HttpNotFound();
            }
            return View(sysCompany);
        }

        // POST: SysAdmin/SysCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Logo,Domain,ContactName,Email,Phone,Fax,BillingInfo,IsActive,IsDeleted,CreatedBy,CreatedDate")] SysCompany sysCompany)
        {
            if (ModelState.IsValid)
            {

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(path);

                        sysCompany.Logo = "/Images/" + fileName;
                    }
                }

                db.Entry(sysCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysCompany);
        }

        // GET: SysAdmin/SysCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysCompany sysCompany = db.SysCompanies.Find(id);
            if (sysCompany == null)
            {
                return HttpNotFound();
            }
            return View(sysCompany);
        }

        // POST: SysAdmin/SysCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysCompany sysCompany = db.SysCompanies.Find(id);
            db.SysCompanies.Remove(sysCompany);
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
