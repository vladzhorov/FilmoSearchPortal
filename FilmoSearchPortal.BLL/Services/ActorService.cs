using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.BLL.Services
{
    public class ActorService : GenericService<ActorEntity, Actor>, IActorService
    {

        public ActorService(IActorRepository actorRepository, IMapper mapper) : base(actorRepository, mapper)

        {

        }
    }
}


