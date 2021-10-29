using FermaOnline.Data;
using FermaOnline.Models;
using FermaOnline.Models.DTOs;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FermaOnline.Facades;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace FermaOnline.Facades
{
    public class ExperimentFacade
    {
        private readonly ApplicationDbContext repository;//dostęp do bazy danych 
        public ExperimentFacade(ApplicationDbContext db)
        {
            repository = db;
        }

        public void Create(ExperimentDTO experimentDTO)
        {
            Experiment experimentToAdd = new(experimentDTO);
            if (experimentToAdd.Code == null)
            {
                experimentToAdd.Code = $"{experimentToAdd.Id}/{experimentToAdd.Species}/{System.DateTime.Today.Year}";
            }
            repository.Experiment.Add(experimentToAdd);
            repository.SaveChanges();
        }

        public Experiment Show(int id)
        {
            var experiment = repository.Experiment.Find(id);//pobieranie danych z bazy 
            if (experiment == null)
            {
                return experiment;
            }
            else
            {
                experiment.SurveysList = repository.Surveys.Where(s => s.ExperimentId == id).ToList();//dodanie pomiarów dla danego eksperymentu po id
                experiment.SurveysList.ForEach(s => s.Cages = repository.Cage.Where(c => c.SurveyId == s.SurveyId).ToList());
                experiment.Files = repository.Files.Where(i => i.ExperimentId == id).ToList();

                return experiment;
            }
        }
        public Experiment Delete(int? id)
        {
            Experiment experiment = null;
            if(id != null)
            {
               experiment = repository.Experiment.Find(id);
            }
            return experiment;
        }
        public bool DeletePost(int? id) //pomyśleć coś trochę 
        {
            if (id == null)
                return false;

            var experimetnToDelete = repository.Experiment.Find(id);
            if (experimetnToDelete == null)
                return false;

            var surveysToDelete = repository.Surveys.Where(s => s.ExperimentId == experimetnToDelete.Id);
            var cageToDelete = new List<CageSurvey>();
            var filesToDelete = new List<FileModel>();
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\Files\\{id}");

            surveysToDelete.ToList().ForEach(s => s.Cages.ForEach(c => cageToDelete.Add(repository.Cage.Find(c.CageId))));
            filesToDelete = repository.Files.Where(i => i.ExperimentId == id).ToList();

            if (Directory.Exists(basePath))
                Directory.Delete(basePath, true);

            repository.Surveys.RemoveRange(surveysToDelete);
            repository.Cage.RemoveRange(cageToDelete);
            repository.Experiment.Remove(experimetnToDelete);
            repository.Files.RemoveRange(filesToDelete);
            repository.SaveChanges();

            return true;
        }
        public Experiment Update(int? id)
        {
            Experiment experiment = null;
            if (id != null)
            {
                experiment = repository.Experiment.Find(id);
            }
            return experiment;
        }

        public void AddFile(
            List<IFormFile> files,
            int id,
            bool fileType)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\Files\\{id}");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var filePath = Path.Combine(basePath, file.FileName);
                var FileType = fileType ? "Materials" : "Formula";
                if (!File.Exists(filePath))
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
                    repository.Files.Add(fileModel);
                }
            }
        }
        public void Update(Experiment toUpdate,
            List<IFormFile> formula,
            List<IFormFile> materials,
            List<int> areChecked
            )
        {
            var update = repository.Experiment.Find(toUpdate.Id);
            string visible = "";

            for (int i = 0; i < 9; i++)
                if (areChecked.Contains(i))
                    visible += "1";
                else
                    visible += "0";

            update.Name = toUpdate.Name;
            update.Status = toUpdate.Status;
            update.Description = toUpdate.Description;
            update.ShortDescription = toUpdate.ShortDescription;
            update.VisibleProperties = visible;
            repository.Experiment.Update(update);

            if (materials.Count > 0)
                AddFile(materials, toUpdate.Id, true);

            if (formula.Count > 0)
                AddFile(formula, toUpdate.Id, false);

            repository.SaveChanges();
        }
    }
}
