using BlazorPeliculasNet7.Client.Shared;
using BlazorPeliculasNet7.Server.Helpers;
using BlazorPeliculasNet7.Shared.DTOs;
using BlazorPeliculasNet7.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPeliculasNet7.Server.Repositories
{
    public class ActorRepository
    {
        private readonly ApplicationDbContext context;

        public ActorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Actor> GetActor()
        {
            return context.Actores.AsQueryable();
        }
    }
}
