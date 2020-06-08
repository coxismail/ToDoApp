using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoAPP.Models;

namespace ToDoAPP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var today = DateTime.Now;
            var userid = User.Identity.GetUserId();
            var myTask = db.MyTask.Where(s => s.Date == today.Date && s.Category.UserId == userid).Include(m => m.Category).OrderByDescending(s => s.Date).ToList();
            return View(myTask);
        }


        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var cat = db.Category.Where(s => s.UserId == userId).ToList();
            ViewBag.CategoryId = cat;
            return View();
        }

        // POST: MyTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyTask myTask)
        {
            if (ModelState.IsValid)
            {
                myTask.Id = Guid.NewGuid();
                db.MyTask.Add(myTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var userId = User.Identity.GetUserId();
            var cat = db.Category.Where(s => s.UserId == userId).ToList();
            ViewBag.CategoryId = new SelectList(cat, "Id", "Name", myTask.CategoryId);
            return View(myTask);
        }

        // GET: MyTask/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyTask myTask = db.MyTask.Find(id);
            if (myTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", myTask.CategoryId);
            return View(myTask);
        }

        // POST: MyTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MyTask myTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", myTask.CategoryId);
            return View(myTask);
        }



        // POST: MyTask/Delete/5

        public ActionResult Delete(Guid id)
        {
            try
            {
                MyTask myTask = db.MyTask.Find(id);
                db.MyTask.Remove(myTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Done(Guid id)
        {
            try
            {
                MyTask myTask = db.MyTask.Find(id);
                myTask.IsPending = false;
                myTask.IsComplete = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult MyTaskOn(DateTime date)
        {
            try
            {
                var sdate = date.Date;
                var userid = User.Identity.GetUserId();
                var myTask = db.MyTask.Where(s => s.Date == sdate && s.Category.UserId == userid).Include(m => m.Category).OrderByDescending(s => s.Date).ToList();
                ViewBag.Date = sdate.ToString("dd - MMM - yyyy");
                return View(myTask);
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult Monthly()
        {
            try
            {
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime edate = date.AddMonths(1).AddDays(-1);

                var userid = User.Identity.GetUserId();
                var myTask = db.MyTask.Where(s => s.Date.Month >= date.Month && s.Date.Month<=edate.Month && s.Category.UserId == userid).Include(m => m.Category).OrderByDescending(s => s.Date).ToList();
                ViewBag.Date = date.ToString("MMM - yyyy");
                return View(myTask);
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Monthly(DateTime month)
        {
            try
            {
                var date = month;
                DateTime edate = date.AddMonths(1).AddDays(-1);

                var userid = User.Identity.GetUserId();
                var myTask = db.MyTask.Where(s => s.Date.Month >= date.Month && s.Date.Month <= edate.Month && s.Category.UserId == userid).Include(m => m.Category).OrderByDescending(s => s.Date).ToList();
                ViewBag.Date = date.ToString("MMM - yyyy");
                return View(myTask);
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }
        public ActionResult Pending()
        {
            var userid = User.Identity.GetUserId();
            var myTask = db.MyTask.Where(s=>s.Category.UserId == userid && s.IsPending== true).Include(m => m.Category).OrderByDescending(s => s.Date).ToList();
            return View(myTask);
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