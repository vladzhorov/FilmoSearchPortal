using AutoMapper;
using FilmoSearchPortal.BLL.Abstractions.Services;
using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using FilmoSearchPortal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmoSearchPortal.BLL.Services
{
    public class FilmService : GenericService<FilmEntity, Film>, IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository, IMapper mapper)
            : base(filmRepository, mapper)
        {
            _filmRepository = filmRepository;
        }
    }
}
