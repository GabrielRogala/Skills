using SkillShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkillShare.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Skill = new SelectList(db.Skill, "Id", "Name");
            ViewBag.LocationId = new SelectList(db.Location, "Id", "Id");
            ViewBag.Level = new SelectList(db.Level, "Id", "Name");
            ViewBag.TeamId = new SelectList(db.Team, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult About([Bind(Include = "FirstName,SecondName,TeamId,LocationId")] SearchModel search)
        {
            if (ModelState.IsValid)
            {
                var p = db.Person
                .Where(b => b.Team.Name == search.teamId.Name)
                .FirstOrDefault();

                return RedirectToAction("Contact",p);
            }

            return View(search);
        }

        //public ActionResult Contact()
       // {
        //    ViewBag.Message = "Your contact page.";
//
       //     return View();
//}
    }
}