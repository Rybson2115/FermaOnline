using FermaOnline.Data;
using FermaOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermaOnline.Facades
{
    public class FilesRepository : IFilesRepository, IDisposable
    {
        private readonly ApplicationDbContext context;
        public FilesRepository(DbContext context)
        {
            this.context = (ApplicationDbContext)context;
        }
        public void DeleteFiles(int fileId)
        {
            FileModel file = context.Files.Find(fileId);
            context.Files.Remove(file);
        }

        public IEnumerable<FileModel> GetFiles()
        {
            return context.Files.ToList();
        }

        public FileModel GetFilesByID(int fileId)
        {
            return context.Files.Find(fileId);
        }

        public void InsertFiles(FileModel file)
        {
            context.Files.Add(file);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateFiles(FileModel file)
        {
            context.Entry(file).State = EntityState.Modified;
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

        public void DeleteListFiles(List<FileModel> files)
        {
            context.Files.RemoveRange(files); ;
        }

        public List<FileModel> GetFilesByExperimentID(int experimentId)
        {
            return context.Files.Where(i => i.ExperimentId == experimentId).ToList();
        }
    }
}