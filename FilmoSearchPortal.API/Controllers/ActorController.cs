using AutoMapper;
using FilmoSearchPortal.API.ViewModels.Actor;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.API.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;

        public ActorController(IMapper mapper, IActorService actorService)
        {
            _mapper = mapper;
            _actorService = actorService;

        }

        [HttpGet]
        public async Task<IEnumerable<ActorViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var actors = await _actorService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ActorViewModel>>(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActorViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var actor = await _actorService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<ActorViewModel>(actor);
        }

        [HttpPost]
        public async Task<ActorViewModel> Create(CreateActorViewModel viewModel, CancellationToken cancellationToken)
        {
            var actor = _mapper.Map<Actor>(viewModel);
            var result = await _actorService.CreateAsync(actor, cancellationToken);

            return _mapper.Map<ActorViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<ActorViewModel> Update(Guid id, UpdateActorViewModel viewModel, CancellationToken cancellationToken)
        {
            var modelToUpdate = _mapper.Map<Actor>(viewModel);
            modelToUpdate.Id = id;
            var result = await _actorService.UpdateAsync(modelToUpdate, cancellationToken);

            return _mapper.Map<ActorViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _actorService.DeleteAsync(id, cancellationToken);

        }
    }
}
