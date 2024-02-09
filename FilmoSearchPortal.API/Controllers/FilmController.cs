using AutoMapper;
using FilmoSearchPortal.API.ViewModels.Film;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.API.Controllers
{
    [ApiController]
    [Route("api/films")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public FilmController(IMapper mapper, IFilmService filmService)
        {
            _mapper = mapper;
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<IEnumerable<FilmViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var films = await _filmService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<FilmViewModel>>(films);
        }

        [HttpGet("{id}")]
        public async Task<FilmViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var film = await _filmService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<FilmViewModel>(film);
        }

        [HttpPost]
        public async Task<FilmViewModel> Create(CreateFilmViewModel viewModel, CancellationToken cancellationToken)
        {
            var film = _mapper.Map<Film>(viewModel);

            var result = await _filmService.CreateAsync(film, cancellationToken);

            return _mapper.Map<FilmViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<FilmViewModel> Update(Guid id, UpdateFilmViewModel viewModel, CancellationToken cancellationToken)
        {
            var modelToUpdate = _mapper.Map<Film>(viewModel);
            modelToUpdate.Id = id;
            var result = await _filmService.UpdateAsync(modelToUpdate, cancellationToken);

            return _mapper.Map<FilmViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _filmService.DeleteAsync(id, cancellationToken);

        }
    }
}
