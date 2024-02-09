using FilmoSearchPortal.API.Controllers;
using FilmoSearchPortal.API.Mapping;
using FilmoSearchPortal.API.Middleware;
using FilmoSearchPortal.API.Validators.Actor;
using FilmoSearchPortal.API.Validators.Film;
using FilmoSearchPortal.API.Validators.Review;
using FilmoSearchPortal.API.Validators.User;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.DAL;
using FilmoSearchPortal.DAL.Interfaces;
using FilmoSearchPortal.DAL.Repostories;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<UserViewModelValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreateReviewViewModelValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreateFilmViewModelValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreateActorViewModelValidator>();

    });

//AddJsonOptions(op =>
//{
//    op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

//});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = " API ", Version = "v1" });
});

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();



builder.Services.AddDALDependencies(configuration/*, useInMemoryDatabase: true*/);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();

builder.Services.AddScoped<ReviewController>();
builder.Services.AddScoped<FilmController>();
builder.Services.AddScoped<ActorController>();

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
