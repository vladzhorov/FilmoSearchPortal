using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.BLL.Services
{
    public class UserService : GenericService<UserEntity, User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper)
            : base(userRepository, mapper)
        {
            _userRepository = userRepository;
        }
    }
}
