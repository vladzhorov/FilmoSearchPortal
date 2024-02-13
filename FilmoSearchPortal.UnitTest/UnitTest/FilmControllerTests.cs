using AutoMapper;
using FilmoSearchPortal.API.Controllers;
using FilmoSearchPortal.API.ViewModels.Film;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.UnitTest.TestData;
using Moq;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class FilmControllerTests
    {
        private readonly Mock<IFilmService> _filmServiceMock;
        private readonly Mock<IActorService> _actorServiceMock;
        private readonly Mock<IMapper> _mapper;

        public FilmControllerTests()
        {
            _filmServiceMock = new Mock<IFilmService>();
            _actorServiceMock = new Mock<IActorService>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfFilmViewModels()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var title = "title";
            var genre = Genre.Drama;
            var (films, filmViewModels) = FilmControllerTestHelper.TestGetAll();
            var cancellationToken = new CancellationToken();
            _filmServiceMock.Setup(service => service.GetAllAsync(pageNumber, pageSize, title, genre, cancellationToken)).ReturnsAsync(films);
            _mapper.Setup(mapper => mapper.Map<IEnumerable<FilmViewModel>>(films)).Returns(filmViewModels);
            var controller = new FilmController(_mapper.Object, _filmServiceMock.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.GetAll(pageNumber, pageSize, title, genre, cancellationToken);

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<FilmViewModel>>(result);
            Assert.Equal(filmViewModels.Count, result.Count());
            Assert.Equal(filmViewModels[0].Id, result.ElementAt(0).Id);
            Assert.Equal(2, model.Count());

        }

        [Fact]
        public async Task GetById_ReturnsFilmViewModel()
        {
            // Arrange
            var (filmId, film, filmViewModel) = FilmControllerTestHelper.TestGetById();
            var cancellationToken = new CancellationToken();
            _filmServiceMock.Setup(service => service.GetByIdAsync(filmId, cancellationToken)).ReturnsAsync(film);
            _mapper.Setup(mapper => mapper.Map<FilmViewModel>(film)).Returns(new FilmViewModel { Id = filmId, Title = film.Title });
            var controller = new FilmController(_mapper.Object, _filmServiceMock.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.GetById(filmId, cancellationToken);

            // Assert
            var model = Assert.IsType<FilmViewModel>(result);
            Assert.Equal(filmId, model.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedFilmViewModel()
        {
            // Arrange
            var (viewModel, film) = FilmControllerTestHelper.TestCreate();
            var cancellationToken = new CancellationToken();
            _filmServiceMock.Setup(service => service.CreateAsync(It.IsAny<Film>(), cancellationToken)).ReturnsAsync(film);
            _mapper.Setup(mapper => mapper.Map<Film>(viewModel)).Returns(new Film { Id = film.Id, Title = viewModel.Title });
            _mapper.Setup(mapper => mapper.Map<FilmViewModel>(film)).Returns(new FilmViewModel { Id = film.Id, Title = film.Title });
            var controller = new FilmController(_mapper.Object, _filmServiceMock.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.Create(viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<FilmViewModel>(result);
            Assert.Equal(film.Id, model.Id);
            Assert.Equal(viewModel.Title, model.Title);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedFilmViewModel()
        {
            // Arrange
            var (filmId, viewModel, updatedFilm) = FilmControllerTestHelper.TestUpdate();
            var cancellationToken = new CancellationToken();
            _filmServiceMock.Setup(service => service.UpdateAsync(It.IsAny<Film>(), cancellationToken)).ReturnsAsync(updatedFilm);
            _mapper.Setup(mapper => mapper.Map<Film>(viewModel)).Returns(new Film { Id = filmId, Title = viewModel.Title });
            _mapper.Setup(mapper => mapper.Map<FilmViewModel>(updatedFilm)).Returns(new FilmViewModel { Id = filmId, Title = updatedFilm.Title });
            var controller = new FilmController(_mapper.Object, _filmServiceMock.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.Update(filmId, viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<FilmViewModel>(result);
            Assert.Equal(filmId, model.Id);
            Assert.Equal(viewModel.Title, model.Title);
        }
    }
}
