using FilmoSearchPortal.DAL.Interfaces;
using FilmoSearchPortal.DAL.Repostories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace FilmoSearchPortal.DAL
{
    public static class DalServiceExtension
    {
        public static void AddDALDependencies(this IServiceCollection services, IConfiguration configuration, bool useInMemoryDatabase)
        {
            services.Configure<DatabaseOptions>(options => configuration.GetSection(nameof(DatabaseOptions)).Bind(options));


            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(dbOptions.ConnectionString,
                    b => b.MigrationsAssembly("FilmoSearchPortal.DAL"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();

        }
    }
}
