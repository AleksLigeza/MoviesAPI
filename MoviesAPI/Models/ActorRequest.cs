using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class ActorRequest
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Lastname { get; set; }
    }
}
