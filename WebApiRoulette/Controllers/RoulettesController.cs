using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Contexts;
using WebApiRoulette.Dto;
using WebApiRoulette.Entities;
using WebApiRoulette.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiRoulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoulettesController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IBets bets;
        private readonly IRoulettes roulettes;
        private readonly ILogger<RoulettesController> logger;
        private StringValues headerValues;
        public RoulettesController(ApplicationDBContext context, IMapper mapper, IBets bets, IRoulettes roulettes, ILogger<RoulettesController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.bets = bets;
            this.roulettes = roulettes;
            this.logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<RouletteDto>> CreateRoulette()
        {
            RouletteDto roulletteDto = await roulettes.CreateRoulette();

            return new CreatedAtRouteResult("GetRoulette", new { id = roulletteDto.Id }, roulletteDto.Id);
        }
        [HttpGet("open/{idroulette:int}")]
        public async Task<ActionResult> OpenRoulette(int idroulette)
        {
            var result = await roulettes.OpenRoulette(idroulette);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}", Name = "GetRoulette")]
        public ActionResult<Roulette> GetRoulette(int id)
        {
            var rouletteResult = context.Roulettes.FirstOrDefault(x => x.Id == id);
            if (rouletteResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(rouletteResult);
            }
        }
        [HttpPost("bet")]
        public async Task<ActionResult> CreateBet([FromBody] BetCreateDto betCreateDto)
        {
            string headerValue = null;
            if (Request.Headers.TryGetValue("UserId", out headerValues))
            {
                headerValue = headerValues.FirstOrDefault();
            }
            if (string.IsNullOrEmpty(headerValue)) return BadRequest("El UserId es requerido en la cabecera");
            betCreateDto.UserId = headerValue;
            var result = await bets.CreateBets(betCreateDto);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("ResultBet/{id}")]
        public async Task<ActionResult<List<BetDto>>> ResultBet(int id)
        {
            var rouletteResult = await bets.ResultsBets(id);
            if (rouletteResult == null)
            {
                logger.LogWarning($"La ruleta {id}  ya se encuentra cerrada o no existe");
                return NotFound();
            }
            else
            {
                return rouletteResult;
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Roulette>> GetAllRoulette()
        {
            return context.Roulettes.ToList();
        }

    }
}
