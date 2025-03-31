using AutoMapper;
using MinimalApi.Abstractions.Movies;
using MinimalApi.Core.Cqrs.Commands;
using MinimalApi.Core.Cqrs.Commands.Genres;
using MinimalApi.Core.Cqrs.Commands.Medias.Movies;
using MinimalApi.Core.Cqrs.Commands.Medias.Musics;
using MinimalApi.Core.Cqrs.Commands.Platforms;
using MinimalApi.Core.Cqrs.Commands.Users;
using MinimalApi.Core.Cqrs.Queries.Medias.Movies;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, UserModel>();
        CreateMap<CreateUserCommand, UserEntity>();
        CreateMap<UpdateUserCommand, UserEntity>();
        
        CreateMap<GenreEntity, GenreModel>();
        CreateMap<CreateGenreCommand, GenreEntity>();
        CreateMap<UpdateGenreCommand, GenreEntity>();
        
        CreateMap<PlatformEntity, PlatformModel>();
        CreateMap<CreatePlatformCommand, PlatformEntity>();
        CreateMap<UpdatePlatformCommand, PlatformEntity>();
        
        CreateMap<MediaEntity, MediaModel>();
        
        CreateMap<MovieEntity, MovieModel>();
        CreateMap<CreateMovieCommand, MovieEntity>();
        CreateMap<UpdateMovieCommand, MovieEntity>();
        CreateMap<GetMovieByTitleQuery, GetMovieByTitleModel>();
        CreateMap<GetMovieByTitleResponseModel, MovieModel>()
            .ForMember(dest => 
                dest.Genres, opt => 
                opt.MapFrom(src =>
                src.Genres.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(name => new GenreModel { Name = name })
                    .ToList()
            ));
        
        CreateMap<MusicEntity, MusicModel>();
        CreateMap<CreateMusicCommand, MusicEntity>();
        CreateMap<UpdateMusicCommand, MusicEntity>();
    }
}