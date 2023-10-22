using AutoMapper;
using BlazorPeliculasNet7.Shared.DTOs;
using BlazorPeliculasNet7.Shared.Entidades;

namespace BlazorPeliculasNet7.Server.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, Actor>()
                .ForMember(x => x.Foto, option => option.Ignore());

            CreateMap<Pelicula, Pelicula>()
                .ForMember(x => x.Poster, option => option.Ignore());

            CreateMap<VotoPeliculaDTO, VotoPelicula>();
        }
    }
}
