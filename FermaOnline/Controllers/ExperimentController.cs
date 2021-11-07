using FermaOnline.Data;
using FermaOnline.Facades;
using FermaOnline.Models;
using FermaOnline.Models.DTOs;
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
        private readonly ExperimentFacade facade;
        //GET
        public IActionResult Index() //lista experymentów
        {
            return View(facade.Index());
        }
        //GET-Create
        public IActionResult Create()
        {
            return View();
        }
        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]//zabezpieczenie 
        public IActionResult Create(ExperimentDTO formData)
        {
            facade.Create(formData);
            return RedirectToAction("Index");
        }
        //GET-Show
        public IActionResult Show(int id) //Wyswietla eksperyment
        {
            var experiment = facade.Show(id);
            if (experiment == null)
                return NotFound();
            ViewBag.IsFirstSurvay = experiment.SurveysList.Count == 0 ? true : false;
            return View(experiment);
        }
        // GET Delete
        public IActionResult Delete(int? id)
        {
            var toDelete = facade.Delete(id);
            if (toDelete == null)
                return NotFound();
            return View(toDelete);
        }
        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            bool deleted = facade.DeletePost(id);
            if(deleted == true)
                return RedirectToAction("Index");
            return NotFound();
        }

        //GET-Update
        public IActionResult Update(int? id)
        {

            var toUpdate = facade.Update(id);
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
            facade.AddFile(files, id, fileType);
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
                facade.Update(toUpdate, formula, materials, areChecked);
                return RedirectToAction("Index");
            }
            return View(toUpdate);
        }
    }
}