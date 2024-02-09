namespace FilmoSearchPortal.DAL.Entites
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public ICollection<ReviewEntity> Reviews { get; set; }
    }
}
