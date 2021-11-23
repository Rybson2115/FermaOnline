using FermaOnline.Data;
using FermaOnline.Models;
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
        private readonly IExperimentRepository experimentRepository;
        private readonly ISurveyRepository surveyRepository;
        private readonly ICageRepository cageRepository;
        private readonly IFilesRepository filesRepository;
        public ExperimentFacade(ApplicationDbContext db)
        {
            this.experimentRepository = new ExperimentRepository(db);
            this.surveyRepository = new SurveyRepository(db);
            this.cageRepository = new CageRepository(db);
            this.filesRepository = new FilesRepository(db);
        }
        public IEnumerable<Experiment> Index()
        {
            return experimentRepository.GetExperiments();
        }
        public void Create(Experiment formData)
        {
            Experiment experimentToAdd = new(formData.Name, formData.Description, formData.ShortDescription, formData.Species, formData.CageNumber, formData.Author);
            if (experimentToAdd.Code == null)
            {
                experimentToAdd.Code = $"{experimentToAdd.Id}/{experimentToAdd.Species}/{System.DateTime.Today.Year}";
            }
            experimentRepository.InsertExperiment(experimentToAdd);
            experimentRepository.Save();
        }

        public Experiment Show(int id)
        {
            var experiment = experimentRepository.GetExperimentByID(id);//pobieranie danych z bazy 
            if (experiment == null)
            {
                return experiment;
            }
            else
            {
                experiment.SurveysList = surveyRepository.GetSurveysByExperimentID(id);//dodanie pomiarów dla danego eksperymentu po id
                experiment.SurveysList.ForEach(s => s.Cages = cageRepository.GetCageBySurveyID(s.SurveyId));
                experiment.Files = filesRepository.GetFilesByExperimentID(id);

                return experiment;
            }
        }
        public Experiment Delete(int? id)
        {
            Experiment experiment = null;
            if(id != null)
            {
               experiment = experimentRepository.GetExperimentByID((int)id);
            }
            return experiment;
        }
        public bool DeletePost(int? id) //pomyśleć coś trochę 
        {
            if (id == null)
                return false;

            var experimetnToDelete = experimentRepository.GetExperimentByID((int)id);
            if (experimetnToDelete == null)
                return false;

            var surveysToDelete = surveyRepository.GetSurveyByExperimentID((int)id);
            var cageToDelete = new List<CageSurvey>();
            var filesToDelete = new List<FileModel>();
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\Files\\{id}");

            surveysToDelete.ForEach(s => s.Cages.ForEach(c => cageToDelete.Add(cageRepository.GetCageByID(c.CageId))));
            filesToDelete = filesRepository.GetFilesByExperimentID((int)id);

            if (Directory.Exists(basePath))
                Directory.Delete(basePath, true);

            surveyRepository.DeleteSurveys(surveysToDelete);
            cageRepository.DeleteCages(cageToDelete);
            experimentRepository.DeleteExperiment((int)id);
            filesRepository.DeleteListFiles(filesToDelete);

            experimentRepository.Save(); //prawdopodnie bedzie trzeba save
            surveyRepository.Save();
            cageRepository.Save();
            filesRepository.Save();

            return true;
        }
        public Experiment Update(int? id)
        {
            Experiment experiment = null;
            if (id != null)
            {
                experiment = experimentRepository.GetExperimentByID((int)id);
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
                    filesRepository.InsertFiles(fileModel);
                }
            }
            filesRepository.Save();
        }
        public  void Update(Experiment toUpdate,
            List<IFormFile> formula,
            List<IFormFile> materials,
            List<int> areChecked
            )
        {
            var update = experimentRepository.GetExperimentByID(toUpdate.Id);
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
            experimentRepository.UpdateExperiment(update);

            if (materials.Count > 0)
                AddFile(materials, toUpdate.Id, true);

            if (formula.Count > 0)
                AddFile(formula, toUpdate.Id, false);

            experimentRepository.Save();//prawdopodnie na kazdym repo!
        }
    }
}
