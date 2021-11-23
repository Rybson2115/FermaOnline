using FermaOnline.Data;
using FermaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermaOnline.Facades
{
    public class SurveyFacade
    {
        private readonly ApplicationDbContext _db; //dostęp do bazy danych
        private readonly ISurveyRepository surveyRepository;
        private readonly IExperimentRepository experimentRepository;
        private readonly ICageRepository cageRepository;
        private readonly ICageFirstIndividualBodyWeightRepository cageFirstIndividualBodyWeightRepository;

        public SurveyFacade(ApplicationDbContext db)
        {
            _db = db;
            this.surveyRepository = new SurveyRepository(db);
            this.experimentRepository = new ExperimentRepository(db);
            this.cageRepository = new CageRepository(db);
            this.cageFirstIndividualBodyWeightRepository = new CageFirstIndividualBodyWeightRepository(db);
        }
        public void Create(Survey survey)
        {
            Experiment experiment = experimentRepository.GetExperimentByID(survey.ExperimentId);

            if (surveyRepository.ExistSurveyInExperiment(survey.ExperimentId))
            {
                Survey lastSurvey = surveyRepository.GetLastSurvey(survey.ExperimentId);

                //pobranie cage dla lastSurvey 
                lastSurvey.Cages = cageRepository.GetCageBySurveyID(lastSurvey.SurveyId);
                //pobieranie CageFirstIndividualBodyWeight
                experiment.CageFirstIndividualBodyWeight = cageFirstIndividualBodyWeightRepository.GetCFIBWByExperimentID(experiment.Id);
                //dodanie survey do bazy 
                Survey newSurvey = new(survey, new Survey(lastSurvey), experiment.CageFirstIndividualBodyWeight, (int)experiment.CageNumber);
                surveyRepository.InsertSurvey(new Survey(newSurvey));       
            }
            else
            {
                survey.ExperimentId = survey.ExperimentId;
                var DataToAdd = new Survey(survey);
                experiment.Start = (DateTime)DataToAdd.SurveyDate;
                //dodanie CageFirstIndividualBodyWeight do bazy  
                DataToAdd.Cages.ForEach(c => cageFirstIndividualBodyWeightRepository.InsertCFIBW(new CageFirstIndividualBodyWeight(DataToAdd.ExperimentId, c.CageId, c.IndividualBodyWeight)));
                experiment.Status = true;
                surveyRepository.InsertSurvey(DataToAdd);//repository.Surveys.Add(DataToAdd);
                experimentRepository.UpdateExperiment(experiment); //repository.Experiment.Update(experiment);
            }
            surveyRepository.Save();        }
        public Survey Delete(int? id)
        {
            Survey experiment = null;
            if (id != null)
            {
                experiment = surveyRepository.GetSurveyByID((int)id);
            }
            return experiment;
        }

        public Survey DeletePost(int id)
        {
            var SurveysToDelete = surveyRepository.GetSurveyByID(id);

            if (surveyRepository.GetSurveyByID(id) == null)
                return SurveysToDelete;

            var CageToDelete = cageRepository.GetCageBySurveyID(id);

            cageRepository.DeleteCages(CageToDelete);
            surveyRepository.DeleteSurvey(id);

            cageRepository.Save();
            surveyRepository.Save();

            return SurveysToDelete;

        }
        public bool IsFirst(int id)
        {
            bool SurveyExistInThisExperiment = surveyRepository.ExistSurveyInExperiment(id);
            return SurveyExistInThisExperiment;
        }
        public int GetCageNumber(int experimentId)
        {
            return experimentRepository.GetExperimentByID(experimentId).CageNumber;
        }
        public Experiment FindById(int id)
        {
            return experimentRepository.GetExperimentByID(id);
        }
    }
}
