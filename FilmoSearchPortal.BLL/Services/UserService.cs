using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.BLL.Services
{
    public class UserService : GenericService<UserEntity, User>, IUserService
    {


        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {

        }
    }
}
