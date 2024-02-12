using AutoMapper;
using FilmoSearchPortal.API.Controllers;
using FilmoSearchPortal.API.ViewModels.Actor;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.UnitTest.TestData;
using Moq;

namespace FilmoSearchPortal.UnitTest.UnitTest
{
    public class ActorControllerTests
    {
        private readonly Mock<IActorService> _actorServiceMock;
        private readonly Mock<IMapper> _mapper;

        public ActorControllerTests()
        {
            _actorServiceMock = new Mock<IActorService>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfActorViewModels()
        {
            // Arrange
            var (actors, actorViewModels) = ActorControllerTestHelper.TestGetAll();
            var cancellationToken = new CancellationToken();
            _actorServiceMock.Setup(service => service.GetAllAsync(cancellationToken)).ReturnsAsync(actors);
            _mapper.Setup(mapper => mapper.Map<IEnumerable<ActorViewModel>>(actors)).Returns(actorViewModels);
            var controller = new ActorController(_mapper.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.GetAll(cancellationToken);

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<ActorViewModel>>(result);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetById_ReturnsActorViewModel()
        {
            // Arrange
            var (actorId, actor, actorViewModel) = ActorControllerTestHelper.TestGetById();
            var cancellationToken = new CancellationToken();
            _actorServiceMock.Setup(service => service.GetByIdAsync(actorId, cancellationToken)).ReturnsAsync(actor);
            _mapper.Setup(mapper => mapper.Map<ActorViewModel>(actor)).Returns(actorViewModel);
            var controller = new ActorController(_mapper.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.GetById(actorId, cancellationToken);

            // Assert
            var model = Assert.IsType<ActorViewModel>(result);
            Assert.Equal(actorId, model.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedActorViewModel()
        {
            // Arrange
            var (viewModel, actor) = ActorControllerTestHelper.TestCreate();
            var cancellationToken = new CancellationToken();
            _actorServiceMock.Setup(service => service.CreateAsync(It.IsAny<Actor>(), cancellationToken)).ReturnsAsync(actor);
            _mapper.Setup(mapper => mapper.Map<Actor>(viewModel)).Returns(new Actor { Id = actor.Id, FirstName = viewModel.FirstName, LastName = viewModel.LastName });
            _mapper.Setup(mapper => mapper.Map<ActorViewModel>(actor)).Returns(new ActorViewModel { Id = actor.Id, FirstName = actor.FirstName, LastName = actor.LastName });
            var controller = new ActorController(_mapper.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.Create(viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<ActorViewModel>(result);
            Assert.Equal(actor.Id, model.Id);
            Assert.Equal(viewModel.FirstName, model.FirstName);
            Assert.Equal(viewModel.LastName, model.LastName);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedActorViewModel()
        {
            // Arrange
            var (actorId, viewModel, updatedActor) = ActorControllerTestHelper.TestUpdate();
            var cancellationToken = new CancellationToken();
            _actorServiceMock.Setup(service => service.UpdateAsync(It.IsAny<Actor>(), cancellationToken)).ReturnsAsync(updatedActor);
            _mapper.Setup(mapper => mapper.Map<Actor>(viewModel)).Returns(new Actor { Id = actorId, FirstName = viewModel.FirstName, LastName = viewModel.LastName });
            _mapper.Setup(mapper => mapper.Map<ActorViewModel>(updatedActor)).Returns(new ActorViewModel { Id = actorId, FirstName = updatedActor.FirstName, LastName = updatedActor.LastName });
            var controller = new ActorController(_mapper.Object, _actorServiceMock.Object);

            // Act
            var result = await controller.Update(actorId, viewModel, cancellationToken);

            // Assert
            var model = Assert.IsType<ActorViewModel>(result);
            Assert.Equal(actorId, model.Id);
            Assert.Equal(viewModel.FirstName, model.FirstName);
            Assert.Equal(viewModel.LastName, model.LastName);
        }
    }
}

