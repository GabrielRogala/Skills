using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkillShare.Models;

namespace SkillShare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SkillListsController : Controller
    {
        private Entities db = new Entities();

        // GET: SkillLists
        public ActionResult Index()
        {
            var skillList = db.SkillList.Include(s => s.Person).Include(s => s.Skill).Include(s => s.Level);
            return View(skillList.ToList());
        }

        // GET: SkillLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillList skillList = db.SkillList.Find(id);
            if (skillList == null)
            {
                return HttpNotFound();
            }
            return View(skillList);
        }

        // GET: SkillLists/Create
        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Signum");
            ViewBag.SkillId = new SelectList(db.Skill, "Id", "Name");
            ViewBag.LevelId = new SelectList(db.Level, "Id", "Name");
            return View();
        }

        // POST: SkillLists/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonId,SkillId,LevelId")] SkillList skillList)
        {
            if (ModelState.IsValid)
            {
                db.SkillList.Add(skillList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(db.Person, "Id", "Signum", skillList.PersonId);
            ViewBag.SkillId = new SelectList(db.Skill, "Id", "Name", skillList.SkillId);
            ViewBag.LevelId = new SelectList(db.Level, "Id", "Name", skillList.LevelId);
            return View(skillList);
        }

        // GET: SkillLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillList skillList = db.SkillList.Find(id);
            if (skillList == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Signum", skillList.PersonId);
            ViewBag.SkillId = new SelectList(db.Skill, "Id", "Name", skillList.SkillId);
            ViewBag.LevelId = new SelectList(db.Level, "Id", "Name", skillList.LevelId);
            return View(skillList);
        }

        // POST: SkillLists/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonId,SkillId,LevelId")] SkillList skillList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.Person, "Id", "Signum", skillList.PersonId);
            ViewBag.SkillId = new SelectList(db.Skill, "Id", "Name", skillList.SkillId);
            ViewBag.LevelId = new SelectList(db.Level, "Id", "Name", skillList.LevelId);
            return View(skillList);
        }

        // GET: SkillLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillList skillList = db.SkillList.Find(id);
            if (skillList == null)
            {
                return HttpNotFound();
            }
            return View(skillList);
        }

        // POST: SkillLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkillList skillList = db.SkillList.Find(id);
            db.SkillList.Remove(skillList);
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
