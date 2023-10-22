using BlazorPeliculasNet7.Shared.Entidades;

namespace BlazorPeliculasNet7.Client.Repositorios
{
    public interface IRepositorio
    {
        List<Pelicula> ObtenerPeliculas();
    }
}
