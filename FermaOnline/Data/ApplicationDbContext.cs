using FermaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace FermaOnline.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<CageSurvey> Cage { get; set; }

        public DbSet<CageFirstIndividualBodyWeight> CFIBW { get; set; }
        
        public DbSet<Experiment> Experiment { get; set; }
        public DbSet<FileModel> Files { get; set; }
    }
}
