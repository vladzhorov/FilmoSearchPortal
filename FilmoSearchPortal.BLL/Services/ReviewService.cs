using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;

namespace FilmoSearchPortal.BLL.Services
{
    public class ReviewService : GenericService<ReviewEntity, Review>, IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper) : base(reviewRepository, mapper)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetAllAsync(int pageNumber, int pageSize, string? title, int? stars, CancellationToken cancellationToken)
        {
            var reviewsEntities = await _reviewRepository.GetAllAsync(pageNumber, pageSize, title, stars, cancellationToken);
            return _mapper.Map<IEnumerable<Review>>(reviewsEntities);
        }
    }
}
