using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class ActorTestData
    {
        public static (List<ActorEntity>, List<Actor>) TestGetAllActors()
        {
            var actorEntity1 = new ActorEntity { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", ActorStatus = ActorStatus.Active };
            var actorEntity2 = new ActorEntity { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", ActorStatus = ActorStatus.Retired };

            var actorModel1 = new Actor { Id = actorEntity1.Id, FirstName = actorEntity1.FirstName, LastName = actorEntity1.LastName, ActorStatus = actorEntity1.ActorStatus };
            var actorModel2 = new Actor { Id = actorEntity2.Id, FirstName = actorEntity2.FirstName, LastName = actorEntity2.LastName, ActorStatus = actorEntity2.ActorStatus };

            var actorsEntities = new List<ActorEntity> { actorEntity1, actorEntity2 };
            var actorModels = new List<Actor> { actorModel1, actorModel2 };

            return (actorsEntities, actorModels);
        }

        public static (Guid, ActorEntity, Actor) CreateActor()
        {
            var actorId = Guid.NewGuid();
            var actorEntity = new ActorEntity { Id = actorId, FirstName = "Actor First Name", LastName = "Actor Last Name", ActorStatus = ActorStatus.Active };
            var actorModel = new Actor { Id = actorEntity.Id, FirstName = actorEntity.FirstName, LastName = actorEntity.LastName, ActorStatus = actorEntity.ActorStatus };
            return (actorId, actorEntity, actorModel);
        }
    }
}
