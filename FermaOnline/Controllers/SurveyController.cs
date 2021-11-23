using FermaOnline.Data;
using FermaOnline.Facades;
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
        private readonly ApplicationDbContext _db;
        private readonly SurveyFacade surveyFacade;
        public SurveyController(ApplicationDbContext db)
        {
            _db = db;
            this.surveyFacade = new SurveyFacade(db);
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET-Create
        public IActionResult Create(int id)
        {
            ViewBag.IsFirstSurvay = !surveyFacade.IsFirst(id);
            ViewBag.CageNumber = surveyFacade.GetCageNumber(id);
            return View();
        }
        //POST-Create
        [HttpPost]
        public IActionResult Create(Survey formData)
        {
            if (ModelState.IsValid)
            {
                //int newId = surveyFacade.Create(formData);
                return RedirectToAction("Show", "Experiment"); //new { newId });
            }
            return View(formData);
        }
        public IActionResult Delete(int? id)
        {
            var toDelete = surveyFacade.Delete(id);
            if (toDelete == null)
                return NotFound();
            return View(toDelete);
        }
        // POST Delete  
        //[ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)  
        {
            var SurveysToDelete = surveyFacade.DeletePost(id);
         
            if (SurveysToDelete == null)
                return NotFound();
            return RedirectToAction("Show", "Experiment", new { id = SurveysToDelete.ExperimentId });
        }
    }
}