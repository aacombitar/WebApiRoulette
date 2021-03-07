using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Contexts;
using WebApiRoulette.Entities;
using WebApiRoulette.Dto;
using WebApiRoulette.Services;
using WebApiRoulette.Data.Repositorio;

namespace WebApiRoulette
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
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Roulette, RouletteDto>();
                configuration.CreateMap<RouletteCreateDto, Roulette>();
                configuration.CreateMap<Bet, RouletteDto>();
                configuration.CreateMap<BetCreateDto, Bet>();
            }, typeof(Startup));
            services.AddTransient<IBets, Bets>();
            services.AddTransient<IRoulettes, Roulletes>();
            services.AddScoped<BetsRepository>();
            services.AddScoped<RouletteRepository>();
            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
