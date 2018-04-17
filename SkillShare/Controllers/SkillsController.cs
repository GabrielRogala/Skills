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
    public class SkillsController : Controller
    {
        private Entities db = new Entities();

        // GET: Skills
        public ActionResult Index()
        {
            var skill = db.Skill.Include(s => s.SkillType);
            return View(skill.ToList());
        }

        // GET: Skills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            ViewBag.SkillTypeId = new SelectList(db.SkillTypeSet, "Id", "Name");
            return View();
        }

        // POST: Skills/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SkillTypeId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Skill.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SkillTypeId = new SelectList(db.SkillTypeSet, "Id", "Name", skill.SkillTypeId);
            return View(skill);
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            ViewBag.SkillTypeId = new SelectList(db.SkillTypeSet, "Id", "Name", skill.SkillTypeId);
            return View(skill);
        }

        // POST: Skills/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SkillTypeId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SkillTypeId = new SelectList(db.SkillTypeSet, "Id", "Name", skill.SkillTypeId);
            return View(skill);
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skill.Find(id);
            db.Skill.Remove(skill);
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
