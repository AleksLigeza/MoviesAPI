using System.Collections.Generic;
using System.Linq;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Common;

namespace MoviesAPI.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly MoviesContext moviesContext;

        public ReviewsService(MoviesContext moviesContext)
        {
            this.moviesContext = moviesContext;
        }

        public List<Review> GetAll()
        {
            return this.moviesContext.Reviews.ToList();
        }

        public List<Review> GetByMovieId(int movieId)
        {
            var movie = this.moviesContext.Movies.Find(movieId);
            return movie.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            Review foundMovie = this.moviesContext.Reviews
                  .Where(review => review.Id == id)
                  .SingleOrDefault();

            return foundMovie;
        }

        public void AddNewReview(Review review)
        {
            var movie = this.moviesContext.Movies.Find(review.MovieId);
            if (movie == null)
            {
                throw new MovieApiException("Invalid movie Id");
            }
            movie.Reviews.Add(review);
            this.moviesContext.SaveChanges();  
        }

        public bool UpdateReview(Review review)
        {
            Review foundReview = GetById(review.Id);

            if (foundReview == null)
            {
                return false;
            }

            foundReview.Comment = review.Comment;
            foundReview.MovieId = review.MovieId;
            foundReview.Rate = review.Rate;

            this.moviesContext.SaveChanges();

            return true;
        }

        public void Remove(int reviewId)
        {
            Review review = GetById(reviewId);

            this.moviesContext.Reviews.Remove(review);
            this.moviesContext.SaveChanges();
        }
    }
}
