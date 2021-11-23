using FermaOnline.Models;
using System;
using System.Collections.Generic;

namespace FermaOnline.Facades
{
    public interface ICageRepository : IDisposable
    {
        IEnumerable<CageSurvey> GetCage();
        CageSurvey GetCageByID(int surveyId);
        List<CageSurvey> GetCageBySurveyID(int surveyId);
        void InsertCage(CageSurvey survey);
        void DeleteCage(int surveyId);
        void DeleteCages(List<CageSurvey> cages);
        void UpdateCage(CageSurvey survey);
        void Save();
    }
}