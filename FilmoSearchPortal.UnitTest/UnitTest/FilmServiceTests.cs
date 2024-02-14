using AutoMapper;
using FilmoSearchPortal.API.Mapping;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using FilmoSearchPortal.UnitTest.TestData;
using Moq;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class FilmServiceTests
    {
        private readonly Mock<IFilmRepository> _filmRepositoryMock;
        private readonly IMapper _mapper;
        private readonly FilmService _filmService;

        public FilmServiceTests()
        {
            _filmRepositoryMock = new Mock<IFilmRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _filmService = new FilmService(_filmRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllFilms_ReturnsListOfFilmModels()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string title = "";
            EnumsEntity.Genre genre = EnumsEntity.Genre.Animation;

            var (films, filmModels) = FilmsTestData.TestGetAllFilms();
            _filmRepositoryMock.Setup(x => x.GetAllAsync(pageNumber, pageSize, title, genre, It.IsAny<CancellationToken>())).ReturnsAsync(films);

            // Act
            var result = await _filmService.GetAllAsync(pageNumber, pageSize, title, genre, CancellationToken.None);

            // Assert
            Assert.Equal(filmModels.Count, result.Count());
            Assert.Equal(filmModels[0].Id, result.ElementAt(0).Id);
            Assert.Equal(filmModels[1].Title, result.ElementAt(1).Title);

        }

        [Fact]
        public async Task GetFilmById_ExistingFilmId_ReturnsFilmModel()
        {
            // Arrange
            var (filmId, film, filmModel) = FilmsTestData.CreateFilm();
            _filmRepositoryMock.Setup(x => x.GetByIdAsync(filmId, CancellationToken.None)).ReturnsAsync(film);

            // Act
            var result = await _filmService.GetByIdAsync(filmId, cancellationToken: CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(filmModel.Id, result.Id);
            Assert.Equal(filmModel.Title, result.Title);
        }


        [Fact]
        public async Task CreateFilm_ValidFilmModel_CallsAddAsyncWithCorrectFilmEntity()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var newFilmModel = new Film { Title = "New Film", Genre = EnumsEntity.Genre.Animation };
            var newFilmEntity = new FilmEntity { Id = Guid.NewGuid(), Title = "New Film", Genre = EnumsEntity.Genre.Animation };
            _filmRepositoryMock.Setup(x => x.AddAsync(It.IsNotNull<FilmEntity>(), cancellationToken)).ReturnsAsync(newFilmEntity);

            // Act
            await _filmService.CreateAsync(newFilmModel, cancellationToken);

            // Assert
            _filmRepositoryMock.Verify(x => x.AddAsync(It.Is<FilmEntity>(filmEntity =>
                filmEntity.Title == newFilmModel.Title &&
                filmEntity.Genre == newFilmModel.Genre), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task UpdateFilm_ExistingFilmId_ValidFilmModel_CallsUpdateAsyncWithCorrectFilmEntity()
        {
            // Arrange
            var filmId = Guid.NewGuid();
            var existingFilmEntity = new FilmEntity { Id = filmId, Title = "Existing Film", Genre = EnumsEntity.Genre.Eastern };
            var updatedFilmModel = new Film { Id = existingFilmEntity.Id, Title = "Updated Film", Genre = existingFilmEntity.Genre };

            _filmRepositoryMock.Setup(x => x.GetByIdAsync(filmId, CancellationToken.None)).ReturnsAsync(existingFilmEntity);
            _filmRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<FilmEntity>(), CancellationToken.None)).ReturnsAsync(existingFilmEntity);

            // Act
            await _filmService.UpdateAsync(updatedFilmModel, CancellationToken.None);

            // Assert
            _filmRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<FilmEntity>(), CancellationToken.None), Times.Once);
        }
        [Fact]
        public async Task UpdateFilm_NonExistingFilmId_ReturnsNull()
        {
            var filmId = Guid.NewGuid();
            _filmRepositoryMock.Setup(x => x.GetByIdAsync(filmId, CancellationToken.None)).ReturnsAsync((FilmEntity)null);

            // Act
            var result = await _filmService.UpdateAsync(new Film { Id = filmId }, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteFilm_ExistingFilmId_CallsDeleteAsyncWithCorrectFilmEntity()
        {
            // Arrange
            var filmId = Guid.NewGuid();
            var existingFilm = new FilmEntity { Id = filmId, Title = "Film Title" };
            _filmRepositoryMock.Setup(x => x.GetByIdAsync(filmId, CancellationToken.None)).ReturnsAsync(existingFilm);
            _filmRepositoryMock.Setup(x => x.DeleteAsync(existingFilm, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            await _filmService.DeleteAsync(filmId, CancellationToken.None);

            // Assert
            _filmRepositoryMock.Verify(x => x.DeleteAsync(existingFilm, CancellationToken.None), Times.Once);
        }




    }
}
