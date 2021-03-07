using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Contexts;
using WebApiRoulette.Dto;
using WebApiRoulette.Entities;

namespace WebApiRoulette.Data.Repositorio
{
    public class RouletteRepository
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly ILogger<RouletteRepository> logger;

        public RouletteRepository(ApplicationDBContext context, IMapper mapper, ILogger<RouletteRepository> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<bool> OpenRouletteAsync(int idRoulette)
        {
            try
            {
                var roulette = await (from r in context.Roulettes
                                      where r.Id == idRoulette
                                      select r).FirstOrDefaultAsync();
                if (roulette == null)
                {
                    return false;
                }
                else
                {
                    roulette.IsOpen = true;
                    roulette.OpenDate = DateTime.Now.ToUniversalTime();
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Error en OpenRouletteAsync" + ex.Message);
                return false;
            }

        }

        public async Task<RouletteDto> CreateRouletteAsync()
        {
            Roulette rouletteCreate = new Roulette
            {
                IsOpen = false,
                Name = "Ruleta"
            };
            context.Add(rouletteCreate);
            await context.SaveChangesAsync();

            return mapper.Map<RouletteDto>(rouletteCreate);
        }
    }
}
