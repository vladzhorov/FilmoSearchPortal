using AutoMapper;
using FilmoSearchPortal.API.ViewModels.Actor;
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
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;

        public FilmController(IMapper mapper, IFilmService filmService, IActorService actorService)
        {
            _mapper = mapper;
            _filmService = filmService;
            _actorService = actorService;
        }

        [HttpGet]
        public async Task<IEnumerable<FilmViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var films = await _filmService.GetAllAsync(cancellationToken);
            var filmViewModels = _mapper.Map<IEnumerable<FilmViewModel>>(films);


            return filmViewModels;
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
        [HttpPost("{filmId}/actors")]
        public async Task<ActionResult<FilmViewModel>> AddActorsToFilm(Guid filmId, ICollection<Guid> actorIds, CancellationToken cancellationToken)
        {
            var film = await _filmService.GetByIdAsync(filmId, cancellationToken);
            if (film == null)
            {
                return NotFound();
            }

            var actors = new List<Actor>();
            foreach (var actorId in actorIds)
            {
                var actor = await _actorService.GetByIdAsync(actorId, cancellationToken);
                if (actor == null)
                {
                    return NotFound($"Actor with ID {actorId} not found.");
                }
                actors.Add(actor);
            }

            film.Actors ??= new List<Actor>();
            foreach (var actor in actors)
            {
                film.Actors.Add(actor);
            }

            // Сохраняем изменения в базе данных
            var result = await _filmService.UpdateAsync(film, cancellationToken);
            if (result == null)
            {
                return BadRequest("Error updating film.");
            }

            // После сохранения обновленного фильма, получаем актуальные данные о фильме
            var updatedFilm = await _filmService.GetByIdAsync(filmId, cancellationToken);

            // Маппим фильм в FilmViewModel
            var filmViewModel = _mapper.Map<FilmViewModel>(updatedFilm);

            // Маппим актеров фильма в ActorViewModel
            filmViewModel.Actors = _mapper.Map<IEnumerable<ActorViewModel>>(updatedFilm.Actors);

            return filmViewModel;
        }




    }
}
