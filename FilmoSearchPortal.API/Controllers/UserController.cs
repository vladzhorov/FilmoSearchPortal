using AutoMapper;
using FilmoSearchPortal.API.ViewModels.User;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IUserService UserService)
        {
            _mapper = mapper;
            _UserService = UserService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var Users = await _UserService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<UserViewModel>>(Users);
        }

        [HttpGet("{id}")]
        public async Task<UserViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var User = await _UserService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<UserViewModel>(User);
        }

        [HttpPost]
        public async Task<UserViewModel> Create(CreateUserViewModel viewModel, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(viewModel);
            var result = await _UserService.CreateAsync(user, cancellationToken);

            return _mapper.Map<UserViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<UserViewModel> Update(Guid id, UpdateUserViewModel viewModel, CancellationToken cancellationToken)
        {
            var modelToUpdate = _mapper.Map<User>(viewModel);
            modelToUpdate.Id = id;
            var result = await _UserService.UpdateAsync(modelToUpdate, cancellationToken);

            return _mapper.Map<UserViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _UserService.DeleteAsync(id, cancellationToken);

        }
    }
}