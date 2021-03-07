using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRoulette.Dto
{
    public class RouletteDto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
