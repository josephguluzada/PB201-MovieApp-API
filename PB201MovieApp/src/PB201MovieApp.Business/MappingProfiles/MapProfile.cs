using AutoMapper;
using PB201MovieApp.Business.DTOs.GenreDtos;
using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Core.Entities;

namespace PB201MovieApp.Business.MappingProfiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Movie, MovieGetDto>().ReverseMap();
        CreateMap<Movie, MovieCreateDto>().ReverseMap();
        CreateMap<Movie, MovieUpdateDto>().ReverseMap();

        CreateMap<Genre, GenreGetDto>().ReverseMap();
        CreateMap<Genre,GenreCreateDto>().ReverseMap();
        CreateMap<Genre, GenreUpdateDto>().ReverseMap();
    }
}
