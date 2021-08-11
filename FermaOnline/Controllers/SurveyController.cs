using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _db;//dostęp do bazy danych  /ja bym to jakoś repo nazwał 
        public SurveyController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET-Create
        public IActionResult Create(int id)
        {
            bool SurveyExistInThisExperiment = _db.Surveys.Any(s => s.ExperymentId == id);
            ViewBag.IsFirstSurvay = !SurveyExistInThisExperiment;
            return View();
        }

        //POST-Create
        [HttpPost]
        public IActionResult Create(Survey formData)
        {
            int id = formData.ExperymentId;
            Experiment experiment = _db.Experiment.Find(id);
            if (_db.Surveys.Any(s => s.ExperymentId == id))
            {
                Survey lastSurvey = _db.Surveys
                                .Where(s => s.ExperymentId == id)
                                .OrderByDescending(t => t.SurveyDate)
                                .FirstOrDefault();
                _db.Surveys.Add(new Survey(formData, lastSurvey, experiment.AFirstIndividualBodyWeight, experiment.BFirstIndividualBodyWeight));
            }
            else
            {
                experiment.Start = formData.SurveyDate;
                experiment.AFirstIndividualBodyWeight = formData.A.IndividualBodyWeight;
                experiment.BFirstIndividualBodyWeight = formData.B.IndividualBodyWeight;
                _db.Surveys.Add(new Survey(formData));
                _db.Experiment.Update(experiment);
            }
            _db.SaveChanges();

            return RedirectToAction("Show", "Experiment", new { id = id });
        }
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var ToDelete = _db.Surveys.Find(id);

            if (ToDelete == null)
                return NotFound();

            return View(ToDelete);

        }

        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var ToDelete = _db.Surveys.Find(id);
            if (ToDelete == null)
                return NotFound();

            _db.Cage.Remove(ToDelete.A);
            _db.Cage.Remove(ToDelete.B);
            _db.Surveys.Remove(ToDelete);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
