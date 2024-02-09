namespace FilmoSearchPortal.DAL.Entites
{
    public class ReviewEntity : BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public DateTime ReviewDate { get; set; }
        public Guid FilmId { get; set; }
        public FilmEntity Film { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
