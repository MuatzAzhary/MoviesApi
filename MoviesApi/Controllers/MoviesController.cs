namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenreService _genresService;
        private readonly IImagesUpload _imagesUpload;
        private readonly IMapper _mapper;
        public MoviesController(IImagesUpload imagesUpload, IMoviesService moviesService, IGenreService genresService, IMapper mapper)
        {
            _imagesUpload = imagesUpload;
            _moviesService = moviesService;
            _genresService = genresService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of movies
        /// </summary>
        /// <returns>return all movies list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _moviesService.GetAllMoviesAsync();
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        /// <summary>
        /// Get the movie with given id 
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>Movie</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieAsync(int id)
        {
            var movie = await _moviesService.GetMovieAsync(id);

            if (movie is null)
                return NotFound($"No movie found with id {id}");

            var data = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(data);
        }

        /// <summary>
        /// Get list of movies under the given genre
        /// </summary>
        /// <param name="genreId">Genre id</param>
        /// <returns>list of movies</returns>
        [HttpGet("/GetByGenreId/")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _moviesService.GetAllMoviesAsync(genreId);
            if (!movies.Any())
                return NotFound($"No movies in genre id {genreId}");

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        /// <summary>
        /// Create new movie
        /// </summary>
        /// <param name="movieDto">object of type movie Dto</param>
        /// <returns>return movie if creation success</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync([FromForm] CreateMovieDto movieDto)
        {
            if(movieDto.Image is not null)
            {
                var imageName =$"{Guid.NewGuid()}{Path.GetExtension(movieDto.Image.FileName)}";
                var (uploaded, errorMessege) =await _imagesUpload.UploadAsync(movieDto.Image,imageName,"Images/");
                if (uploaded)
                    movieDto.Poster = $"/Images/{imageName}";
                else
                    return BadRequest(errorMessege);
            }

            if(!(await _genresService.IsValidGenre(movieDto.GenreId)))
                return BadRequest("Invalid Genre Id");

            var movie = _mapper.Map<Movie>(movieDto);

            await _moviesService.AddAsync(movie);
            return Ok(movie);
        }

        /// <summary>
        /// Update an existing movie
        /// </summary>
        /// <param name="id">movie id</param>
        /// <param name="movieDto">object of type movie dto</param>
        /// <returns>return the updated movie when success</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(int id , [FromForm]UpdateMovieDto movieDto)
        {
            var movie = await _moviesService.GetMovieAsync(id);
            if (movie is null)
                return NotFound();

            movieDto.Poster = movie.Poster;

            if (!(await _genresService.IsValidGenre(movieDto.GenreId)))
                return BadRequest("Invalid Genre Id");

            if (movieDto.Image is not null)
            {
                if(movieDto.Poster is not null) 
                    _imagesUpload.Delete(movieDto.Poster);

                var imageName = $"{Guid.NewGuid()}{movieDto.Image.FileName}";
                var (uploded, errorMessege) = await _imagesUpload.UploadAsync(movieDto.Image, imageName, "/Images/");
                if (uploded)
                    movieDto.Poster = imageName;
                else
                    return BadRequest(errorMessege);               
            }

            movie.Title = movieDto.Title;
            movie.StoreLine = movieDto.StoreLine;
            movie.Rate = movieDto.Rate;
            movie.Year = movieDto.Year;
            movie.GenreId = movieDto.GenreId;
            movie.Poster = movieDto.Poster!;

            _moviesService.Update(movie);
            return Ok(movie);
        }

        /// <summary>
        /// Delete an existing movie 
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>ok when deleted</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _moviesService.GetMovieAsync(id);
            if (movie is null)
                return NotFound();

            _moviesService.Delete(movie);
            return Ok();
        }
    }
}

