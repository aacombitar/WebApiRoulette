using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRoulette.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        [Required]
        public int RouletteId { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public double ValueBet { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool Winner { get; set; }
        public double ValuePayout { get; set; }
        public DateTime DateBet { get; set; }

    }
}
