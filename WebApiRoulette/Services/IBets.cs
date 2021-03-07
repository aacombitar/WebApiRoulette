using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Dto;

namespace WebApiRoulette.Services
{
    public interface IBets
    {
        Task<bool> CreateBets(BetCreateDto betCreateDto);
        Task<List<BetDto>> ResultsBets(int idRoulette);
    }
}
