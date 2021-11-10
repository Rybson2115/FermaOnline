using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
     
        public IActionResult UpdateTable(List<string> AreChecked) //lista experymentów
        {
            IEnumerable<Experiment> ExperimentsList = _db.Experiment;//pobieranie danych z bazy 
            List<Experiment> Result = new List<Experiment>();
            Expression    constant, StatusProp, SpeciesProp, StatusExpresion, SpeciesExpresion, expression;
            var parameterExpression = Expression.Parameter(typeof(Experiment), "Experiment");
            StatusProp = Expression.Property(parameterExpression, "Status");
            SpeciesProp = Expression.Property(parameterExpression, "Species");
            StatusExpresion = Expression.Equal(StatusProp, Expression.Constant(true));
            SpeciesExpresion = null;
            
            for (int i = 0; i < AreChecked.Count; i++)
            {
             
                switch (AreChecked[i])
                {
                    case "Active":
                        StatusExpresion = Expression.Equal(StatusProp, Expression.Constant(true));
                        break;
                    case "Inactive":
                        StatusExpresion = Expression.Equal(StatusProp, Expression.Constant(false));
                        break;
                    case "Pig":
                        SpeciesExpresion = Expression.Equal(SpeciesProp, Expression.Constant("Pig"));
                        break;
                    case "Poultry":
                        SpeciesExpresion = Expression.Equal(SpeciesProp, Expression.Constant("Poultry"));
                        break;
                    case "Ruminants":
                        SpeciesExpresion = Expression.Equal(SpeciesProp, Expression.Constant("Ruminants"));
                        break;
                }

      
            }
            if (AreChecked.Count != 1)
            {
                expression = Expression.And(StatusExpresion, SpeciesExpresion);
                var lambda = Expression.Lambda<Func<Experiment, bool>>(expression, parameterExpression);
                var compiledLambda = lambda.Compile();
                 Result = ExperimentsList.Where(compiledLambda).ToList();
            }

            return  View("Index", Result );
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
            ExperimentToAdd = new Experiment(formData.Name, formData.Description, formData.ShortDescription, formData.Species, formData.CageNumber,formData.Author);

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

            if (_db.Experiment.Find(id) == null)//sprawdza czy w bazie jest podane id
                return NotFound();

            experiment.SurveysList = _db.Surveys.Where(s => s.ExperimentId == id).ToList();//dodanie pomiarów dla danego eksperymentu po id

            //pobierz cage > dla każdego survey pobierz każdy cage 
            experiment.SurveysList.ForEach(s => s.Cages = _db.Cage.Where(c => c.SurveyId == s.SurveyId).ToList());

            //pobierz Files 
            experiment.Files = _db.Files.Where(i => i.ExperimentId == id).ToList();

            ViewBag.IsFirstSurvay = experiment.SurveysList.Count == 0 ? true : false;
            
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
            var FilesToDelete = new List<FileModel>();
            //tworzenie listy cage do usunięcia dla wszystkich pomiarów do usuniecia dla każdego survey dodaj każdy cage do listy usuń 
            SurveysToDelete.ToList().ForEach(s => s.Cages.ForEach(c => CageToDelete.Add(_db.Cage.Find(c.CageId))));
            FilesToDelete = _db.Files.Where(i => i.ExperimentId == id).ToList();

            var basePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\Files\\{id}");
            if (Directory.Exists(basePath))
                Directory.Delete(basePath, true);

            _db.Surveys.RemoveRange(SurveysToDelete);
            _db.Cage.RemoveRange(CageToDelete);
            _db.Experiment.Remove(ExperimetnToDelete);
            _db.Files.RemoveRange(FilesToDelete);
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
        public void AddFile(List<IFormFile> files,int id,bool fileType)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\Files\\{id}");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var filePath = Path.Combine(basePath, file.FileName);
                var FileType = fileType ? "Materials" : "Formula";
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var fileModel = new FileModel
                    {
                        ExperimentId = id,
                        FileType = FileType,
                        Name = file.FileName,
                        FilePath = filePath
                    };
                    _db.Files.Add(fileModel);
                }
            }
        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Experiment ToUpdate, List<IFormFile> Formula, List<IFormFile> Materials,  List<int> AreChecked)
        {

            if (ModelState.IsValid)
            {

                var Update = _db.Experiment.Find(ToUpdate.Id);
                string visible = "";
                
                for (int i = 0; i < AreChecked.Count; i++)
                    if (AreChecked.Contains(i))
                        visible += "1";
                    else
                        visible += "0";



                Update.Name = ToUpdate.Name;  
                Update.Status = ToUpdate.Status;
                Update.Description = ToUpdate.Description;
                Update.ShortDescription = ToUpdate.ShortDescription;
                Update.VisibleProperties = visible;
                _db.Experiment.Update(Update);


                if (Materials.Count > 0)
                    AddFile(Materials, ToUpdate.Id, true);

                if (Formula.Count > 0)
                    AddFile(Formula, ToUpdate.Id, false);


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ToUpdate);
        }

    }


}


