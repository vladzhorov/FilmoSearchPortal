using FilmoSearchPortal.API.Mapping;
using FilmoSearchPortal.API.Middleware;
using FilmoSearchPortal.API.Validators.Actor;
using FilmoSearchPortal.API.Validators.Film;
using FilmoSearchPortal.API.Validators.Review;
using FilmoSearchPortal.API.Validators.User;
using FilmoSearchPortal.BLL;
using FilmoSearchPortal.DAL;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers()
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateUserViewModelValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<CreateReviewViewModelValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<CreateFilmViewModelValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<CreateActorViewModelValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<UpdateUserViewModelValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<UpdateReviewViewModelValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<UpdateFilmViewModelValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<UpdateActorViewModelValidator>();

            })
             .AddJsonOptions(op =>
        {
            op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = " API ", Version = "v1" });
        });

        var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        builder.Services.AddDALDependencies(configuration);

        builder.Services.AddBLLDependencies();

        builder.Services.AddAutoMapper(typeof(Program), typeof(ViewModelMappingProfile));
        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " API ");
            });
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
        //VULTURES
    }
}