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
        public IActionResult Show(int id) //lista experymentów
        {
            var experiment = _db.Experiment.Find(id);//pobieranie danych z bazy 
            if (_db.Experiment.Find(id)==null)//sprawdza czy w bazie jest podane id
                return NotFound();

            experiment.SurveysList = _db.Surveys.Where(s => s.ExperymentId == id).ToList();
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

    }
}


