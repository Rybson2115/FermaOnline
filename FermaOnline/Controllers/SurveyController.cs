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
                Facades.ExperimentFacade.Create(formData);
                
                int id = formData.ExperimentId;
                Experiment experiment = _db.Experiment.Find(id);

                if (_db.Surveys.Any(s => s.ExperimentId == id))
                {
                    Survey lastSurvey = _db.Surveys
                                    .Where(s => s.ExperimentId == id)
                                    .OrderByDescending(t => t.SurveyDate)
                                    .FirstOrDefault();

                    //pobranie cage dla lastSurvey 
                    lastSurvey.Cages = _db.Cage.Where(c => c.SurveyId == lastSurvey.SurveyId).ToList();
                    //pobieranie CageFirstIndividualBodyWeight
                    experiment.CageFirstIndividualBodyWeight = _db.CFIBW.Where(f => f.ExperimentId == experiment.Id).Select(s => s.FirstIndividualBodyWeight).ToList();
                    //dodanie survey do bazy 
                    _db.Surveys.Add(new Survey(formData, lastSurvey, experiment.CageFirstIndividualBodyWeight, (int)experiment.CageNumber));
                }
                else
                {
                    formData.ExperimentId = id;
                    var DataToAdd = new Survey(formData);
                    experiment.Start = (DateTime)DataToAdd.SurveyDate;
                    //dodanie CageFirstIndividualBodyWeight do bazy  
                    DataToAdd.Cages.ForEach(c => _db.CFIBW.Add(new CageFirstIndividualBodyWeight(DataToAdd.ExperimentId, c.CageId, c.IndividualBodyWeight)));
                    experiment.Status = true;
                    _db.Surveys.Add(DataToAdd);
                    _db.Experiment.Update(experiment);
                }
                _db.SaveChanges();

                return RedirectToAction("Show", "Experiment", new { id });
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
