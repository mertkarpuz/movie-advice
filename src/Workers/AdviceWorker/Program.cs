using AdviceWorker;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.Services;

IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            IConfiguration _configuration = hostContext.Configuration;
            services.AddHostedService<Worker>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            Configuration configuration = _configuration.Get<Configuration>();
            services.AddSingleton(configuration);
            services.AddTransient<IEmailService, EmailService>();

        }).ConfigureAppConfiguration((hostContext, configBuilder) =>
        {
            var env = hostContext.HostingEnvironment;
            var sharedFolder = Path.Combine(env.ContentRootPath, "../..", "ConfigFiles");
            configBuilder.AddJsonFile(Path.Combine(sharedFolder, "sharedsettings.json"), optional: true)
            .AddJsonFile("sharedsettings.json", optional: true);
        })
    .Build();

await host.RunAsync();
