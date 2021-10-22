using FermaOnline.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FermaOnline.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _db;//dostęp do bazy danych  /ja bym to jakoś repo nazwał 
        public FileController(ApplicationDbContext db)
        {
            _db = db;
        }
     

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var ToDelete = _db.Files.Find(id);

            if (ToDelete == null)
                return NotFound();

            return View(ToDelete);

        }

        // POST Delete

        //[ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var FileToDelete = _db.Files.Find(id);

            if (FileToDelete == null)
                return NotFound();

           
            if (System.IO.File.Exists(FileToDelete.FilePath))
                System.IO.File.Delete(FileToDelete.FilePath);
           
            _db.Files.Remove(FileToDelete);
            _db.SaveChanges();

            return RedirectToAction("Show", "Experiment", new { id = FileToDelete.ExperimentId });
        }
    }
}
