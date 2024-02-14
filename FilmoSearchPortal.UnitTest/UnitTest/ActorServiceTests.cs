using AutoMapper;
using FilmoSearchPortal.API.Mapping;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using FilmoSearchPortal.UnitTest.TestData;
using Moq;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.BLL.UnitTest.UnitTest
{
    public class ActorServiceTests
    {
        private readonly Mock<IActorRepository> _actorRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ActorService _actorService;

        public ActorServiceTests()
        {
            _actorRepositoryMock = new Mock<IActorRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _actorService = new ActorService(_actorRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllActors_ReturnsListOfActorModels()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string firstName = "";
            string lastName = "";
            ActorStatus actorStatus = ActorStatus.Active;

            var (actorsEntities, actorModels) = ActorTestData.TestGetAllActors();
            _actorRepositoryMock.Setup(x => x.GetAllAsync(pageNumber, pageSize, firstName, lastName, actorStatus, It.IsAny<CancellationToken>())).ReturnsAsync(actorsEntities);

            // Act
            var result = await _actorService.GetAllAsync(pageNumber, pageSize, firstName, lastName, actorStatus, CancellationToken.None);

            // Assert
            Assert.Equal(actorModels.Count, result.Count());
            Assert.Equal(actorModels[0].Id, result.ElementAt(0).Id);
            Assert.Equal(actorModels[1].FirstName, result.ElementAt(1).FirstName);

        }

        [Fact]
        public async Task GetActorById_ExistingActorId_ReturnsActorModel()
        {
            // Arrange
            var (actorId, actorEntity, actorModel) = ActorTestData.CreateActor();
            _actorRepositoryMock.Setup(x => x.GetByIdAsync(actorId, CancellationToken.None)).ReturnsAsync(actorEntity);

            // Act
            var result = await _actorService.GetByIdAsync(actorId, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(actorModel.Id, result.Id);
            Assert.Equal(actorModel.FirstName, result.FirstName);

        }

        [Fact]
        public async Task CreateActor_ValidActorModel_CallsAddAsyncWithCorrectActorEntity()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var newActorModel = new Actor { FirstName = "New", LastName = "Actor", ActorStatus = ActorStatus.Active };
            var newActorEntity = new ActorEntity { Id = Guid.NewGuid(), FirstName = "New", LastName = "Actor", ActorStatus = ActorStatus.Active };
            _actorRepositoryMock.Setup(x => x.AddAsync(It.IsNotNull<ActorEntity>(), cancellationToken)).ReturnsAsync(newActorEntity);

            // Act
            await _actorService.CreateAsync(newActorModel, cancellationToken);

            // Assert
            _actorRepositoryMock.Verify(x => x.AddAsync(It.Is<ActorEntity>(actorEntity =>
                actorEntity.FirstName == newActorModel.FirstName &&
                actorEntity.LastName == newActorModel.LastName &&
                actorEntity.ActorStatus == newActorModel.ActorStatus), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task UpdateActor_ExistingActorId_ValidActorModel_CallsUpdateAsyncWithCorrectActorEntity()
        {
            // Arrange
            var actorId = Guid.NewGuid();
            var existingActorEntity = new ActorEntity { Id = actorId, FirstName = "Existing", LastName = "Actor", ActorStatus = ActorStatus.Active };
            var updatedActorModel = new Actor { Id = existingActorEntity.Id, FirstName = "Updated", LastName = "Actor", ActorStatus = ActorStatus.Retired };

            _actorRepositoryMock.Setup(x => x.GetByIdAsync(actorId, CancellationToken.None)).ReturnsAsync(existingActorEntity);
            _actorRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<ActorEntity>(), CancellationToken.None)).ReturnsAsync(existingActorEntity);

            // Act
            await _actorService.UpdateAsync(updatedActorModel, CancellationToken.None);

            // Assert
            _actorRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ActorEntity>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UpdateActor_NonExistingActorId_ReturnsNull()
        {
            var actorId = Guid.NewGuid();
            _actorRepositoryMock.Setup(x => x.GetByIdAsync(actorId, CancellationToken.None)).ReturnsAsync((ActorEntity)null);

            // Act
            var result = await _actorService.UpdateAsync(new Actor { Id = actorId }, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteActor_ExistingActorId_CallsDeleteAsyncWithCorrectActorEntity()
        {
            // Arrange
            var actorId = Guid.NewGuid();
            var existingActor = new ActorEntity { Id = actorId, FirstName = "Existing", LastName = "Actor", ActorStatus = ActorStatus.Active };
            _actorRepositoryMock.Setup(x => x.GetByIdAsync(actorId, CancellationToken.None)).ReturnsAsync(existingActor);
            _actorRepositoryMock.Setup(x => x.DeleteAsync(existingActor, CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            await _actorService.DeleteAsync(actorId, CancellationToken.None);

            // Assert
            _actorRepositoryMock.Verify(x => x.DeleteAsync(existingActor, CancellationToken.None), Times.Once);
        }


    }
}

