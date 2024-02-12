using FilmoSearchPortal.API.ViewModels.Film;
using FilmoSearchPortal.BLL.Models;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class FilmControllerTestHelper
    {
        public static (List<Film> films, List<FilmViewModel> filmViewModels) TestGetAll()
        {
            var filmId1 = Guid.NewGuid();
            var filmId2 = Guid.NewGuid();

            var films = new List<Film>
            {
                new Film { Id = filmId1, Title = "Film 1" },
                new Film { Id = filmId2, Title = "Film 2" }
            };

            var filmViewModels = new List<FilmViewModel>
            {
                new FilmViewModel { Id = filmId1, Title = "Film 1" },
                new FilmViewModel { Id = filmId2, Title = "Film 2" }
            };

            return (films, filmViewModels);
        }

        public static (Guid filmId, Film film, FilmViewModel filmViewModel) TestGetById()
        {
            var filmId = Guid.NewGuid();
            var film = new Film { Id = filmId, Title = "Test Film" };
            var filmViewModel = new FilmViewModel { Id = filmId, Title = "Test Film" };
            return (filmId, film, filmViewModel);
        }

        public static (CreateFilmViewModel viewModel, Film film) TestCreate()
        {
            var filmId = Guid.NewGuid();
            var viewModel = new CreateFilmViewModel { Title = "Test Film" };
            var film = new Film { Id = filmId, Title = viewModel.Title };
            return (viewModel, film);
        }

        public static (Guid filmId, UpdateFilmViewModel viewModel, Film film) TestUpdate()
        {
            var filmId = Guid.NewGuid();
            var viewModel = new UpdateFilmViewModel { Title = "Updated Film" };
            var film = new Film { Id = filmId, Title = viewModel.Title };
            return (filmId, viewModel, film);
        }

        public static Guid TestDelete()
        {
            return Guid.NewGuid();
        }

        public static (Guid filmId, ICollection<Guid> actorIds, Film film, IEnumerable<Actor> actors, FilmViewModel updatedFilmViewModel) TestAddActorsToFilm()
        {
            var filmId = Guid.NewGuid();
            var actorIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var film = new Film { Id = filmId, Title = "Film 1" };
            var actors = new List<Actor>
            {
                new Actor { Id = actorIds[0], FirstName = "Actor 1",LastName = "One" },
                new Actor { Id = actorIds[1], FirstName = "Actor 2", LastName = "Two" }
            };
            var updatedFilmViewModel = new FilmViewModel { Id = filmId, Title = "Film" };
            return (filmId, actorIds, film, actors, updatedFilmViewModel);
        }
    }
}
