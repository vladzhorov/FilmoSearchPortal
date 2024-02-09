using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.BLL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public ICollection<ReviewEntity> Reviews { get; set; }
    }
}
