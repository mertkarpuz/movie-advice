using GetMoviesWorker;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Services;
using MovieAdvice.Application.Utilities;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        services.AddTransient<IHttpUtilities, HttpUtilities>();
        services.AddTransient<IGetMoviesService,GetMoviesService>();
         
    })
    .Build();

await host.RunAsync();
