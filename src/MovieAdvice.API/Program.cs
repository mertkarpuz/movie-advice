using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.OpenApi.Models;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Application.IoC;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MovieAdvice API",
        Description = "A Web API for where you can list and view the movies that are shown, comment and rate and recommend to your friends.",
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.linkedin.com/in/mustafa-mert-karpuz-32514a1a7/")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    var sharedFolder = Path.Combine(env.ContentRootPath, "..", "ConfigFiles");
    config.AddJsonFile(Path.Combine(sharedFolder, "sharedsettings.json"), optional: true)
    .AddJsonFile("sharedsettings.json", optional: true);
});

DependencyInjection.RegisterServices(builder.Services, builder.Configuration);

builder.Services.AddHangfire(config => config
            .UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
            .UseSqlServerStorage("Server=DESKTOP-1SLG2VP;User Id=sa;Password=phoenix1;Database=MovieAdviceDB;TrustServerCertificate=True")
            );
builder.Services.AddHangfireServer();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "MovieAdvice Hangfire",
    Authorization = new[] {
        new HangfireCustomBasicAuthenticationFilter()
        {
            User = "Mert",
            Pass = "Test"
        }
    }
});
app.MapHangfireDashboard();

RecurringJob.AddOrUpdate<IGetMoviesService>("SnycMovies", x => x.SyncMovies(), Cron.Minutely);

app.Run();
