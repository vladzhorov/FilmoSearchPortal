using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.BLL.Services
{
    public class ActorService : GenericService<ActorEntity, Actor>, IActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository, IMapper mapper)
            : base(actorRepository, mapper)
        {
            _actorRepository = actorRepository;
        }
    }
}
