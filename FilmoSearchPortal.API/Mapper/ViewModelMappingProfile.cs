using AutoMapper;
using FilmoSearchPortal.API.ViewModels.Actor;
using FilmoSearchPortal.API.ViewModels.Film;
using FilmoSearchPortal.API.ViewModels.Review;
using FilmoSearchPortal.API.ViewModels.User;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;

namespace FilmoSearchPortal.API.Mapping
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {

            CreateMap<Film, FilmViewModel>();
            CreateMap<Actor, ActorViewModel>();
            CreateMap<Review, ReviewViewModel>();


            CreateMap<FilmViewModel, Film>();
            CreateMap<ActorViewModel, Actor>();
            CreateMap<ReviewViewModel, Review>();


            CreateMap<CreateFilmViewModel, Film>();
            CreateMap<UpdateFilmViewModel, Film>();

            CreateMap<CreateActorViewModel, Actor>();
            CreateMap<UpdateActorViewModel, Actor>();

            CreateMap<CreateReviewViewModel, Review>();
            CreateMap<UpdateReviewViewModel, Review>();


            CreateMap<UserViewModel, UserEntity>();
            CreateMap<UserEntity, UserViewModel>();
        }
    }
}
