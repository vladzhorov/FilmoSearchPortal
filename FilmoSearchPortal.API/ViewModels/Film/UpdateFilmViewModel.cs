
using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.API.ViewModels.Film
{
    public class UpdateFilmViewModel
    {
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
