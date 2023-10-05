using MoviesApi.Models;

namespace MoviesApi.Services.Emplemintaions
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;
        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            Save();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            Save();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync(byte genreId = 0)
        {
            return await _context.Movies.Where(m=>m.GenreId == genreId || genreId == 0).OrderBy(m=>m.Rate).Include(m=>m.Genre).ToListAsync();
        }

        public async Task<Movie> GetMovieAsync(int id)
        { 
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
            _context.Movies.Update(movie);
            Save();
            return movie;
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
