﻿using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Create(string name)
        {
            _db.Experiment.Add(new Experiment(name));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET-Show
        public IActionResult Show(int id) //Wyswietla eksperyment
        {
            var experiment = _db.Experiment.Find(id);//pobieranie danych z bazy 
            if (_db.Experiment.Find(id)==null)//sprawdza czy w bazie jest podane id
                return NotFound();

            experiment.SurveysList = _db.Surveys.Where(s => s.ExperymentId == id).ToList();//dodanie pomiarów dla danego eksperymentu po id
            var test = experiment.SurveysList.Select(t=> t.ExperymentId);
            experiment.SurveysList.ForEach(s =>
            {
                //s.A = _db.Cage.First(c => c.CageId == s.CageAId);
               // s.B = _db.Cage.First(c => c.CageId == s.CageBId);
            });  //pobierz cage


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
            var ToDelete = _db.Experiment.Find(id);
            if (ToDelete == null)
                return NotFound();
            
            _db.Surveys.RemoveRange(_db.Surveys.Where(s => s.ExperymentId== ToDelete.Id));
            _db.Experiment.Remove(ToDelete);
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


