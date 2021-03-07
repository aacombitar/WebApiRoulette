using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiRoulette.Helpers;

namespace WebApiRoulette.Dto
{
    public class BetCreateDto
    {
        public int RouletteId { get; set; }
        [Required]
        [Range(0,36)]
        public int Number { get; set; }
        [Required]
        [ValidateColorBets]
        public string Color { get; set; }
        [Required]
        [Range(1, 10000)]
        public double ValueBet { get; set; }
        public string UserId { get; set; }
        public DateTime DateBet { get; set; }

    }
}
