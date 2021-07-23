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
        public IActionResult Create(int ExperymentId, Survey formData)
        {
         
            //_db.Surveys.Add(new Survey(ExperymentId,formData.A.GroupWeight));
            //_db.SaveChanges();
            return RedirectToPage("Experiment/Show.cshtml");
        }
    }
}
