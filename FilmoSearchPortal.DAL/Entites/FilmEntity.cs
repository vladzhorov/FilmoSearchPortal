namespace FilmoSearchPortal.DAL.Entites
{
    public class FilmEntity : BaseEntity
    {

        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<ActorEntity> Actors { get; set; }
        public ICollection<ReviewEntity> Reviews { get; set; }

    }
}
