using AutoMapper;
using GetMoviesWorker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Mapping;
using MovieAdvice.Application.Services;
using MovieAdvice.Application.Utilities;
using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Infrastructure.Context;
using MovieAdvice.Infrastructure.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            IConfiguration iConfiguration = hostContext.Configuration;
            Configuration configuration = iConfiguration.Get<Configuration>();
            services.AddSingleton(configuration);
            services.AddHostedService<Worker>();
            services.AddScoped<IMoviesService, MoviesService>();
            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            services.AddTransient<IHttpUtilities, HttpUtilities>();
            services.AddTransient<IGetMoviesService, GetMoviesService>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddDbContext<MovieAdviceDbContext>(options =>
            options.UseSqlServer(iConfiguration.GetSection("ConnectionStrings:SQLConnection").Value), ServiceLifetime.Transient);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


        }).ConfigureAppConfiguration((hostContext, configBuilder) =>
        {
            var env = hostContext.HostingEnvironment;
            var sharedFolder = Path.Combine(env.ContentRootPath, "../..", "ConfigFiles");
            configBuilder.AddJsonFile(Path.Combine(sharedFolder, "sharedsettings.json"), optional: true)
            .AddJsonFile("sharedsettings.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true);
        })
    .Build();

await host.RunAsync();