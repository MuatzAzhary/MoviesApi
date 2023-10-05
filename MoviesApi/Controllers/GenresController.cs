namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        ///  Get all genres 
        /// </summary>
        /// <returns>return list of genres</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }

        /// <summary>
        /// Create a new genre
        /// </summary>
        /// <param name="genreDto">object of type genre Dto</param>
        /// <returns>return the new genre if creation success</returns>
        [HttpPost]
        public async Task<IActionResult> CreateGenreAsync(GenreDto genreDto)
        {
            Genre genre = new() { Name = genreDto.Name };
            await _genreService.AddAsync(genre); 

            return Ok(genre);
        }

        /// <summary>
        /// Update an existing genre by id and genre object
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <param name="genreDto">object of type genre Dto</param>
        /// <returns>return the updated genre if updated success</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenreAsync(byte id, [FromBody] GenreDto genreDto)
        {
            var genre = await _genreService.GetGenreAsync(id);
            if (genre is null)
                return NotFound($"No genre was found with id {id}");

            genre.Name = genreDto.Name;
            _genreService.Update(genre);

            return Ok(genre);
        }

        /// <summary>
        /// Deleting and existing genre by id
        /// </summary>
        /// <param name="id">genre id</param>
        /// <returns>return ok if deleting success</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreAsync(byte id)
        {
            var genre = await _genreService.GetGenreAsync(id);
            if (genre is null)
                return NotFound($"No genre was found with id {id}");

            _genreService.Delete(genre);
            return Ok();
        }
    }
}

