using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermaOnline.Facades
{
    public class CageFirstIndividualBodyWeightRepository : ICageFirstIndividualBodyWeightRepository, IDisposable
    {
        private readonly ApplicationDbContext context;
        public CageFirstIndividualBodyWeightRepository(DbContext context)
        {
            this.context = (ApplicationDbContext)context;
        }
        public void DeleteCFIBW(int surveyId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCFIBW(List<CageFirstIndividualBodyWeightRepository> cages)
        {
            throw new NotImplementedException();
        }

        public void DeleteCFIBW(List<CageFirstIndividualBodyWeight> cages)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CageFirstIndividualBodyWeight> GetCFIBW()
        {
            throw new NotImplementedException();
        }

        public List<float> GetCFIBWByExperimentID(int surveyId)
        {
            return context.CFIBW.Where(f => f.ExperimentId == surveyId).Select(s => s.FirstIndividualBodyWeight).ToList();
        }

        public CageFirstIndividualBodyWeight GetCFIBWByID(int surveyId)
        {
            throw new NotImplementedException();
        }

        public List<CageFirstIndividualBodyWeight> GetCFIBWBySurveyID(int surveyId)
        {
            throw new NotImplementedException();
        }

        public void InsertCFIBW(CageFirstIndividualBodyWeight survey)
        {
            context.CFIBW.Add(survey);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateCFIBW(CageFirstIndividualBodyWeight survey)
        {
            throw new NotImplementedException();
        }
    }
}