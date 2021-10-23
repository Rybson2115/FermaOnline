using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace FermaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
      
        public IActionResult Index()
        {
        //    Experiment NewestExperiment = _db.Experiment
        //                       .OrderByDescending(t => t.Start)
        //                       .FirstOrDefault();
        //    NewestExperiment.SurveysList = _db.Surveys.Where(s => s.ExperimentId == NewestExperiment.Id).ToList();
        //    if(NewestExperiment.SurveysList!=null)
        //    return View(NewestExperiment);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
           
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
