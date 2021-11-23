using FermaOnline.Models;
using System;
using System.Collections.Generic;

namespace FermaOnline.Facades
{
    public interface ISurveyRepository : IDisposable
    {
        IEnumerable<Survey> GetSurveys();
        Survey GetSurveyByID(int surveyId);
        List<Survey> GetSurveyByExperimentID(int experimentId);
        Survey GetLastSurvey(int id);
        bool ExistSurveyInExperiment(int experimentId);
        void InsertSurvey(Survey survey);
        void DeleteSurvey(int surveyId);
        void DeleteSurveys(List<Survey> surveys);
        void UpdateSurvey(Survey survey);
        void Save();
        List<Survey> GetSurveysByExperimentID(int id);
    }
}