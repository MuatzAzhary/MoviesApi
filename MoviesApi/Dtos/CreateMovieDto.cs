namespace MoviesApi.Dtos
{
    public class CreateMovieDto : BasedDto
    {
        public IFormFile Image { get; set; } = null!;
    }
}
