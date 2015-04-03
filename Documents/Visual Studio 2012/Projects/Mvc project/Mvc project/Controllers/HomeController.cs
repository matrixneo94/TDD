﻿using Mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PagedList;

namespace Mvc_project.Controllers
{
    public class HomeController : Controller
    {
        MVCProjectDB _db = new MVCProjectDB();
        public ActionResult Autocomplete(string temp)
        {
            var list = _db.Links.OrderByDescending(r => r.Raiting)
               .Where(r => r.Title.StartsWith(temp))
               .Take(10)
               .Select(r => new
               {
                   Title = r.Title,
               }

               );
            return Json(list, JsonRequestBehavior.AllowGet);
        }
 
        public ActionResult IncreaseRaiting(int id =0)
        {
            Links links = _db.Links.Find(id);
            links.Raiting = links.Raiting + 1;
            _db.Entry(links).State = EntityState.Modified;
            _db.SaveChanges();
            
            return RedirectToAction("Index");

        }
        public ActionResult Index(string search = null, int page =1)
        {
          
            var list = _db.Links.OrderByDescending(r => r.Raiting)      
                .Where(r=> search ==null||r.Title.StartsWith(search.ToLower()))               
                .Select(r => new LinksListViewModel
            {
                ID = r.ID,
                Link = r.Link,
                Title = r.Title,
                Raiting = r.Raiting,
                ShortDescription = r.ShortDescription

            }).ToPagedList(page, 10);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Lists", list);
            }

            return View(list);
        }    
        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
