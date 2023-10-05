namespace MoviesApi.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetGenreAsync(byte id);
        Task<Genre> AddAsync(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<bool> IsValidGenre(byte id);
        void Save();
    }
}
