using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Dto;

namespace WebApiRoulette.Services
{
    public interface IRoulettes
    {
        Task<RouletteDto> CreateRoulette();
        Task<bool> OpenRoulette(int idRoulette);
    }
}
