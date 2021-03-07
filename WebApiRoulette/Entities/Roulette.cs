using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRoulette.Entities
{
    public class Roulette
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
    }
}
