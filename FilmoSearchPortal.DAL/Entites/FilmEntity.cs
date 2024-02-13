using static FilmoSearchPortal.DAL.Entites.EnumsEntity;

namespace FilmoSearchPortal.DAL.Entites
{
    public class FilmEntity : BaseEntity
    {

        public string Title { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<ActorEntity> Actors { get; set; }
        public ICollection<ReviewEntity> Reviews { get; set; }

    }
}
