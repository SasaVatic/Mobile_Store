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
    public class BrandsController : Controller
    {
        private MobileContext db = new MobileContext();

        // GET: Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandId,BrandName")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (CanAddBrand(brand.BrandName))
                {
                    db.Brands.Add(brand);
                    db.SaveChanges();
                    TempData["BrandStatusMessage"] = "Brand has been added successfully!";
                    return RedirectToAction("Index");
                }
                DisplayBrandExistsMessage(brand);
            }

            return View(brand);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandId,BrandName")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (CanUpdateBrand(brand))
                {
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["BrandStatusMessage"] = "Brand has been updated successfully!";
                    return RedirectToAction("Index");
                }
                DisplayBrandExistsMessage(brand);
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
            db.SaveChanges();
            TempData["BrandStatusMessage"] = "Brand has been deleted successfully!";
            return RedirectToAction("Index");
        }

        private bool CanAddBrand(string brandName)
        {
            var query = db.Brands.Any(b => b.BrandName == brandName);
            return !query;
        }
        private bool CanUpdateBrand(Brand brand)
        {
            var query = db.Brands.Where(b => b.BrandId != brand.BrandId).Any(b => b.BrandName == brand.BrandName);
            return !query;
        }
        private void DisplayBrandExistsMessage(Brand b)
        {
            ModelState.AddModelError("BrandName", $"Brand {b.BrandName} already exists please choose another name.");
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
