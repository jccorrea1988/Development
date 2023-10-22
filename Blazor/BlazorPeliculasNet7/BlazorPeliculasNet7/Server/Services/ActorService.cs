using BlazorPeliculasNet7.Server.Repositories;
using BlazorPeliculasNet7.Shared.Entidades;

namespace BlazorPeliculasNet7.Server.Services
{
    public class ActorService
    {
        private readonly ActorRepository actorRepository;

        public ActorService(ActorRepository actorRepository)
        {
            this.actorRepository = actorRepository;
        }

        public IQueryable<Actor> GetActor()
        {
            return actorRepository.GetActor();
        }
    }
}
