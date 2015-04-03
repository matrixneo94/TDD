﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_project.Models;

namespace Mvc_project.Controllers
{
     [Authorize]
    public class ListsController : Controller
    {
        private MVCProjectDB db = new MVCProjectDB();

        //
        // GET: /Lists/
       
        public ActionResult Index()
        {
            return View(db.Links.ToList());
        }

        //
        // GET: /Lists/Details/5

        public ActionResult Details(int id = 0)
        {
            Links links = db.Links.Find(id);
            if (links == null)
            {
                return HttpNotFound();
            }
            return View(links);
        }

        //
        // GET: /Lists/Create
  
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Lists/Create

        [HttpPost]
    
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Links links)
        {
            links.AddDate = DateTime.Now.ToString();
            if (ModelState.IsValid)
            {
                db.Links.Add(links);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(links);
        }

        //
        // GET: /Lists/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Links links = db.Links.Find(id);
            if (links == null)
            {
                return HttpNotFound();
            }
            return View(links);
        }

        //
        // POST: /Lists/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Links links)
        {
            if (ModelState.IsValid)
            {
                db.Entry(links).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(links);
        }


        public ActionResult Delete(int id = 0)
        {
            Links links = db.Links.Find(id);
            if (links == null)
            {
                return HttpNotFound();
            }
            return View(links);
        }

        //
        // POST: /Lists/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Links links = db.Links.Find(id);
            db.Links.Remove(links);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}