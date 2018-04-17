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
    public class SkillTypesController : Controller
    {
        private Entities db = new Entities();

        // GET: SkillTypes
        public ActionResult Index()
        {
            return View(db.SkillTypeSet.ToList());
        }

        // GET: SkillTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillType skillType = db.SkillTypeSet.Find(id);
            if (skillType == null)
            {
                return HttpNotFound();
            }
            return View(skillType);
        }

        // GET: SkillTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillTypes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SkillType skillType)
        {
            if (ModelState.IsValid)
            {
                db.SkillTypeSet.Add(skillType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skillType);
        }

        // GET: SkillTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillType skillType = db.SkillTypeSet.Find(id);
            if (skillType == null)
            {
                return HttpNotFound();
            }
            return View(skillType);
        }

        // POST: SkillTypes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SkillType skillType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skillType);
        }

        // GET: SkillTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillType skillType = db.SkillTypeSet.Find(id);
            if (skillType == null)
            {
                return HttpNotFound();
            }
            return View(skillType);
        }

        // POST: SkillTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkillType skillType = db.SkillTypeSet.Find(id);
            db.SkillTypeSet.Remove(skillType);
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
