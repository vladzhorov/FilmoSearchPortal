using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class FilmsTestData
    {
        public static (List<FilmEntity>, List<Film>) TestGetAllFilms()
        {
            var filmEntity1 = new FilmEntity { Id = Guid.NewGuid(), Title = "Film 1", Genre = EnumsEntity.Genre.Animation };
            var filmEntity2 = new FilmEntity { Id = Guid.NewGuid(), Title = "Film 2", Genre = EnumsEntity.Genre.Comedy };

            var filmModel1 = new Film { Id = filmEntity1.Id, Title = filmEntity1.Title, Genre = filmEntity1.Genre };
            var filmModel2 = new Film { Id = filmEntity2.Id, Title = filmEntity2.Title, Genre = filmEntity2.Genre };

            var filmsEntity = new List<FilmEntity> { filmEntity1, filmEntity2 };
            var filmModels = new List<Film> { filmModel1, filmModel2 };

            return (filmsEntity, filmModels);
        }

        public static (Guid, FilmEntity, Film) CreateFilm()
        {
            var filmId = Guid.NewGuid();
            var filmEntity = new FilmEntity { Id = filmId, Title = "Film Title", Genre = EnumsEntity.Genre.Adventure };
            var filmModel = new Film { Id = filmEntity.Id, Title = filmEntity.Title, Genre = filmEntity.Genre };
            return (filmId, filmEntity, filmModel);
        }
    }
}
