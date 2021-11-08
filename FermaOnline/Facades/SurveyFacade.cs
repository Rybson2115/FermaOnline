using FermaOnline.Data;
using FermaOnline.Models;
using System;
using System.Linq;

namespace FermaOnline.Facades
{
    public class SurveyFacade
    {
        private readonly ApplicationDbContext repository;//dostęp do bazy danych 
        public SurveyFacade(ApplicationDbContext db)
        {
            repository = db;
        }

        public int Create(SurveyDTO surveyDTO)
        {
            int id = surveyDTO.ExperimentId;
            Experiment experiment = repository.Experiment.Find(id);

            if (repository.Surveys.Any(s => s.ExperimentId == id))
            {
                Survey lastSurvey = repository.Surveys
                                .Where(s => s.ExperimentId == id)
                                .OrderByDescending(t => t.SurveyDate)
                                .FirstOrDefault();

                //pobranie cage dla lastSurvey 
                lastSurvey.Cages = repository.Cage.Where(c => c.SurveyId == lastSurvey.SurveyId).ToList();
                //pobieranie CageFirstIndividualBodyWeight
                experiment.CageFirstIndividualBodyWeight = repository.CFIBW.Where(f => f.ExperimentId == experiment.Id).Select(s => s.FirstIndividualBodyWeight).ToList();
                //dodanie survey do bazy 
                SurveyDTO newSurveyDTO = new SurveyDTO(surveyDTO, new SurveyDTO(lastSurvey), experiment.CageFirstIndividualBodyWeight, (int)experiment.CageNumber);
                repository.Surveys.Add(new Survey(newSurveyDTO));       
            }
            else
            {
                surveyDTO.ExperimentId = id;
                var DataToAdd = new Survey(surveyDTO);
                experiment.Start = (DateTime)DataToAdd.SurveyDate;
                //dodanie CageFirstIndividualBodyWeight do bazy  
                DataToAdd.Cages.ForEach(c => repository.CFIBW.Add(new CageFirstIndividualBodyWeight(DataToAdd.ExperimentId, c.CageId, c.IndividualBodyWeight)));
                experiment.Status = true;
                repository.Surveys.Add(DataToAdd);
                repository.Experiment.Update(experiment);
            }
            int surveyId = repository.SaveChanges(); //potencjalnie gnój
            return surveyId;
        }
    }
}
