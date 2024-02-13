using FilmoSearchPortal.API.ViewModels.Actor;
using FilmoSearchPortal.API.ViewModels.Review;

using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.API.ViewModels.Film
{
    public class FilmViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<ActorViewModel> Actors { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}
