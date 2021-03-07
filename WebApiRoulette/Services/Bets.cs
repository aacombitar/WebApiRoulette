using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Data.Repositorio;
using WebApiRoulette.Dto;

namespace WebApiRoulette.Services
{
    public class Bets : IBets
    {
        private readonly BetsRepository betsRepository;
        public Bets(BetsRepository betsRepository)
        {
            this.betsRepository = betsRepository;
        }
        public async Task<bool> CreateBets(BetCreateDto betCreateDto)
        {
            return await betsRepository.CreateBetAsync(betCreateDto);
        }
        public async Task<List<BetDto>> ResultsBets(int idRoulette)
        {
            return await betsRepository.ResultBetAsync(idRoulette: idRoulette);
        }
    }
}
