using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.BLL.Services
{
    public class ActorService : GenericService<ActorEntity, Actor>, IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorService(IActorRepository actorRepository, IMapper mapper) : base(actorRepository, mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync(int pageNumber, int pageSize, string? firstName, string? lastName, ActorStatus? actorStatus, CancellationToken cancellationToken)
        {
            var actorEntities = await _actorRepository.GetAllAsync(pageNumber, pageSize, firstName, lastName, actorStatus, cancellationToken);
            return _mapper.Map<IEnumerable<Actor>>(actorEntities);
        }
    }
}
