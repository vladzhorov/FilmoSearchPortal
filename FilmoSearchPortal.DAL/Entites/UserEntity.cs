namespace FilmoSearchPortal.DAL.Entites
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public ICollection<ReviewEntity> Reviews { get; set; }

        public Guid UserId { get; set; }
        public UserEntity()
        {
            UserId = Id;
        }
    }
}
