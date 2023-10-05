namespace MoviesApi.Dtos
{
    public class UpdateMovieDto : BasedDto
    {
        public IFormFile? Image { get; set; }
    }
}
