using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class TraktTvMovie
    {
        public string title;
        public int year;
        public Ids ids;
    }

    public struct Ids
    {
        public int trakt;
        public string slug;
        public string imdb;
        public int tmdb;
    }
}
