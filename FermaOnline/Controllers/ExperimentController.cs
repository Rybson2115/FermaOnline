using FermaOnline.Data;
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
        public IActionResult Create(Experiment formData)
        {
 
            Experiment ExperimentToAdd;
            ExperimentToAdd = new Experiment(formData.Name, formData.Description, formData.ShortDescription, formData.Species, formData.CageNumber);
            
            _db.Experiment.Add(ExperimentToAdd);
            _db.SaveChanges();
         
            return RedirectToAction("Index");
        }
        //GET-Show
        public IActionResult Show(int id) //Wyswietla eksperyment
        {
            var experiment = _db.Experiment.Find(id);//pobieranie danych z bazy 
            if (experiment.Code == null)
            {
                experiment.Code = $"{experiment.Id}/{experiment.Species}/{System.DateTime.Today.Year}";
                _db.Experiment.Update(experiment);
                _db.SaveChanges();
            }
            
            if (_db.Experiment.Find(id)==null)//sprawdza czy w bazie jest podane id
                return NotFound();
         
                experiment.SurveysList = _db.Surveys.Where(s => s.ExperimentId == id).ToList();//dodanie pomiarów dla danego eksperymentu po id

                //pobierz cage > dla każdego survey pobierz każdy cage 
                experiment.SurveysList.ForEach(s => s.Cages = _db.Cage.Where(c => c.SurveyId == s.SurveyId).ToList()); 
           
                //pobierz img 
               // experiment.Files = _db.Files.Where(i => i.ExperimentId == id).ToList();
        
           
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
           
            var CageToDelete = new List<CageSurvey>();
            
            //tworzenie listy cage do usunięcia dla wszystkich pomiarów do usuniecia dla każdego survey dodaj każdy cage do listy usuń 
            SurveysToDelete.ToList().ForEach(s => s.Cages.ForEach(c => CageToDelete.Add(_db.Cage.Find(c.CageId))));
           

            _db.Surveys.RemoveRange(SurveysToDelete);
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
        public IActionResult Update(Experiment ToUpdate, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                _db.Experiment.Update(ToUpdate);
                 if (files.Count > 0)
            {
                foreach (var file in files)
                {
                       // string typePath = type ? "\\Formula\\" : "\\Resorces\\";
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\Files\\{ToUpdate.Id}" +  typePath );
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) Directory.CreateDirectory(basePath);
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var filePath = Path.Combine(basePath, file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                             file.CopyTo(stream);
                        }
                        var fileModel = new FileModel
                        {
                            ExperimentId = ToUpdate.Id,
                            Extension = extension,
                            Name = fileName,
                            FilePath = filePath
                        };
                        _db.Files.Add(fileModel);
                        _db.SaveChanges();
                    }
                }
            }
                return RedirectToAction("Index");
            }
            return View(ToUpdate);
        }
   
    }

     
}


