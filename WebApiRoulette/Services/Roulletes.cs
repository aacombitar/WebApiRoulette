using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Data.Repositorio;
using WebApiRoulette.Dto;

namespace WebApiRoulette.Services
{
    public class Roulletes : IRoulettes
    {
        private readonly RouletteRepository betsRepository;
        public Roulletes(RouletteRepository betsRepository)
        {
            this.betsRepository = betsRepository;
        }
        public async Task<bool> OpenRoulette(int idRoulette)
        {
            return await betsRepository.OpenRouletteAsync(idRoulette: idRoulette);
        }
        public async Task<RouletteDto> CreateRoulette()
        {
            return await betsRepository.CreateRouletteAsync();
        }
    }
}
