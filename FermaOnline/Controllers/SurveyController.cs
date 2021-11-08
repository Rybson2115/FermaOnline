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
        private readonly Facades.SurveyFacade facade;
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
            //sprawdzenie czy dodajemy pierwsze doświadczenie 
            bool SurveyExistInThisExperiment = _db.Surveys.Any(s => s.ExperimentId == id);
            Experiment experiment = _db.Experiment.Find(id);
            ViewBag.IsFirstSurvay = !SurveyExistInThisExperiment;
            ViewBag.CageNumber = experiment.CageNumber;
            return View();
        }

        //POST-Create
        [HttpPost]
        public IActionResult Create(SurveyDTO formData)
        {
            if (ModelState.IsValid)
            {
                int newId = facade.Create(formData);
                return RedirectToAction("Show", "Experiment", new { newId });
            }
            return View(formData);
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
     
        //[ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)  
        {
            var SurveysToDelete = _db.Surveys.Find(id);
          
            if (SurveysToDelete == null)
                return NotFound();

            var CageToDelete = new List<CageSurvey>();
            // SurveysToDelete.CagesIndex.ForEach(c => CageToDelete.Add(_db.Cage.Find(c.CageId))); //zrobić pobieranie cage 

            CageToDelete = _db.Cage.Where(c => c.SurveyId == id).ToList();
            
            _db.Cage.RemoveRange(CageToDelete);
            _db.Surveys.Remove(SurveysToDelete);
            _db.SaveChanges();

            return RedirectToAction("Show", "Experiment", new { id = SurveysToDelete.ExperimentId });
        }
    }
}
