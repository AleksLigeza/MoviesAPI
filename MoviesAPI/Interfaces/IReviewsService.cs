using System.Collections.Generic;
using MoviesAPI.DbModels;

namespace MoviesAPI.Interfaces
{
    public interface IReviewsService
    {
        List<Review> GetAll();

        Review GetById(int id);

        void AddNewReview(Review review);

        bool UpdateReview(Review review);

        void Remove(int reviewId);

        List<Review> GetByMovieId(int movieId);
    }
}
