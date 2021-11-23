using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermaOnline.Facades
{
    public class SurveyRepository : ISurveyRepository, IDisposable
    {
        private readonly ApplicationDbContext context;
        public SurveyRepository(DbContext context)
        {
            this.context = (ApplicationDbContext)context;
        }
        public void DeleteSurvey(int surveyId)
        {
            Survey survey = context.Surveys.Find(surveyId);
            context.Surveys.Remove(survey);
        }
        public IEnumerable<Survey> GetSurveys()
        {
            return context.Surveys.ToList();
        }
        public Survey GetSurveyByID(int surveyId)
        {
            return context.Surveys.Find(surveyId);
        }
        public List<Survey> GetSurveysByExperimentID(int experimentId)
        {
            return context.Surveys.Where(s => s.ExperimentId == experimentId).ToList();
        }
        public void InsertSurvey(Survey survey)
        {
            context.Surveys.Add(survey);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void UpdateSurvey(Survey survey)
        {
            context.Entry(survey).State = EntityState.Modified;
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void DeleteSurveys(List<Survey> surveys)
        {
            context.Surveys.RemoveRange(surveys);
        }

        public List<Survey> GetSurveyByExperimentID(int experimentId)
        {
            return context.Surveys.Where(s => s.ExperimentId == experimentId).ToList();
        }

        public bool ExistSurveyInExperiment(int experimentId)
        {
            return context.Surveys.Any(s => s.ExperimentId == experimentId);
        }

        public Survey GetLastSurvey(int id)
        {
            return context.Surveys
                .Where(s => s.ExperimentId == id)
                .OrderByDescending(t => t.SurveyDate)
                .FirstOrDefault();
        }
    }
}