using FermaOnline.Models;
using System;
using System.Collections.Generic;

namespace FermaOnline.Facades
{
    public interface ICageFirstIndividualBodyWeightRepository : IDisposable
    {
        IEnumerable<CageFirstIndividualBodyWeight> GetCFIBW();
        CageFirstIndividualBodyWeight GetCFIBWByID(int surveyId);
        List<float> GetCFIBWByExperimentID(int surveyId);
        List<CageFirstIndividualBodyWeight> GetCFIBWBySurveyID(int surveyId);
        void InsertCFIBW(CageFirstIndividualBodyWeight survey);
        void DeleteCFIBW(int surveyId);
        void DeleteCFIBW(List<CageFirstIndividualBodyWeight> cages);
        void UpdateCFIBW(CageFirstIndividualBodyWeight survey);
        void Save();
    }
}