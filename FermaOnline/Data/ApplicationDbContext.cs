using FermaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace FermaOnline.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Cage> Cage { get; set; }
  
    }
}
