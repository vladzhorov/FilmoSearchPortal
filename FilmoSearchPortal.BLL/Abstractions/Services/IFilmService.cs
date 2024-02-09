using FilmoSearchPortal.BLL.Models;
using FilmoSearchPortal.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmoSearchPortal.BLL.Abstractions.Services
{
    public interface IFilmService : IGenericService<FilmEntity, Film>
    {
    }
}
