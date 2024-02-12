namespace FilmoSearchPortal.API.ViewModels.Review
{
    public class CreateReviewViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public Guid FilmId { get; set; }
        public Guid UserId { get; set; }
    }
}
