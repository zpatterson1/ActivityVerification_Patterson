using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ActivityVerification.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ActivityVerification.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index(string search, int? pageNumber)
        {
            ViewBag.Title = "Activities Dashboard";
            string userId = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id.ToString();
            if (String.IsNullOrEmpty(search))
            {
                return View(db.Activities.Where(x=> x.UserID == userId).OrderByDescending(s => s.Id).ToList().ToPagedList(pageNumber ?? 1, 10));
            }
            else
            {
                return View(db.Activities.Where(x => (x.UserID == userId) && (x.Description.Contains(search) || x.Name.Contains(search))).OrderByDescending(s => s.Id).ToList().ToPagedList(pageNumber ?? 1, 10));
            }
        }

        public ActionResult _details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return PartialView(activity);
        }

        public ActionResult Create()
        {
            SelectListItem defaultItem = new SelectListItem()
            {
                Text = "-select-",
                Selected = true
            };
            ViewBag.RecognitionReason = new SelectList(db.RecognitionReasons, "Id", "Reason", defaultItem);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Type = new SelectList(db.ActivityTypes, "Id", "Type", defaultItem);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserID,Description,Type,Verified,RecognitionReason,Name,Created,Updated")] Activity activity)
        {
            activity.Updated = DateTime.Now;
            activity.UserID = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id.ToString();

            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecognitionReason = new SelectList(db.RecognitionReasons, "Id", "Reason", activity.RecognitionReason);
            ViewBag.Type = new SelectList(db.ActivityTypes, "Id", "Type", activity.Type);
            return View(activity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecognitionReason = new SelectList(db.RecognitionReasons, "Id", "Reason", activity.RecognitionReason);
            ViewBag.Type = new SelectList(db.ActivityTypes, "Id", "Type", activity.Type);
            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Type,Verified,RecognitionReason,Name,Updated,Created")] Activity activity)
        {
            activity.UserID = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id.ToString();
            if (ModelState.IsValid)
            {
                activity.Updated = DateTime.Now;
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecognitionReason = new SelectList(db.RecognitionReasons, "Id", "Reason", activity.RecognitionReason);
            ViewBag.Type = new SelectList(db.ActivityTypes, "Id", "Type", activity.Type);
            return View(activity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
