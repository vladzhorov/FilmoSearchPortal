using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Services;
using Microsoft.Extensions.DependencyInjection;


namespace FilmoSearchPortal.BLL
{
    public static class BLLServiceExtension
    {
        public static void AddBLLDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IActorService, ActorService>();

        }
    }
}

