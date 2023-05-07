using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            options.UseSqlServer(configuration.ConnectionStrings.SQLConnection),ServiceLifetime.Transient);

            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IGetMoviesService, GetMoviesService>();
            services.AddScoped<IHttpUtilities, HttpUtilities>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRabbitMqHelper, RabbitMqHelper>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<IJwtService, JwtService>();


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = configuration.JwtConfiguration.Issuer,
                    ValidAudience = configuration.JwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JwtConfiguration.SecurityKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddFluentValidationAutoValidation();
        }
    }
}
