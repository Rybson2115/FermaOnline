﻿using FermaOnline.Data;
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
            ViewBag.IsFirstSurvay = !SurveyExistInThisExperiment;
            return View();
        }

        //POST-Create
        [HttpPost]
        public IActionResult Create(Survey formData)
        {
            int id = formData.ExperimentId;
            Experiment experiment = _db.Experiment.Find(id);

            if (_db.Surveys.Any(s => s.ExperimentId == id))
            {
                Survey lastSurvey = _db.Surveys
                                .Where(s => s.ExperimentId == id)
                                .OrderByDescending(t => t.SurveyDate)
                                .FirstOrDefault();

                //pobranie cage dla lastSurvey 
              lastSurvey.CagesIndex.ForEach(id => lastSurvey.Cages.Add(_db.Cage.Find(id.CageId)));

                _db.Surveys.Add(new Survey(formData, lastSurvey, experiment.CageFirstIndividualBodyWeight));
            }
            else
            {
                formData.ExperimentId = id;
                var DataToAdd = new Survey(formData);
                experiment.Start = DataToAdd.SurveyDate;
                DataToAdd.Cages.ForEach(c => experiment.CageFirstIndividualBodyWeight.Add(c.IndividualBodyWeight));
                experiment.Status = true;
                _db.Surveys.Add(DataToAdd);
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
     
        //[ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id) //nie dostaje id id jest null
        {
            var SurveysToDelete = _db.Surveys.Find(id);
          
            if (SurveysToDelete == null)
                return NotFound();

            var CageToDelete = new List<CageSurvey>();
            SurveysToDelete.CagesIndex.ForEach(c => CageToDelete.Add(_db.Cage.Find(c.CageId)));

            _db.Cage.RemoveRange(CageToDelete);
            _db.Surveys.Remove(SurveysToDelete);
            _db.SaveChanges();

            return RedirectToAction("Show", "Experiment", new { id = SurveysToDelete.ExperimentId });
        }
    }
}
