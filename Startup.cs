using Microsoft.OpenApi.Models;
using api_games_catalog.Services;
using api_games_catalog.Repositories;
using api_games_catalog.Controllers.V1;
using api_games_catalog.Middleware;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;


namespace api_games_catalog
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.


    public void ConfigureServices(IServiceCollection services)
    {

      services.AddScoped<IGameService, GameService>();
      services.AddScoped<IGameRepository, GameRepository>();
      services.AddRouting(options =>
      {
        options.LowercaseUrls = true;
      });

      services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();
      services.AddScoped<IExemploScoped, ExemploCicloDeVida>();
      services.AddTransient<IExemploTransient, ExemploCicloDeVida>();

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "api_games_catalog", Version = "v1" });
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
        c.IncludeXmlComments(Path.Combine(basePath, fileName));
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_games_catalog v1"));
      }
      app.UseMiddleware<ExceptionMiddleware>();
      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
