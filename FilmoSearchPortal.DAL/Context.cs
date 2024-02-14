using FilmoSearchPortal.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FilmoSearchPortal.DAL
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<FilmEntity> Films { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<ActorEntity> Actors { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            System.Diagnostics.Debug.WriteLine(_configuration);
        }
    }
}