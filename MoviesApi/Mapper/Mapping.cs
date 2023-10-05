namespace MoviesApi.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            //Movies Mapping
            CreateMap<Movie,MovieDetailsDto>();
            CreateMap<CreateMovieDto,Movie>();
        }
    }
}