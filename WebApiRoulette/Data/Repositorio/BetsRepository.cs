using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class BetsRepository
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly ILogger<BetsRepository> logger;

        public BetsRepository(ApplicationDBContext context, IMapper mapper, ILogger<BetsRepository> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<bool> CreateBetAsync(BetCreateDto betCreateDto)
        {
            try
            {
                betCreateDto.RouletteId = await GetIdRouletteOpened();
                betCreateDto.DateBet = DateTime.Now.ToUniversalTime();
                var betNew = mapper.Map<Bet>(betCreateDto);
                context.Add(betNew);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogCritical("Error en CreateBetAsync" + ex.Message);
                return false;
            }
        }
        private async Task<int> GetIdRouletteOpened()
        {
            Random rnd = new Random();
            var lstRoulette = await (from r in context.Roulettes
                                     where r.IsOpen
                                     select r.Id).ToListAsync();
            var idRoulette = lstRoulette.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault();

            return idRoulette;
        }
        public async Task<List<BetDto>> ResultBetAsync(int idRoulette)
        {
            try
            {
                var existRoulette = await CloseRoullet(idRoulette: idRoulette);
                if (!existRoulette)
                {
                    return null;
                }
                var lstRoulette = await SetNumberWinner(idRoulette: idRoulette);
                var lstRouletteDto = mapper.Map<List<BetDto>>(lstRoulette);
                return lstRouletteDto;
            }
            catch (Exception ex)
            {
                logger.LogCritical("Error en CreateBetAsync" + ex.Message);
                return null;
            }
        }
        private async Task<bool> CloseRoullet(int idRoulette)
        {
            Roulette roulette = await (from r in context.Roulettes
                                       where r.IsOpen && r.Id == idRoulette
                                       select r).FirstOrDefaultAsync();
            if (roulette == null)
            {
                return false;
            }
            roulette.IsOpen = false;
            roulette.CloseDate = DateTime.Now.ToUniversalTime();
            await context.SaveChangesAsync();
            return true;
        }
        private async Task<List<Bet>> SetNumberWinner(int idRoulette)
        {
            Random rnd = new Random();
            int numWinner = rnd.Next(0, 36);
            return await setWinnerPayout(idRoulette: idRoulette, numWinner: numWinner);
        }
        private async Task<List<Bet>> setWinnerPayout(int idRoulette, int numWinner)
        {
            var lstBet = await (from b in context.Bets
                                join r in context.Roulettes
         on b.RouletteId equals r.Id
                                where b.RouletteId == idRoulette &&
                                (b.DateBet >= r.OpenDate && b.DateBet <= r.CloseDate)
                                select b).ToListAsync();
            foreach (var winner in lstBet)
            {
                if (winner.Number == numWinner)
                {
                    winner.ValuePayout = winner.ValueBet * 5;
                    winner.Winner = true;
                }
                else
                {
                    var winnerByColor = (winner.Number + 2) % 2 == 0;
                    var validateNumbre = ((numWinner + 2) % 2 == 0);
                    if (winnerByColor == validateNumbre)
                    {
                        winner.ValuePayout = ((winner.ValueBet * 1.8) / 100) + winner.ValueBet;
                        winner.Winner = true;
                    }

                }
                await context.SaveChangesAsync();
            }

            return lstBet;
        }
    }
}
