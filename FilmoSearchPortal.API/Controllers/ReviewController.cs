using AutoMapper;
using FilmoSearchPortal.API.ViewModels.Review;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearchPortal.API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IMapper mapper, IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReviewViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ReviewViewModel>>(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ReviewViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<ReviewViewModel>(review);
        }

        [HttpPost]
        public async Task<ReviewViewModel> Create(CreateReviewViewModel viewModel, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(viewModel);
            var result = await _reviewService.CreateAsync(review, cancellationToken);

            return _mapper.Map<ReviewViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<ReviewViewModel> Update(Guid id, UpdateReviewViewModel viewModel, CancellationToken cancellationToken)
        {
            var modelToUpdate = _mapper.Map<Review>(viewModel);
            modelToUpdate.Id = id;
            var result = await _reviewService.UpdateAsync(modelToUpdate, cancellationToken);

            return _mapper.Map<ReviewViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _reviewService.DeleteAsync(id, cancellationToken);

        }
    }
}
