using FilmoSearchPortal.API.ViewModels.Film;
using FilmoSearchPortal.DAL;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.IntegrationTest.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

public class FilmControllerTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;
    private readonly Guid _filmId = Guid.NewGuid();
    private readonly Guid _actorId1 = Guid.NewGuid();
    public FilmControllerTest(TestingWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
        AddTestData();

    }


    public void AddTestData()
    {
        // Arrange
        var filmsToAdd = new List<FilmEntity>
    {
        new FilmEntity { Id = _filmId, Title = "Film 1", Genre = Genre.Comedy },
        new FilmEntity { Id = Guid.NewGuid(), Title = "Film 2", Genre = Genre.Drama }

    };
        var actorsToAdd = new List<ActorEntity>
            {
                new ActorEntity { Id = _actorId1, FirstName = "Actor 1",LastName = "Actor 1" },
                new ActorEntity { Id =Guid.NewGuid(), FirstName = "Actor 2" , LastName = "Actor 2"}
            };



        using (var scope = _factory.Services.CreateScope())
        {

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();


            dbContext.Films.AddRange(filmsToAdd);
            dbContext.Actors.AddRange(actorsToAdd);
            dbContext.SaveChanges();
        }
    }
    [Fact]
    public async Task GetAll_ReturnsListOfFilmViewModels()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;


        using (var scope = _factory.Services.CreateScope())
        {

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Act
            var response = await _client.GetAsync($"/api/films?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var filmViewModels = await response.Content.ReadFromJsonAsync<IEnumerable<FilmViewModel>>();

            // Assert
            Assert.NotNull(filmViewModels);
            Assert.NotEmpty(filmViewModels);
        }
    }

    [Fact]
    public async Task GetById_ReturnsFilmViewModel()
    {
        // Arrange
        var filmId = _filmId;

        // Act
        var response = await _client.GetAsync($"/api/films/{filmId}");
        response.EnsureSuccessStatusCode();

        var filmViewModel = await response.Content.ReadFromJsonAsync<FilmViewModel>();

        // Assert
        Assert.NotNull(filmViewModel);
    }


    [Fact]
    public async Task Create_ReturnsCreatedFilmViewModel()
    {
        // Arrange
        var createViewModel = new CreateFilmViewModel
        {
            Title = "New Film",
            Genre = Genre.Western,
            ReleaseDate = DateTime.Now,

        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/films", createViewModel);
        response.EnsureSuccessStatusCode();

        var createdFilmViewModel = await response.Content.ReadFromJsonAsync<FilmViewModel>();

        // Assert
        Assert.NotNull(createdFilmViewModel);
        Assert.Equal(createViewModel.Title, createdFilmViewModel.Title);
        Assert.Equal(createViewModel.Genre, createdFilmViewModel.Genre);
        Assert.Equal(createViewModel.ReleaseDate, createdFilmViewModel.ReleaseDate);
    }


    [Fact]
    public async Task Update_ReturnsUpdatedFilmViewModel()
    {
        // Arrange
        var filmId = _filmId;
        var updateViewModel = new UpdateFilmViewModel
        {
            Title = "Updated Film Title",
            Genre = Genre.Fantasy,
            ReleaseDate = DateTime.Now.AddDays(1)
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/films/{filmId}", updateViewModel);
        response.EnsureSuccessStatusCode();

        var updatedFilmViewModel = await response.Content.ReadFromJsonAsync<FilmViewModel>();

        // Assert
        Assert.NotNull(updatedFilmViewModel);
        Assert.Equal(updateViewModel.Title, updatedFilmViewModel.Title);
        Assert.Equal(updateViewModel.Genre, updatedFilmViewModel.Genre);
        Assert.Equal(updateViewModel.ReleaseDate, updatedFilmViewModel.ReleaseDate);
    }


    [Fact]
    public async Task AddActorsToFilm_ReturnsUpdatedFilmViewModel()
    {
        // Arrange
        var filmId = _filmId;
        var actorIds = new List<Guid> { _actorId1 };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/films/{filmId}/actors", actorIds);
        response.EnsureSuccessStatusCode();

        var updatedFilmViewModel = await response.Content.ReadFromJsonAsync<FilmViewModel>();

        // Assert
        Assert.NotNull(updatedFilmViewModel);
        Assert.Equal(filmId, updatedFilmViewModel.Id);
        Assert.Equal(actorIds.Count, updatedFilmViewModel.Actors.Count());
    }


    [Fact]
    public async Task Delete_RemovesFilmFromDatabase()
    {
        // Arrange
        var filmId = _filmId;

        // Act
        var response = await _client.DeleteAsync($"/api/films/{filmId}");
        response.EnsureSuccessStatusCode();

        // Assert
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var deletedFilm = await dbContext.Films.FindAsync(filmId);
            Assert.Null(deletedFilm);
        }
    }

}
