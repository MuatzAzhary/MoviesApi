namespace MoviesApi.Services.Emplemintaions
{
    public class GenresService : IGenreService
    {
        private readonly ApplicationDbContext _context;
        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            Save();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
            Save();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.OrderBy(g=>g.Name).ToListAsync();
        }

        public async Task<Genre> GetGenreAsync(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g=>g.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Genres.Update(genre);
            Save();
            return genre;
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<bool> IsValidGenre(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
