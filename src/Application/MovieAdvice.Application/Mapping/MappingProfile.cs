using AutoMapper;
using MovieAdvice.Domain.ApiModels.MovieApi;
using MovieAdvice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieApiModel, Movie>().ForMember(x=>x.Id, t=>t.Ignore());
        }
    }
}
