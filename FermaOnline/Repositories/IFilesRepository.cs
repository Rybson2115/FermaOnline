using FermaOnline.Models;
using System;
using System.Collections.Generic;

namespace FermaOnline.Facades
{
    public interface IFilesRepository : IDisposable
    {
        IEnumerable<FileModel> GetFiles();
        FileModel GetFilesByID(int fileId);
        List<FileModel> GetFilesByExperimentID(int experimentId);
        void InsertFiles(FileModel file);
        void DeleteFiles(int fileId);
        void DeleteListFiles(List<FileModel> files);
        void UpdateFiles(FileModel file);
        void Save();
    }
}