using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermaOnline.Facades
{
    public class ExperimentRepository : IExperimentRepository, IDisposable
    {
        private readonly ApplicationDbContext context;
        public ExperimentRepository(DbContext context)
        {
            this.context = (ApplicationDbContext)context;
        }
        public void DeleteExperiment(int experimentId)
        {
            Experiment experiment = context.Experiment.Find(experimentId);
            context.Experiment.Remove(experiment);
        }
        public Experiment GetExperimentByID(int experimentId)
        {
            return context.Experiment.Find(experimentId);
        }
        public IEnumerable<Experiment> GetExperiments()
        {
            return context.Experiment; 
        }
        public void InsertExperiment(Experiment experment)
        {
            context.Experiment.Add(experment);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void UpdateExperiment(Experiment experment)
        {
            context.Entry(experment).State = EntityState.Modified;
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
    }
}