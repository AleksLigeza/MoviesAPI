using System.Collections;
using System.Collections.Generic;

namespace MoviesAPI.DbModels
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
