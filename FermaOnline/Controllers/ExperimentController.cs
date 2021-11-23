using FermaOnline.Data;
using FermaOnline.Facades;
using FermaOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly ApplicationDbContext _db;
        private readonly ExperimentFacade experimentFacade;
       public ExperimentController(ApplicationDbContext db)
        {
            _db = db;
            this.experimentFacade = new ExperimentFacade(db);
        }
        //GET
        public IActionResult Index() //lista experymentów
        {
            return View(experimentFacade.Index());
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
            experimentFacade.Create(formData);
            return RedirectToAction("Index");
        }
        //GET-Show
        public IActionResult Show(int id) //Wyswietla eksperyment
        {
            var experiment = experimentFacade.Show(id);
            if (experiment == null)
                return NotFound();
            ViewBag.IsFirstSurvay = experiment.SurveysList.Count == 0 ? true : false;
            return View(experiment);
        }
        // GET Delete
        public IActionResult Delete(int? id)
        {
            var toDelete = experimentFacade.Delete(id);
            if (toDelete == null)
                return NotFound();
            return View(toDelete);
        }
        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            bool deleted = experimentFacade.DeletePost(id);
            if(deleted == true)
                return RedirectToAction("Index");
            return NotFound();
        }

        //GET-Update
        public IActionResult Update(int? id)
        {

            var toUpdate = experimentFacade.Update(id);
            if (toUpdate == null)
                return NotFound();
            return View(toUpdate);
        }
        public void AddFile(
            List<IFormFile> files,
            int id,
            bool fileType
            )
        {
            experimentFacade.AddFile(files, id, fileType);
        }
        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(
            Experiment toUpdate, 
            List<IFormFile> formula, 
            List<IFormFile> materials, 
            List<int> areChecked
            )
        {
            if (ModelState.IsValid)
            {
                experimentFacade.Update(toUpdate, formula, materials, areChecked);
                return RedirectToAction("Index");
            }
            return View(toUpdate);
        }
    }
}