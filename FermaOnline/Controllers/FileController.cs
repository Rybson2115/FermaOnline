using FermaOnline.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FermaOnline.Models;

namespace FermaOnline.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _db;//dostęp do bazy danych 

        public FileController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task UploadToFileSystem(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\UserFiles\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new FileModel
                    {
                       // ExperimentId =experimentId,
                        Extension = extension,
                        Name = fileName,
                        FilePath = filePath
                    };
                    _db.Files.Add(fileModel);
                    _db.SaveChanges();
                }
            }
            TempData["Message"] = "File successfully uploaded to File System.";
        }
        //private async Task<FileUploadViewModel> LoadAllFiles()
        //{
        //    var viewModel = new FileUploadViewModel();
        //    viewModel.FilesOnDatabase = await context.FilesOnDatabase.ToListAsync();
        //    viewModel.FilesOnFileSystem = await context.FilesOnFileSystem.ToListAsync();
        //    return viewModel;
        //}

        //public  Task<IActionResult> DownloadFileFromDatabase(int id)
        //{
        //    var file =  _db.Files.Where(x => x.Id == id);
        //    if (file == null) return null;
        //    return  (file);
        //}
    }
}
