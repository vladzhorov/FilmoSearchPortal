namespace FilmoSearchPortal.BLL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public Guid UserId { get; set; }
    }
}
