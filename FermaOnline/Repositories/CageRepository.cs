using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermaOnline.Facades
{
    public class CageRepository : ICageRepository, IDisposable
    {
        private readonly ApplicationDbContext context;
        public CageRepository(DbContext context)
        {
            this.context = (ApplicationDbContext)context;
        }
        public void DeleteCage(int surveyId)
        {
            CageSurvey cage = context.Cage.Find(surveyId);
            context.Cage.Remove(cage);
        }

        public IEnumerable<CageSurvey> GetCage()
        {
            return context.Cage.ToList();
        }

        public CageSurvey GetCageByID(int surveyId)
        {
            return context.Cage.Find(surveyId);
        }

        public void InsertCage(CageSurvey cage)
        {
            context.Cage.Add(cage);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCage(CageSurvey survey)
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

        public void DeleteCages(List<CageSurvey> cages)
        {
            context.Cage.RemoveRange(cages);
        }

        public List<CageSurvey> GetCageBySurveyID(int surveyId)
        {
            return context.Cage.Where(c => c.SurveyId == surveyId).ToList();
        }
    }
}