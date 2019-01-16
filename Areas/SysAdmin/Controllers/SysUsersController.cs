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
    public class SysUsersController : Controller
    {
        private SCBaseProjectDBEntities db = new SCBaseProjectDBEntities();

        // GET: SysAdmin/SysUsers
        public ActionResult Index()
        {
            return View(db.SysUsers.ToList());
        }

        // GET: SysAdmin/SysUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // GET: SysAdmin/SysUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysAdmin/SysUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,Logo,Domain,ContactName,Email,Phone,Fax,BillingInfo,UserName,Password,IsActive,IsDeleted,CreatedBy,CreatedDate")] SysUser sysUser)
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

                        sysUser.Logo = "/Images/" + fileName;
                    }
                }
                db.SysUsers.Add(sysUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysUser);
        }

        // GET: SysAdmin/SysUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // POST: SysAdmin/SysUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Logo,Domain,ContactName,Email,Phone,Fax,BillingInfo,UserName,Password,IsActive,IsDeleted,CreatedBy,CreatedDate")] SysUser sysUser)
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

                        sysUser.Logo = "/Images/" + fileName;
                    }
                }
                db.Entry(sysUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysUser);
        }

        // GET: SysAdmin/SysUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // POST: SysAdmin/SysUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysUser);
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
