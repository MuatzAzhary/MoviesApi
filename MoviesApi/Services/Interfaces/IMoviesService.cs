namespace MoviesApi.Services.Interfaces
{
    public interface IMoviesService
    {   
        /// <summary>
        /// Get all movies 
        /// </summary>
        /// <param name="genreId">genre id</param>
        /// <returns>return list All of movies || list of movies with given genre id</returns>
        Task<IEnumerable<Movie>> GetAllMoviesAsync(byte genreId = 0);
        Task<Movie> GetMovieAsync(int id);
        Task<Movie> AddAsync(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
        void Save();
    }
}
