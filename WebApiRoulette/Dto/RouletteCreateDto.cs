using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRoulette.Dto
{
    public class RouletteCreateDto
    {
        [Required]
        public string Nombre { get; set; }
    }
}
