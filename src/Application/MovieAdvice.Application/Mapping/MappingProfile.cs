using AutoMapper;
using MovieAdvice.Application.Dtos.Comment;
using MovieAdvice.Application.Dtos.Movie;
using MovieAdvice.Application.Dtos.User;
using MovieAdvice.Domain.ApiModels.MovieApi;
using MovieAdvice.Domain.Models;
using Pipelines.Sockets.Unofficial.Arenas;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieAdvice.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieApiModel, Movie>().ForMember(x=>x.Id, t=>t.Ignore());

            CreateMap<MovieListDto, Movie>();
            CreateMap<Movie, MovieListDto>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();

            CreateMap<CommentSaveDto,CommentAddDto>();
            CreateMap<CommentAddDto, CommentSaveDto>();

            CreateMap<CommentSaveDto, Comment>();
            CreateMap<Comment, CommentSaveDto>();

            CreateMap<Movie, GetMovieDto>();
            CreateMap<GetMovieDto, Movie>();

            CreateMap<Comment, CommentAddDto>();
            CreateMap<CommentAddDto, Comment>();

        }
    }
}
