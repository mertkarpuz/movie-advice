﻿using MovieAdvice.Domain.Models;


namespace MovieAdvice.Domain.Interfaces
{
    public interface IMovieRepository
    {
        void SaveMovies(List<Movie> movieList);
        Task<Movie> GetMovie(int movieId);
        Task<List<Movie>> GetActiveMovies(int pageIndex);
        Task<int> GetActiveMoviesTotalPage();
        Task<bool> IsMovieExists(int movieId);
        Task<Movie> GetMovieByTitle(string movieTitle);
    }
}
