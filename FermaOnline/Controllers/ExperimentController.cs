﻿using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FermaOnline.Controllers
{
    public class ExperimentController : Controller
    {
        private readonly ApplicationDbContext _db;//dostęp do bazy danych 
        public ExperimentController(ApplicationDbContext db)
        {
            _db = db;

        }

        //GET
        public IActionResult Index() //lista experymentów
        {
            IEnumerable<Experiment> ExperimentsList = _db.Experiment;//pobieranie danych z bazy 
            return View(ExperimentsList);
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]//zabezpieczenie 
        public IActionResult Create(Experiment formData, List<IFormFile> postedFiles)
        {
            var imgList = new List<Image>();
            if (postedFiles.Count > 0)
            {
                foreach (IFormFile postedFile in postedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine("~/img/ExperimentImages/", fileName), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }
                    var img = new Image(formData.Id, postedFile.FileName);
                    _db.Image.Add(img);
                    imgList.Add(img);
                }
         
                _db.Experiment.Add(new Experiment(formData.Name, formData.Description, formData.Species, imgList));
            }
            else
                _db.Experiment.Add(new Experiment(formData.Name, formData.Description, formData.Species));
            
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET-Show
        public IActionResult Show(int id) //Wyswietla eksperyment
        {
            var experiment = _db.Experiment.Find(id);//pobieranie danych z bazy 
            if (_db.Experiment.Find(id)==null)//sprawdza czy w bazie jest podane id
                return NotFound();
         
                experiment.SurveysList = _db.Surveys.Where(s => s.ExperimentId == id).ToList();//dodanie pomiarów dla danego eksperymentu po id

                //pobierz cage 
                experiment.SurveysList.ForEach(s =>
                {
                    s.A = _db.Cage.First(c => c.CageId == s.ACageId);
                     s.B = _db.Cage.First(c => c.CageId == s.BCageId);
                });
           
            //pobierz img 
            
                experiment.Images = _db.Image.Where(i => i.ExperimentId == id).ToList();
           
                return View(experiment);
        }

        // GET Delete
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
                return NotFound();
           
            var ToDelete = _db.Experiment.Find(id);
            
            if (ToDelete == null)
                return NotFound();
           
            return View(ToDelete);

        }

        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var ExperimetnToDelete = _db.Experiment.Find(id);
            if (ExperimetnToDelete == null)
                return NotFound();

            var SurveysToDelete = _db.Surveys.Where(s => s.ExperimentId == ExperimetnToDelete.Id);
            _db.Surveys.RemoveRange(SurveysToDelete);
          
            //tworzenie listy cage do usunięcia dla wszystkich pomiarów do usuniecia 
            var CageToDelete = new List<CageSurvey>();
            SurveysToDelete.ToList().ForEach(s =>
            {
                CageToDelete.Add(_db.Cage.First(c => c.CageId == s.ACageId));
                CageToDelete.Add(_db.Cage.First(c => c.CageId == s.BCageId));
            });
            _db.Cage.RemoveRange(CageToDelete);

            _db.Experiment.Remove(ExperimetnToDelete);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-Update
        public IActionResult Update(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var ToUpdate = _db.Experiment.Find(id);

            if (ToUpdate == null)
                return NotFound();

            return View(ToUpdate);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Experiment ToUpdate)
        {
            if (ModelState.IsValid)
            {
                _db.Experiment.Update(ToUpdate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ToUpdate);
        }

    }
}


