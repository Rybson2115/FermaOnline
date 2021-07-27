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
        private readonly ApplicationDbContext _db;//dostęp do bazy danych 
        public SurveyController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]//zabezpieczenie 
        public IActionResult Create( Survey formData)
        { 
           int id = formData.ExperymentId;
          Experiment experiment = _db.Experiment.Find(id);

            if (_db.Surveys.ToList().Exists(s => s.ExperymentId == id)) { 
                Survey lastSurvey = _db.Surveys
                                .Where(s => s.ExperymentId == id)
                                .OrderByDescending(t => t.SurveyDate)
                                .FirstOrDefault();
                formData.SetData(formData, lastSurvey, experiment.AFirstIndividualBodyWeight, experiment.BFirstIndividualBodyWeight);
                _db.Surveys.Add(formData);
            }
            else
            {
                _db.Surveys.Add(new Survey(formData));
                experiment.Start = formData.SurveyDate;
                experiment.AFirstIndividualBodyWeight = formData.A.IndividualBodyWeight;
                experiment.BFirstIndividualBodyWeight = formData.B.IndividualBodyWeight;
                _db.Experiment.Update(experiment);
            }
            _db.SaveChanges();
            
            return RedirectToAction("Show", "Experiment", new { id = id });
        }
    }
}
