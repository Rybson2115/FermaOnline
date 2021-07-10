using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FermaOnline.Data;
using FermaOnline.Models;

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

            IEnumerable<Survey> objList = _db.Surveys;//pobieranie danych z bazy 
            return View(objList);
        }
    }
}
