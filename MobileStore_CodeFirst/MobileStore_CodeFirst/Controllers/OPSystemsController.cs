using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MobileStore_CodeFirst.Models;
using MobileStore_CodeFirst.Models.MobileEDM;

namespace MobileStore_CodeFirst.Controllers
{
    public class OPSystemsController : Controller
    {
        private MobileContext db = new MobileContext();

        // GET: OPSystems
        public ActionResult Index()
        {
            return View(db.OperatingSystems.ToList());
        }

        // GET: OPSystems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPSystem oPSystem = db.OperatingSystems.Find(id);
            if (oPSystem == null)
            {
                return HttpNotFound();
            }
            return View(oPSystem);
        }

        // GET: OPSystems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OPSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OPSystemId,OpSystemName,Description")] OPSystem oPSystem)
        {
            if (ModelState.IsValid)
            {
                if (CanAddOPSystem(oPSystem.OpSystemName))
                {
                    db.OperatingSystems.Add(oPSystem);
                    db.SaveChanges();
                    TempData["OPSystemStatusMessage"] = "Operating system has been added successfully!";
                    return RedirectToAction("Index");
                }
                DisplayOPSystemExistsMessage(oPSystem);
            }

            return View(oPSystem);
        }

        // GET: OPSystems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPSystem oPSystem = db.OperatingSystems.Find(id);
            if (oPSystem == null)
            {
                return HttpNotFound();
            }
            return View(oPSystem);
        }

        // POST: OPSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OPSystemId,OpSystemName,Description")] OPSystem oPSystem)
        {
            if (ModelState.IsValid)
            {
                if (CanUpdateOPSystem(oPSystem))
                {
                    db.Entry(oPSystem).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["OPSystemStatusMessage"] = "Operating system has been updated successfully!";
                    return RedirectToAction("Index");
                }
                DisplayOPSystemExistsMessage(oPSystem);
            }
            return View(oPSystem);
        }

        // GET: OPSystems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPSystem oPSystem = db.OperatingSystems.Find(id);
            if (oPSystem == null)
            {
                return HttpNotFound();
            }
            return View(oPSystem);
        }

        // POST: OPSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetForeignKeysToNull(id);
            OPSystem oPSystem = db.OperatingSystems.Find(id);
            db.OperatingSystems.Remove(oPSystem);
            db.SaveChanges();
            TempData["OPSystemStatusMessage"] = "Operating system has been deleted successfully!";
            return RedirectToAction("Index");
        }

        private bool CanAddOPSystem(string opsName)
        {
            var query = db.OperatingSystems.Any(o => o.OpSystemName == opsName);
            return !query;
        }
        private bool CanUpdateOPSystem(OPSystem opS)
        {
            var query = db.OperatingSystems.Where(o => o.OPSystemId != opS.OPSystemId).Any(o => o.OpSystemName == opS.OpSystemName);
            return !query;
        }
        private void DisplayOPSystemExistsMessage(OPSystem o)
        {
            ModelState.AddModelError("OpSystemName", $"Model {o.OpSystemName} already exists please choose another name.");
        }

        private void SetForeignKeysToNull(int id)
        {
            var opSys = db.OperatingSystems.Find(id);

            var query = from m in opSys.Models
                        where m.OPSystemId == opSys.OPSystemId
                        select new { m };

            query.ToList().ForEach(x => x.m.OPSystemId = null);

            db.Entry(opSys).State = EntityState.Modified;
            db.SaveChanges();
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
