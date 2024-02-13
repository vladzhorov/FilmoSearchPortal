

using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.BLL.Models
{
    public class Film
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
