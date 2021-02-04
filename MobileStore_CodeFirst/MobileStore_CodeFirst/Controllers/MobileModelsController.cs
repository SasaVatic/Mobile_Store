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
    public class MobileModelsController : Controller
    {
        private MobileContext db = new MobileContext();

        // GET: MobileModels
        public ActionResult Index()
        {
            var models = db.Models.Include(m => m.Brand).Include(m => m.OPSystem);
            return View(models.ToList());
        }

        // GET: MobileModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileModel mobileModel = db.Models.Find(id);
            if (mobileModel == null)
            {
                return HttpNotFound();
            }
            return View(mobileModel);
        }

        // GET: MobileModels/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            ViewBag.OPSystemId = new SelectList(db.OperatingSystems, "OPSystemId", "OpSystemName");
            return View();
        }

        // POST: MobileModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MobileModelId,ModelName,BrandId,Price,RAM,StorageCapacity,OPSystemId")] MobileModel mobileModel)
        {
            if (ModelState.IsValid)
            {
                if (CanAddModel(mobileModel.ModelName))
                {
                    db.Models.Add(mobileModel);
                    db.SaveChanges();
                    TempData["ModelStatusMessage"] = "Model has been added successfully!";
                    return RedirectToAction("Index");
                }
                DisplayModelExistsMessage(mobileModel);
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", mobileModel.BrandId);
            ViewBag.OPSystemId = new SelectList(db.OperatingSystems, "OPSystemId", "OpSystemName", mobileModel.OPSystemId);
            return View(mobileModel);
        }

        // GET: MobileModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileModel mobileModel = db.Models.Find(id);
            if (mobileModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", mobileModel.BrandId);
            ViewBag.OPSystemId = new SelectList(db.OperatingSystems, "OPSystemId", "OpSystemName", mobileModel.OPSystemId);
            return View(mobileModel);
        }

        // POST: MobileModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MobileModelId,ModelName,BrandId,Price,RAM,StorageCapacity,OPSystemId")] MobileModel mobileModel)
        {
            if (ModelState.IsValid)
            {
                if (CanUpdateModel(mobileModel))
                {
                    db.Entry(mobileModel).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ModelStatusMessage"] = "Model has been updated successfully!";
                    return RedirectToAction("Index");
                }
                DisplayModelExistsMessage(mobileModel);
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", mobileModel.BrandId);
            ViewBag.OPSystemId = new SelectList(db.OperatingSystems, "OPSystemId", "OpSystemName", mobileModel.OPSystemId);
            return View(mobileModel);
        }

        // GET: MobileModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobileModel mobileModel = db.Models.Find(id);
            if (mobileModel == null)
            {
                return HttpNotFound();
            }
            return View(mobileModel);
        }

        // POST: MobileModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MobileModel mobileModel = db.Models.Find(id);
            db.Models.Remove(mobileModel);
            TempData["ModelStatusMessage"] = "Model has been deleted successfully!";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool CanAddModel(string modelName)
        {
            var query = db.Models.Any(m => m.ModelName == modelName);
            return !query;
        }

        private bool CanUpdateModel(MobileModel mbM)
        {
            var query = db.Models.Where(m => m.MobileModelId != mbM.MobileModelId).Any(m => m.ModelName == mbM.ModelName);
            return !query;
        }
        private void DisplayModelExistsMessage(MobileModel m)
        {
            ModelState.AddModelError("ModelName", $"Model {m.ModelName} already exists please choose another name.");
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
