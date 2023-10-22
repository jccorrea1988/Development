using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPeliculasNet7.Shared.Entidades
{
    public class GenerosPeliculas
    {
        public int PeliculaId { get; set; }
        public int GeneroId { get; set; }
        public Genero? Genero { get; set; }
        public Pelicula? Pelicula { get; set; }
    }
}
