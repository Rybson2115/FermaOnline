using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExperymentController : Controller
    {
        private readonly ApplicationDbContext _db;//dostęp do bazy danych 
        public ExperymentController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            //
            IEnumerable<Experiment> ExperimentsList = _db.Experiment;//pobieranie danych z bazy 
            //IEnumerable<Survey> SurveysList = _db.Surveys;//pobieranie danych z bazy 
            //IEnumerable<CageSurvey> CagesList = _db.CageSurvey;//pobieranie danych z bazy 

            //Experiment ShowExperyment = null;
            //Wybierz z listy experymentów po id jeden do wyświetlenia 
            //wyubierz i przypisz pomiary dla tego eksperymentu 
            //przypisz pomiarą klatki po id 
            return View(ExperimentsList);
        }
        
        /*
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var ExperymentList = await _db.Experiment.ToListAsync();//pobieranie danych z bazy 
            return new JsonResult(ExperymentList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var experiment = await _db.Experiment.FirstOrDefaultAsync(e => e.ExperymentId == id);

            //  var SurveysList = await _db.Surveys.Where(s => s experiment.SurveysList);//pobieranie danych z bazy 

            //IEnumerable<CageSurvey> CagesList = _db.CageSurvey;//pobieranie danych z bazy 

            //Wybierz z listy experymentów po id jeden do wyświetlenia 
            //wyubierz i przypisz pomiary dla tego eksperymentu 
            //przypisz pomiarą klatki po id 
            //var experiment =  
            return new JsonResult(experiment);
        }

        */
    }
}


