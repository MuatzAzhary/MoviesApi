namespace MoviesApi.Dtos
{
    public class BasedDto
    {
        [MaxLength(250)]
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2200)]
        public string StoreLine { get; set; } = null!;
        public string? Poster { get; set; }
        public byte GenreId { get; set; }
    }
}
