using FermaOnline.Models;
using System;
using System.Collections.Generic;

namespace FermaOnline.Facades
{
    public interface IExperimentRepository : IDisposable
    {
        IEnumerable<Experiment> GetExperiments();
        Experiment GetExperimentByID(int experimentId);
        void InsertExperiment(Experiment experment);
        void DeleteExperiment(int experimentId);
        void UpdateExperiment(Experiment experment);
        void Save();
    }
}