using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.BLL.Abstractions.Services
{
    public interface IUserService : IGenericService<UserEntity, User>
    {
    }
}
