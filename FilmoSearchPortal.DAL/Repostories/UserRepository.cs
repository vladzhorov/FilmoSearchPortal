using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.DAL.Repostories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
