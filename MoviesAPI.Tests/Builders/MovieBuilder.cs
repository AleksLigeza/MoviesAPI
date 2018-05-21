using System;
using System.Collections.Generic;
using System.Text;
using MoviesAPI.DbModels;

namespace MoviesAPI.Tests.Builders
{
    public class MovieBuilder
    {
        private int _id;
        private string _title;
        private int _year;
        private ICollection<Review> _reviews;
        private ICollection<MovieRole> _movieRoles;

        public MovieBuilder()
        {
            _id = 1;
            _title = "";
            _year = 0;
            _reviews = new List<Review>();
            _movieRoles = new List<MovieRole>();
        }

        public MovieBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public MovieBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public MovieBuilder WithYear(int year)
        {
            _year = year;
            return this;
        }

        public Movie Build()
        {
            return new Movie()
            {
                Id = _id,
                Title = _title,
                Year = _year,
                Roles = _movieRoles,
                Reviews = _reviews
            };
        }

    }
}
