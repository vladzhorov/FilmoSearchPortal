using FilmoSearchPortal.API.ViewModels.Actor;
using FilmoSearchPortal.BLL.Models;

namespace FilmoSearchPortal.UnitTest.TestData
{
    public static class ActorControllerTestHelper
    {
        public static (List<Actor> actors, List<ActorViewModel> actorViewModels) TestGetAll()
        {
            var actorId1 = Guid.NewGuid();
            var actorId2 = Guid.NewGuid();

            var actors = new List<Actor>
            {
                new Actor { Id = actorId1, FirstName = "Actor 1" , LastName ="One"  },
                new Actor { Id = actorId2, FirstName = "Actor 2" , LastName ="Two" }
            };

            var actorViewModels = new List<ActorViewModel>
            {
                new ActorViewModel { Id = actorId1,  FirstName = "Actor 1" , LastName ="One" },
                new ActorViewModel { Id = actorId2, FirstName = "Actor 2" , LastName ="Two" }
            };

            return (actors, actorViewModels);
        }

        public static (Guid actorId, Actor actor, ActorViewModel actorViewModel) TestGetById()
        {
            var actorId = Guid.NewGuid();
            var actor = new Actor { Id = actorId, FirstName = "TestActor", LastName = "TestActor" };
            var actorViewModel = new ActorViewModel { Id = actorId, FirstName = "TestActor", LastName = "TestActor" };
            return (actorId, actor, actorViewModel);
        }

        public static (CreateActorViewModel viewModel, Actor actor) TestCreate()
        {
            var actorId = Guid.NewGuid();
            var viewModel = new CreateActorViewModel { FirstName = "TestActor", LastName = "TestActor" };
            var actor = new Actor { Id = actorId, FirstName = viewModel.FirstName, LastName = viewModel.LastName };
            return (viewModel, actor);
        }

        public static (Guid actorId, UpdateActorViewModel viewModel, Actor actor) TestUpdate()
        {
            var actorId = Guid.NewGuid();
            var viewModel = new UpdateActorViewModel { FirstName = "Updated ", LastName = "Actor" };
            var actor = new Actor { Id = actorId, FirstName = viewModel.FirstName, LastName = viewModel.LastName };
            return (actorId, viewModel, actor);
        }
    }
}
