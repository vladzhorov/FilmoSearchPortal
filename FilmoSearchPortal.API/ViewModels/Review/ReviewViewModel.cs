namespace FilmoSearchPortal.API.ViewModels.Review
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public DateTime ReviewDate { get; set; }
        public int FilmId { get; set; }
    }
}
