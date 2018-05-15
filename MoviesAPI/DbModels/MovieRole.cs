using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DbModels
{
    public class MovieRole
    {
        public int Id { get; set; }
        
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        
        public virtual Movie Movie { get; set; }
        public virtual Actor Actor { get; set; }
    }
}
