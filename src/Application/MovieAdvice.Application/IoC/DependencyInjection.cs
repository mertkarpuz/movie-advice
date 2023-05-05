using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Mapping;
using MovieAdvice.Application.Services;
using MovieAdvice.Application.Utilities;
using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Infrastructure.Context;
using MovieAdvice.Infrastructure.Repositories;
using System.Text;

namespace MovieAdvice.Application.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration _configuration)
        {
            Configuration configuration = _configuration.Get<Configuration>();
            services.AddSingleton(configuration);

            services.AddDbContext<MovieAdviceDbContext>(options =>
            options.UseSqlServer(configuration.ConnectionStrings.SQLConnection),ServiceLifetime.Singleton);

            services.AddSingleton<IMoviesService, MoviesService>();
            services.AddSingleton<IMovieRepository, MovieRepository>();

            services.AddSingleton<IGetMoviesService, GetMoviesService>();
            services.AddSingleton<IHttpUtilities, HttpUtilities>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
